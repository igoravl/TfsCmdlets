Param($Configuration = 'Release')

$scriptPath = Split-Path $MyInvocation.MyCommand.Path -Parent
$solutionDir = Join-Path $scriptPath 'TfsCmdlets'
$NugetExePath = Join-Path $solutionDir '.nuget\nuget.exe'

& $NugetExePath Restore "$solutionDir\TfsCmdlets.sln"

$packagesDir = Join-Path $solutionDir 'packages'
$pesterDir = Get-ChildItem "$packagesDir\pester*.*" | sort -Descending | select -First 1 -ExpandProperty FullName
$pesterModulePath = Join-Path $pesterDir 'tools\pester.psm1'

Get-Module pester | Remove-Module
Import-Module $pesterModulePath

Invoke-Pester -Path (Join-Path $solutionDir "TfsCmdlets.UnitTests") -OutputFile Results.xml -OutputFormat NUnitXml
