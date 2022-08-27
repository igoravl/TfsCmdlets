. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should get all revisions' {
            (Get-TfsWorkItemHistory 150).Count | Should -Be 4
        }
        
    }
}
