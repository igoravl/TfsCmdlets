& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsBuildDefinition
        # [[-Definition] <Object>]
        # [-QueryOrder <DefinitionQueryOrder>]
        # [-Project <Object>]
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should return all build definitions' {
            $result = Get-TfsBuildDefinition -Project $tfsProject
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Build.WebApi.BuildDefinition'
            $result.Name | Sort-Object | Should -Be @('PartsUnlimitedE2E', 'TestProject')
        }

        It 'Should support wildcards' {
            $result = Get-TfsBuildDefinition -Project $tfsProject -Definition 'PartsUnlimited*'
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Build.WebApi.BuildDefinition'
            $result.Name | Should -Be 'PartsUnlimitedE2E'
        }

        It 'Should support -QueryOrder' {
            (Get-TfsBuildDefinition -QueryOrder DefinitionNameAscending -Project $tfsProject).Name | Should -Be @('PartsUnlimitedE2E', 'TestProject')
            (Get-TfsBuildDefinition -QueryOrder DefinitionNameDescending -Project $tfsProject).Name | Should -Be @('TestProject', 'PartsUnlimitedE2E')
            (Get-TfsBuildDefinition -QueryOrder LastModifiedAscending -Project $tfsProject).Name | Should -Be @('PartsUnlimitedE2E', 'TestProject')
            (Get-TfsBuildDefinition -QueryOrder LastModifiedDescending -Project $tfsProject).Name | Should -Be @('TestProject', 'PartsUnlimitedE2E')
        }
    } 
}