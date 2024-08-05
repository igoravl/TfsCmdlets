& "$($PSScriptRoot.Split('_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should throw on parameterless invocation' {
            { Get-TfsTeam } | Should -Throw
        }

        It 'Should get all teams' {
            Get-TfsTeam -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('PUL', 'PUL-DB', 'TestProject Team')
        }

        It 'Should get some teams' {
            Get-TfsTeam 'PUL*' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('PUL', 'PUL-DB')
        }

        It 'Should get default team' {
            Get-TfsTeam -Default -Project $tfsProject | Select-Object -ExpandProperty Name | Should -Be 'TestProject Team'
        }

        It 'Should get settings with -IncludeSettings' {
            (Get-TfsTeam -Default -Project $tfsProject).Settings | Should -BeNullOrEmpty
            (Get-TfsTeam -Default -Project $tfsProject -IncludeSettings).Settings | Should -Not -BeNullOrEmpty
        }

        It 'Should get members with -QueryMembership' {
            (Get-TfsTeam -Default -Project $tfsProject).TeamMembers.Length | Should -Be 0
            (Get-TfsTeam -Default -Project $tfsProject -QueryMembership).TeamMembers.Length | Should -Be 1
        }

    }
}
