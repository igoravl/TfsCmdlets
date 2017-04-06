$scriptPath = Split-Path $MyInvocation.MyCommand.Path -Parent

Import-Module (Join-Path $scriptPath 'out\module\TfsCmdlets.psd1')

Get-Module TfsCmdlets