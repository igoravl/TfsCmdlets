. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Unit Tests' {
        It 'Should get connection string from local server' -Tag 'Desktop' {
        }
    }
}