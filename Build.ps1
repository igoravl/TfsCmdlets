[CmdletBinding()]
Param
(
    $SolutionDir = (Join-Path $PSScriptRoot 'src'),
    $Configuration = 'Release',
    $EnableFusionLog = $false,
    $ModuleName = 'TfsCmdlets',
    $ModuleAuthor = 'Igor Abade V. Leite',
    $ModuleDescription = 'PowerShell Cmdlets for TFS and VSTS',
    $Targets = "Package"
)

try
{
    Write-Host "Building $ModuleName ($ModuleDescription)`n" -ForegroundColor Cyan

    Write-Verbose "Module being built from $SolutionDir"

    Push-Location $SolutionDir

    # Restore/install Nuget

    Write-Verbose "Restoring Nuget client (if needed)"

    $PackagesDir = Join-Path $SolutionDir 'packages'
    $NugetExePath = Join-Path $SolutionDir 'nuget.exe'

    Write-Verbose "PackagesDir: $PackagesDir"
    Write-Verbose "NugetExePath: $NugetExePath"
    
    if (-not (Test-Path $PackagesDir -PathType Container))
    {
        Write-Verbose "Folder $PackagesDir not found. Creating folder."
        md $PackagesDir -Force | Write-Verbose
    }

    if (-not (Test-Path $NugetExePath -PathType Leaf))
    {
        Write-Verbose "Nuget.exe not found. Downloading from https://dist.nuget.org"
        Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile $NugetExePath | Write-Verbose
    }

    # Restore/install GitVersion

    Write-Verbose "Restoring GitVersion client (if needed)"

    & $NugetExePath Install GitVersion.CommandLine -ExcludeVersion -OutputDirectory Packages | Write-Verbose
    $GitVersionPath = Join-Path $SolutionDir 'packages\gitversion.commandline\tools\GitVersion.exe'
    $script:VersionMetadata = (& $GitVersionPath | ConvertFrom-Json)

    # Set VSTS build name

    if ($env:BUILD_BUILDURI)
    {
        Write-Output "##vso[build.updatebuildnumber]$($VersionMetadata.FullSemver)"
    }

    # Restore/install Psake

    Write-Verbose "Restoring Psake (if needed)"

    & $NugetExePath Install psake -ExcludeVersion -OutputDirectory packages | Write-Verbose
    $psakeModulePath = Join-Path $SolutionDir 'packages\psake\tools\psake.psm1'
    Get-Module psake | Remove-Module
    Import-Module $psakeModulePath

    # Restore/install vswhere

    Write-Verbose "Restoring vswhere (if needed)"

    & $NugetExePath Install vswhere -ExcludeVersion -OutputDirectory packages | Write-Verbose
    $vswherePath = Join-Path $SolutionDir 'packages\vswhere\tools\vswhere.exe'

    # Detect installed Visual Studio version

    $vsVersion = & $vswherePath -latest -legacy -property installationVersion -format value -version [12.0

    if (-not $vsVersion)
    {
        throw "Visual Studio installation not found. It usually means a supported VS version (2013 and newer) is not currently installed."
    }

    # Run Psake

    Write-Host "Running Psake script`n" -ForegroundColor Cyan

    Invoke-Psake -BuildFile (Resolve-Path 'psake.ps1') -TaskList $Targets -Verbose:([bool] ($PSBoundParameters['Verbose'].IsPresent)) `
      -Parameters @{
        SolutionDir = $SolutionDir; 
        Configuration = $Configuration;
        BranchName = $VersionMetadata.BranchName;
        ModuleName = $ModuleName;
        ModuleAuthor = $ModuleAuthor;
        ModuleDescription = $ModuleDescription;
        Commit = $VersionMetadata.Sha;
        Version = $VersionMetadata.MajorMinorPatch;
        PreRelease = "$($VersionMetadata.PreReleaseLabel)$($VersionMetadata.PreReleaseNumber)";
        BuildName = "$($VersionMetadata.FullSemver).$($VersionMetadata.BranchName)";
        VisualStudioVersion = ([version]$vsVersion).Major;
        SemVer = $VersionMetadata.LegacySemVer
        VersionMetadata = $VersionMetadata
    }
}
finally
{
    Pop-Location
}

if (-not $psake.build_success)
{ 
    throw "Build failed. See log above for details."
}
