$outputDir = Join-Path (Split-Path $MyInvocation.MyCommand.Path -Parent) '..\out' -Resolve
$modulePath = Join-Path $projectDir 'Module\TfsCmdlets.psd1'
$hasBuild = Test-Path $modulePath

if (-not $hasBuild)
{
    throw "Module TfsCmdlets not found at $modulePath. Build project TfsCmdlets.Build prior to running tests."
}

Get-Module TfsCmdlets | Remove-Module

Import-Module $modulePath -Force