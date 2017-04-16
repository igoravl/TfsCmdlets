$scriptPath = Split-Path $MyInvocation.MyCommand.Path -Parent

Get-Module TfsCmdlets | Remove-Module

Import-Module (Join-Path $scriptPath 'out\module\TfsCmdlets.psd1')

Get-Module TfsCmdlets