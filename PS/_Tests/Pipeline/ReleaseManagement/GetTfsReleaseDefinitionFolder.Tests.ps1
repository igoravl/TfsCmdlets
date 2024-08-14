& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsReleaseDefinitionFolder
        # [[-Folder] <Object>]
        # [-QueryOrder <FolderPathQueryOrder>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all release definition folders' {
            $result = Get-TfsReleaseDefinitionFolder -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder'
            $result.Name | Sort-Object | Should -Be @('\', 'TestProject')
        }

        It 'Should support wildcards' {
            $result = Get-TfsReleaseDefinitionFolder -Project $tfsProject -Folder 'Test*'
            $result | Should -BeOfType 'Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder'
            $result.Name | Should -Be 'TestProject'
        }

        It 'Should support -QueryOrder' {
            (Get-TfsReleaseDefinitionFolder -QueryOrder Ascending -Project $tfsProject).Name | Should -Be @('\', 'TestProject')
            (Get-TfsReleaseDefinitionFolder -QueryOrder Descending -Project $tfsProject).Name | Should -Be @('TestProject', '\')
        }
    } 
}