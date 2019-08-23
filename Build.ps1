#requires -Version 5

[CmdletBinding()]
Param
(
    $SolutionDir,
    $Configuration = 'Release',
    $EnableFusionLog = $false,
    $ModuleName = 'TfsCmdlets',
    $ModuleAuthor = 'Igor Abade V. Leite',
    $ModuleDescription = 'PowerShell Cmdlets for Azure DevOps and Team Foundation Server',
    $Targets = "Package",
    $RepoCreationDate = (Get-Date '2014-10-24')
)

Function Install-Dependencies
{
    $NugetPackages = @('GitVersion.CommandLine')
    $PsModules = @('InvokeBuild', 'psake', 'PsScriptAnalyzer')

    $script:PackagesDir = Join-Path $SolutionDir 'packages'

    Write-Verbose "Restoring missing dependencies. Packages directory: $PackagesDir"

    Install-Nuget

    Write-Verbose "Restoring NuGet package(s) ($($NugetPackages -join ', '))"

    foreach($pkg in $NugetPackages)
    {
        Install-NugetPackage $pkg
    }

    Write-Verbose "Restoring PowerShell module(s) ($($PsModules -join ', '))"

    foreach($mod in $PsModules)
    {
        Install-PsModule $mod
    }
}

Function Install-Nuget
{
    Write-Verbose "Restoring Nuget client"

    $BuildToolsDir = Join-Path $SolutionDir 'BuildTools'
    $script:NugetExePath = Join-Path $BuildToolsDir 'nuget.exe'

    if (-not (Test-Path $PackagesDir -PathType Container))
    {
        mkdir $PackagesDir -Force | Write-Verbose
    }

    if (-not (Test-Path $BuildToolsDir -PathType Container))
    {
        mkdir $BuildToolsDir -Force | Write-Verbose
    }

    if (-not (Test-Path $NugetExePath -PathType Leaf))
    {
        Write-Verbose "Nuget.exe not found. Downloading from https://dist.nuget.org"
        Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile $NugetExePath | Write-Verbose
    }
    else
    {
        Write-Verbose "NuGet client found; Skipping..."    
    }

    Write-Verbose "NugetExePath: $NugetExePath"
}

Function Install-NugetPackage($Package)
{
    Write-Verbose "Restoring NuGet package $Package"

    $modulePath = Join-Path $SolutionDir "packages/$Package"

    if (-not (Test-Path "$modulePath/*" -PathType Leaf))
    {
        Write-Verbose "Package not found. Downloading from Nuget.org"
        & $NugetExePath Install $Package -ExcludeVersion -OutputDirectory packages *>&1 | Write-Verbose
    }
    else
    {
        Write-Verbose "NuGet package $Package found; Skipping..."    
    }
}

Function Install-PsModule($Module)
{
    Write-Verbose "Restoring module $Module"

    if (-not (Get-Module $Module -ListAvailable))
    {
        if (-not (Get-PackageProvider -Name Nuget -ListAvailable -ErrorAction SilentlyContinue))
        {
            Write-Verbose "Installing required Nuget package provider in order to install modules from PowerShell Gallery"
            Install-PackageProvider Nuget -Force -Scope CurrentUser | Write-Verbose
        }

        Install-Module $Module -Scope CurrentUser -Force | Write-Verbose
    }
    else
    {
        Write-Verbose "PowerShell module $Module found; Skipping..."    
    }
}

try
{
    if (-not $SolutionDir)
    {
        $SolutionDir = $PSScriptRoot
    }

    Write-Host "Building $ModuleName ($ModuleDescription)`n" -ForegroundColor Cyan

    Write-Verbose "SolutionDir: $SolutionDir"

    Push-Location $SolutionDir

    # Restore/install dependencies

    Write-Verbose "=== RESTORE DEPENDENCIES ==="

    Install-Dependencies

    # Set build name

    Write-Verbose "=== SET BUILD NAME ==="

    $GitVersionPath = Join-Path $SolutionDir 'packages\gitversion.commandline\tools\GitVersion.exe'
    $script:VersionMetadata = (& $GitVersionPath | ConvertFrom-Json)

    $VersionMetadata | Write-Verbose

    $ProjectBuildNumber = ((Get-Date) - $RepoCreationDate).Days

    Write-Warning $env:BUILD_REASON
    
    if($env:BUILD_REASON -eq 'PullRequest')
    {
        $LegacyBuildMetadata =  $VersionMetadata.PreReleaseTagWithDash
        $SemVerMetadata = $LegacyBuildMetadata
    }
    else
    {
        $LegacyBuildMetadata = "$($VersionMetadata.PreReleaseTagWithDash)+0$($VersionMetadata.BuildMetadata)"
        $SemVerMetadata = "$($VersionMetadata.PreReleaseTagWithDash)+$ProjectBuildNumber.0$($VersionMetadata.BuildMetadata)"
    }

    $LegacyVersion = "$($VersionMetadata.MajorMinorPatch).$ProjectBuildNumber"
    $LegacyFullVersion = "${LegacyVersion}$LegacyBuildMetadata"
    
    $SemVerVersion = "$($VersionMetadata.MajorMinorPatch)"
    $SemVerFullVersion = "${SemVerVersion}$SemVerMetadata"

    $BuildName = $SemVerFullVersion

    Write-Verbose "Outputting build name $BuildName to host"
    Write-Host "- Build $BuildName`n" -ForegroundColor Cyan

    if ($env:BUILD_BUILDURI)
    {
        Write-Output "##vso[build.updatebuildnumber]$BuildName"
    }

    # Run Psake

    $IsVerbose = [bool] ($PSBoundParameters['Verbose'].IsPresent -or ($VerbosePreference -eq 'Continue'))
    $psakeScript = (Resolve-Path 'psake.ps1')

    Write-Verbose "=== BEGIN PSAKE ==="
    Write-Verbose "Invoking Psake script $psakeScript"

    Invoke-Psake -Nologo -BuildFile $psakeScript -TaskList $Targets -Verbose:$IsVerbose `
      -Parameters @{
        SolutionDir = $SolutionDir
        Configuration = $Configuration
        BranchName = $VersionMetadata.BranchName
        ModuleName = $ModuleName
        ModuleAuthor = $ModuleAuthor
        ModuleDescription = $ModuleDescription
        Commit = $VersionMetadata.Sha
        Version = $LegacyVersion
        NuGetVersion = $LegacyFullVersion
        PreRelease = "$($VersionMetadata.PreReleaseLabel)$($VersionMetadata.PreReleaseNumber)";
        BuildName = $BuildName
        SemVer = $SemVerFullVersion
        VersionMetadata = $VersionMetadata 
    }

    Write-Verbose "=== END PSAKE ==="

}
finally
{
    Pop-Location
}

if (-not $psake.build_success)
{ 
    throw "Build failed. See log above for details."
}
