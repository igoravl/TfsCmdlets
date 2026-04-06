#requires -Version 5

[CmdletBinding()]
Param
(
    $RootProjectDir,
    $Configuration = 'Release',
    $EnableFusionLog = $false,
    $ModuleName = 'TfsCmdlets',
    $ModuleAuthor = 'Igor Abade V. Leite',
    $ModuleDescription = 'PowerShell Cmdlets for Azure DevOps and Team Foundation Server',
    $Targets = @("Package"),
    $RepoCreationDate = (Get-Date '2014-10-24'),
    [switch] $SkipTests,
    [switch] $SkipReleaseNotes,
    [switch] $Incremental
)

# Dependency versions (lockfile)
$script:DependencyVersions = @{
    DotNetTools = [Ordered]@{
        'GitVersion.Tool' = '6.7.0'
    }
    NugetPackages = [Ordered]@{
    }
    PsModules = [Ordered]@{
        'psake'            = $null
        'PsScriptAnalyzer' = $null
        'VSSetup'          = $null
        'powershell-yaml'  = $null
        'ps1xmlgen'        = $null
        'PlatyPS'          = $null
    }
}

Function Assert-DotNetSdk {
    $dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
    if (-not $dotnet) {
        throw "dotnet SDK not found. Please install .NET SDK 8 or later from https://dotnet.microsoft.com/download"
    }

    $sdkVersion = dotnet --version 2>$null
    if (-not $sdkVersion) {
        throw "Unable to determine dotnet SDK version. Please install .NET SDK 8 or later from https://dotnet.microsoft.com/download"
    }

    $major = [int] ($sdkVersion -split '\.')[0]
    if ($major -lt 8) {
        throw ".NET SDK 8+ is required, but found $sdkVersion. Please upgrade from https://dotnet.microsoft.com/download"
    }

    Write-Verbose "dotnet SDK $sdkVersion found"
}

Function Install-Dependencies {
    Write-Verbose "Restoring missing dependencies"

    $dotNetTools = $script:DependencyVersions.DotNetTools
    Write-Verbose "Restoring dotnet tool(s) ($($dotNetTools.Keys -join ', '))"
    foreach ($tool in $dotNetTools.GetEnumerator()) {
        Install-DotNetTool $tool.Key $tool.Value
    }

    $nugetPackages = $script:DependencyVersions.NugetPackages
    if ($nugetPackages.Count -gt 0) {
        Write-Verbose "Restoring NuGet package(s) ($($nugetPackages.Keys -join ', '))"
        foreach ($pkg in $nugetPackages.GetEnumerator()) {
            Install-NugetPackage $pkg.Key $pkg.Value
        }
    }

    $psModules = $script:DependencyVersions.PsModules
    Write-Verbose "Restoring PowerShell module(s) ($($psModules.Keys -join ', '))"
    foreach ($mod in $psModules.GetEnumerator()) {
        Install-PsModule $mod.Key $mod.Value
    }
}

Function Install-DotNetTool($Tool, $Version) {
    Write-Verbose "Restoring dotnet local tool $Tool $Version"

    # Ensure a tool manifest exists
    if (-not (Test-Path (Join-Path $RootProjectDir '.config/dotnet-tools.json')) -and
        -not (Test-Path (Join-Path $RootProjectDir 'dotnet-tools.json'))) {
        Write-Verbose "Tool manifest not found. Creating with 'dotnet new tool-manifest'"
        dotnet new tool-manifest *>&1 | Write-Verbose
    }

    # Restore tools already listed in the manifest (idempotent)
    Write-Verbose "Running 'dotnet tool restore'"
    dotnet tool restore *>&1 | Write-Verbose

    # Check whether the correct version is already installed
    $installed = (dotnet tool list --local 2>$null | Select-String $Tool)
    if ($installed -and ($installed -match $Version)) {
        Write-Verbose "$Tool $Version already installed; Skipping..."
        return
    }

    Write-Verbose "Installing $Tool $Version"
    dotnet tool install --local $Tool --version $Version *>&1 | Write-Verbose
}

Function Install-NugetPackage($Package, $Version, $OutputDirectory = 'packages') {
    Write-Verbose "Restoring NuGet package $Package"

    $pkgDir = Join-Path $RootProjectDir "$OutputDirectory/$Package"

    if (Test-Path "$pkgDir/*" -PathType Leaf) {
        Write-Verbose "NuGet package $Package found; Skipping..."
        return
    }

    Write-Verbose "Package not found. Downloading from NuGet.org"
    $addArgs = @('add', 'package', $Package, '--package-directory', (Join-Path $RootProjectDir $OutputDirectory), '--no-restore')
    if ($Version) { $addArgs += '--version', $Version }
    dotnet nuget @addArgs *>&1 | Write-Verbose
}

Function Install-PsModule($Module, $Version) {
    Write-Verbose "Restoring module $Module"

    $existing = Get-Module $Module -ListAvailable | Select-Object -First 1
    if ($existing -and (-not $Version -or $existing.Version -eq $Version)) {
        Write-Verbose "PowerShell module $Module found; Skipping..."
        return
    }

    if (-not (Get-PackageProvider -Name Nuget -ListAvailable -ErrorAction SilentlyContinue)) {
        Write-Verbose "Installing required Nuget package provider in order to install modules from PowerShell Gallery"
        Install-PackageProvider Nuget -Force -Scope CurrentUser | Write-Verbose
    }

    $installArgs = @{ Name = $Module; Scope = 'CurrentUser'; Force = $true }
    if ($Version) { $installArgs['RequiredVersion'] = $Version }
    Install-Module @installArgs | Write-Verbose
}

try {
    if (-not $RootProjectDir) {
        $RootProjectDir = $PSScriptRoot
    }

    Write-Host "Building $ModuleName ($ModuleDescription)`n" -ForegroundColor Cyan

    Write-Verbose "SolutionDir: $RootProjectDir"

    Push-Location $RootProjectDir

    # Restore/install dependencies

    Write-Verbose "=== RESTORE DEPENDENCIES ==="

    Assert-DotNetSdk
    Install-Dependencies

    # Set build name

    Write-Verbose "=== SET BUILD NAME ==="

    $VersionMetadata = (dotnet gitversion | ConvertFrom-Json)
    $ProjectBuildNumber = ((Get-Date) - $RepoCreationDate).Days
    $BuildName = $VersionMetadata.FullSemVer.Replace('+', "+$ProjectBuildNumber.")

    $VersionMetadata | Write-Verbose

    Write-Verbose "Outputting build name $BuildName to host"
    Write-Host "- Build $BuildName`n" -ForegroundColor Cyan

    $isCI = $false

    if ($env:BUILD_BUILDURI) {
        Write-Output "##vso[build.updatebuildnumber]$BuildName"
        $isCI = $true
    }
    elseif ($env:GITHUB_ACTIONS) {
        Write-Output "BUILD_NAME=$BuildName" >> $env:GITHUB_OUTPUT
        $isCI = $true
    }

    # Run Psake

    $IsVerbose = [bool] ($PSBoundParameters['Verbose'].IsPresent -or ($VerbosePreference -eq 'Continue'))
    $psakeScript = (Resolve-Path 'psake.ps1')

    Write-Verbose "=== BEGIN PSAKE ==="
    Write-Verbose "Invoking Psake script $psakeScript"

    Invoke-Psake -Nologo -BuildFile $psakeScript -TaskList $Targets -Verbose:$IsVerbose -ErrorAction SilentlyContinue `
        -Parameters @{
        RootProjectDir    = $RootProjectDir
        Configuration     = $Configuration
        ModuleName        = $ModuleName
        ModuleAuthor      = $ModuleAuthor
        ModuleDescription = $ModuleDescription
        BuildName         = $BuildName
        BuildNumber       = $ProjectBuildNumber
        VersionMetadata   = $VersionMetadata 
        SkipTests         = $SkipTests.IsPresent
        SkipReleaseNotes  = $SkipReleaseNotes.IsPresent
        Incremental       = $Incremental.IsPresent
        IsCI              = $isCI
    }

    Write-Verbose "=== END PSAKE ==="
}
finally {
    Pop-Location
}

if (-not $psake.build_success) {
    foreach($logFile in (Get-ChildItem (Join-Path $RootProjectDir 'out/*.log')))
    {
        Write-Host -ForegroundColor Red @"

========== MSBUILD LOG ===========

$(Get-Content $logFile -Raw)

======== END MSBUILD LOG =========

"@
    }

    throw "Build failed. See log above for details."
}
