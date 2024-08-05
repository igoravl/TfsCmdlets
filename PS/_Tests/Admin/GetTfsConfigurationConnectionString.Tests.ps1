& "$($PSScriptRoot.Split('_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Unit Tests' {
        It 'Should get connection string from local server' -Tag 'Desktop' {
        }
    }
}