Get-Module TfsCmdlets | Remove-Module
$projectPath = (Split-Path -Parent $MyInvocation.MyCommand.Path).Replace(".UnitTests", "")
$rootModulePath = $projectPath + "\TfsCmdlets.psd1"
Import-Module $rootModulePath 
