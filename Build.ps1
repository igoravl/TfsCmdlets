[CmdletBinding()]
Param
(
    $SolutionDir = (Join-Path $PSScriptRoot 'src'),
    $Configuration = 'Release',
    $EnableFusionLog = $false,
    $ModuleName = 'TfsCmdlets',
    $ModuleAuthor = 'Igor Abade V. Leite',
    $ModuleDescription = 'PowerShell Cmdlets for TFS and VSTS',
    $Targets = "Package",
    $RepoCreationDate = (Get-Date '2014-10-24')
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

    $GitVersionPath = Join-Path $SolutionDir 'packages\gitversion.commandline\tools\GitVersion.exe'

    if (-not (Test-Path $GitVersionPath -PathType Leaf))
    {
        Write-Verbose "GitVersion.exe not found. Downloading from Nuget.org"
        & $NugetExePath Install GitVersion.CommandLine -ExcludeVersion -OutputDirectory Packages *>&1 | Write-Verbose
    }
    else
    {
        Write-Verbose "FOUND! Skipping..."    
    }

    $script:VersionMetadata = (& $GitVersionPath | ConvertFrom-Json)
    $ProjectBuildNumber = ((Get-Date) - $RepoCreationDate).Days
    $ProjectMetadataInfo = "$(Get-Date -Format 'yyyyMMdd').$ProjectBuildNumber"

    # Set build name

    $BuildName = "$($VersionMetadata.LegacySemver).$ProjectMetadataInfo.$($VersionMetadata.Sha.Substring(0,8))_branch_$($VersionMetadata.BranchName.Replace('/', '-'))"
    Write-Host "- Version $($VersionMetadata.LegacySemver)+$ProjectMetadataInfo, build $BuildName`n" -ForegroundColor Cyan

    if ($env:BUILD_BUILDURI)
    {
        Write-Output "##vso[build.updatebuildnumber]$BuildName"
    }

    # Restore/install Psake

    Write-Verbose "Restoring Psake (if needed)"

    $psakeModulePath = Join-Path $SolutionDir 'packages\psake\tools\psake.psm1'

    if (-not (Test-Path $psakeModulePath -PathType Leaf))
    {
        Write-Verbose "psake.psm1 not found. Downloading from Nuget.org"
        & $NugetExePath Install psake -ExcludeVersion -OutputDirectory packages *>&1 | Write-Verbose
    }
    else
    {
        Write-Verbose "FOUND! Skipping..."    
    }

    Get-Module psake | Remove-Module
    Import-Module $psakeModulePath

    # Run Psake

    $IsVerbose = [bool] ($PSBoundParameters['Verbose'].IsPresent)

    Invoke-Psake -Nologo -BuildFile (Resolve-Path 'psake.ps1') -TaskList $Targets -Verbose:$IsVerbose `
      -Parameters @{
        SolutionDir = $SolutionDir; 
        Configuration = $Configuration;
        BranchName = $VersionMetadata.BranchName;
        ModuleName = $ModuleName;
        ModuleAuthor = $ModuleAuthor;
        ModuleDescription = $ModuleDescription;
        Commit = $VersionMetadata.Sha;
        Version = "$($VersionMetadata.MajorMinorPatch).$($ProjectBuildNumber)";
        PreRelease = "$($VersionMetadata.PreReleaseLabel)$($VersionMetadata.PreReleaseNumber)";
        BuildName = "$BuildName";
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
