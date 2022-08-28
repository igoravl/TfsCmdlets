#requires -Version 5

[CmdletBinding()]
Param
(
    $RootProjectDir,
    $OutDir,
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

Function Install-Dependencies {
    $NugetPackages = @('GitVersion.CommandLine')
    $PsModules = @('psake', 'PsScriptAnalyzer', 'VSSetup', 'powershell-yaml', 'ps1xmlgen', 'PlatyPS')

    $script:PackagesDir = Join-Path $RootProjectDir 'packages'

    Write-Verbose "Restoring missing dependencies. Packages directory: $PackagesDir"

    Install-Nuget

    Write-Verbose "Restoring NuGet package(s) ($($NugetPackages -join ', '))"

    foreach ($pkg in $NugetPackages) {
        Install-NugetPackage $pkg
    }

    Write-Verbose "Restoring PowerShell module(s) ($($PsModules -join ', '))"

    foreach ($mod in $PsModules) {
        Install-PsModule $mod
    }
}

Function Install-Nuget {
    Write-Verbose "Restoring Nuget client"

    $buildToolsDir = Join-Path $RootProjectDir 'build-tools'
    $script:NugetExePath = Join-Path $buildToolsDir 'nuget.exe'

    if (-not (Test-Path $PackagesDir -PathType Container)) {
        mkdir $PackagesDir -Force | Write-Verbose
    }

    if (-not (Test-Path $buildToolsDir -PathType Container)) {
        mkdir $buildToolsDir -Force | Write-Verbose
    }

    if (-not (Test-Path $NugetExePath -PathType Leaf)) {
        Write-Verbose "Nuget.exe not found. Downloading from https://dist.nuget.org"
        Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile $NugetExePath | Write-Verbose
    }
    else {
        Write-Verbose "NuGet client found; Skipping..."    
    }

    Write-Verbose "NugetExePath: $NugetExePath"
}

Function Install-NugetPackage($Package) {
    Write-Verbose "Restoring NuGet package $Package"

    $modulePath = Join-Path $RootProjectDir "packages/$Package"

    if (-not (Test-Path "$modulePath/*" -PathType Leaf)) {
        Write-Verbose "Package not found. Downloading from Nuget.org"
        & $NugetExePath Install $Package -ExcludeVersion -OutputDirectory packages *>&1 | Write-Verbose
    }
    else {
        Write-Verbose "NuGet package $Package found; Skipping..."    
    }
}

Function Install-PsModule($Module) {
    Write-Verbose "Restoring module $Module"

    if (-not (Get-Module $Module -ListAvailable)) {
        if (-not (Get-PackageProvider -Name Nuget -ListAvailable -ErrorAction SilentlyContinue)) {
            Write-Verbose "Installing required Nuget package provider in order to install modules from PowerShell Gallery"
            Install-PackageProvider Nuget -Force -Scope CurrentUser | Write-Verbose
        }

        Install-Module $Module -Scope CurrentUser -Force | Write-Verbose
    }
    else {
        Write-Verbose "PowerShell module $Module found; Skipping..."    
    }
}

try {
    if (-not $RootProjectDir) {
        $RootProjectDir = (Join-Path $PSScriptRoot 'src')
    }

    if (-not $OutDir) {
        $OutDir = (Join-Path $PSScriptRoot 'out')
    }

    Write-Host "Building $ModuleName ($ModuleDescription)`n" -ForegroundColor Cyan

    Write-Verbose "SolutionDir: $RootProjectDir"

    Push-Location $RootProjectDir

    # Restore/install dependencies

    Write-Verbose "=== RESTORE DEPENDENCIES ==="

    Install-Dependencies

    # Set build name

    Write-Verbose "=== SET BUILD NAME ==="

    $GitVersionPath = Join-Path $RootProjectDir 'packages\gitversion.commandline\tools\GitVersion.exe'
    $VersionMetadata = (& $GitVersionPath | ConvertFrom-Json)
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
        Write-Output "::set-output name=BUILD_NAME::$BuildName"
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
    foreach($logFile in (Get-ChildItem (Join-Path $OutDir '*.log')))
    {
        Write-Host -ForegroundColor Red @"

========== MSBUILD LOG ===========

$(Get-Content $logFile -Raw)

======== END MSBUILD LOG =========

"@
    }

    throw "Build failed. See log above for details."
}
