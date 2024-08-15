& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {
        Write-Warning "$(Split-Path $PSCommandPath -Leaf): Test not implemented"
    } 
}