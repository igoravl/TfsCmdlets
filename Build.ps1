Param
(
    $SolutionDir = (Join-Path (Split-Path $MyInvocation.MyCommand.Path -Parent) 'TfsCmdlets'),
    $Configuration = 'Release',
    $BranchName = 'local-build',
    $Commit = '00000000',
    $BuildName = '1.0.0-alpha+1',
    $ModuleName = 'TfsCmdlets',
    $ModuleAuthor = 'Igor Abade V. Leite',
    $ModuleDescription = 'PowerShell Cmdlets for TFS and VSTS',
    $EnableFusionLog = $false
)

$BuildNameTokens = ($BuildName -split {$_ -in @('-','+')})
$BuildTimestamp = Get-Date -Format 'yyyyMMdd'

if($BuildName -match '-')
{
    $Version = $BuildNameTokens[0]
    $PreRelease = $BuildNameTokens[1]
    $BuildNumber = $BuildNameTokens[2]
}
else
{
    $Version = $BuildNameTokens[0]
    $PreRelease = ''
    $BuildNumber = $BuildNameTokens[1]
}

try
{
    if ($env:VS140COMNTOOLS)
    {
        $VisualStudioVersion = '14'
    }
    elseif ($env:VS120COMNTOOLS)
    {
        $VisualStudioVersion = '12'
    }
    else
    {
        throw "Visual Studio environment variables not found. It likely means a supported VS version (2013, 2015) is not currently installed."
    }

	Push-Location $SolutionDir

	$NugetExePath = Join-Path $SolutionDir '.nuget\nuget.exe'
	& $NugetExePath Restore

	$packagesDir = Join-Path $SolutionDir 'packages'
	$psakeDir = Get-ChildItem "$packagesDir\psake*.*" | sort -Descending | select -First 1 -ExpandProperty FullName
	$psakeModulePath = Join-Path $psakeDir 'tools\psake.psm1'

	Get-Module psake | Remove-Module

	Import-Module $psakeModulePath

	Invoke-Psake -BuildFile (Resolve-Path 'psake-default.ps1') -TaskList Build -Parameters @{
	    SolutionDir = $SolutionDir; 
	    Configuration = $Configuration;
	    BranchName = $BranchName;
	    ModuleName = $ModuleName;
	    ModuleAuthor = $ModuleAuthor;
	    ModuleDescription = $ModuleDescription;
	    Commit = $Commit;
	    Version = "$Version.$BuildNumber";
	    PreRelease = $PreRelease;
	    BuildName = "$($BuildName -replace '\+.+', '')+$BuildTimestamp.$BuildNumber";
	    BuildNumber = $BuildNumber;
        VisualStudioVersion = $VisualStudioVersion
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
