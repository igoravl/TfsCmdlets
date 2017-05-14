$outDir = Join-Path (Split-Path $MyInvocation.MyCommand.Path -Parent) '..\..\out' -Resolve
$modulePath = Join-Path $outDir Module
$hasBuild = Test-Path $modulePath

if (-not $hasBuild)
{
    throw "Module TfsCmdlets not found at $modulePath. Build project prior to running tests."
}

Get-Module TfsCmdlets | Remove-Module

$module = (Import-Module (Join-Path $modulePath 'TfsCmdlets.psd1') -Force)