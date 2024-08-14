& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsArea
        # [[-Node] <Object>]
        # [-Project <Object>] # Pipeline input
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]
        
        It 'Should get all areas' {
            $nodes = Get-TfsArea -Project $tfsProject
            $nodes | Should -BeOfType [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode]
            $nodes | Select-Object -ExpandProperty RelativePath | Sort-Object `
            | Should -Be (@('PUL', 'PUL-DB', 'PUL-DB\Migrations', 'PUL\App', 'PUL\Web', 'PUL\Web\Backend', 'PUL\Web\Frontend') | Sort-Object)
        }

        It 'Should return a single level' {
            Get-TfsArea 'PUL/*' -Project $tfsProject | Select-Object -ExpandProperty RelativePath | Sort-Object `
            | Should -Be @('PUL\App', 'PUL\Web')
        }

        It 'Should return an entire branch' {
            Get-TfsArea 'PUL\**' -Project $tfsProject | Select-Object -ExpandProperty RelativePath | Sort-Object `
            | Should -Be @('PUL\App', 'PUL\Web', 'PUL\Web\Backend', 'PUL\Web\Frontend')
        }
    } 
}
