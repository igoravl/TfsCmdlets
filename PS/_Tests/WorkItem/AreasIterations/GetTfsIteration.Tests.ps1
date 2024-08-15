& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsIteration
        # [[-Node] <Object>]
        # [-Project <Object>] # Pipeline input
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should throw on parameterless invocation' {
            { Get-TfsIteration } | Should -Throw
        }

        It 'Should get all iterations' {
            Get-TfsIteration '**' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Release 2', 'Sprint 1', 'Sprint 2', 'Sprint 3', 'Sprint 4', 'Sprint 5', 'Sprint 6', 'Sprint 7')
        }

        It 'Should get child iterations' {
            Get-TfsIteration 'Release 2\**' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Sprint 7')
        }

        It 'Should get by name pattern' {
            Get-TfsIteration '*1' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Sprint 1')
            Get-TfsIteration '*ele*' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('Release 2')
        }

    }
}
