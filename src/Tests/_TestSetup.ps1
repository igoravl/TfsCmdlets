$scriptRoot = Split-Path $MyInvocation.MyCommand.Path -Parent
$solutionDir = Join-Path $scriptRoot '..' -Resolve
$outDir = Join-Path $solutionDir '..\out' -Resolve
$projectDir = Join-Path $solutionDir 'PS' 
$modulePath = Join-Path $outDir Module
$hasBuild = Test-Path $modulePath

if (-not $hasBuild)
{
    throw "Module TfsCmdlets not found at $modulePath. Build project prior to running tests."
}

Get-Module TfsCmdlets | Remove-Module
Import-Module (Join-Path $modulePath 'TfsCmdlets.psd1') -Force