& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsBuildDefinitionFolder
        # [[-Folder] <Object>]
        # [-QueryOrder <FolderQueryOrder>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]    

        It 'Should return all build definition folders' {
            $result = Get-TfsBuildDefinitionFolder -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Build.WebApi.Folder'
            $result.Name | Sort-Object | Should -Be @('\', 'CD Pipelines', 'YAML Pipelines')
        }

        It 'Should support wildcards' {
            $result = Get-TfsBuildDefinitionFolder 'CD*' -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Build.WebApi.Folder'
            $result.Name | Should -Be 'CD Pipelines'
        }

        It 'Should support -QueryOrder' {
            (Get-TfsBuildDefinitionFolder -QueryOrder FolderAscending -Project $tfsProject).Name | Should -Be @('\', 'CD Pipelines', 'YAML Pipelines')
            (Get-TfsBuildDefinitionFolder -QueryOrder FolderDescending -Project $tfsProject).Name | Should -Be @('YAML Pipelines', 'CD Pipelines', '\')
        }
    } 
}