$outputDir = Join-Path (Split-Path $MyInvocation.MyCommand.Path -Parent) '..\..\out' -Resolve
$modulePath = Join-Path $outputDir 'Module\TfsCmdlets.psd1' -Resolve

Get-Module TfsCmdlets | Remove-Module

Import-Module $modulePath -Force