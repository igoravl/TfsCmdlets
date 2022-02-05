. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should not throw on parameterless invocation' {
            { Get-TfsTeamProject } | Should -Not -Throw
        }

        It 'Should get all projects' {
            Get-TfsTeamProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('AgileGit', 'TestProject', 'TfsCmdlets')
        }

        It 'Should not get process info without IncludeDetails' {
            Get-TfsTeamProject | Select-Object -ExpandProperty ProcessTemplate | Should -Be @('(N/A)', '(N/A)', '(N/A)')
        }

        It 'Should get process info with IncludeDetails' {
            Get-TfsTeamProject -IncludeDetails | Select-Object -ExpandProperty ProcessTemplate | Sort-Object | Should -Be @('Agile', 'Agile', 'Scrum')
        }

        It 'Should get deleted projects' {
            Get-TfsTeamProject -Deleted | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('DeletedProject')
        }

    }
}
