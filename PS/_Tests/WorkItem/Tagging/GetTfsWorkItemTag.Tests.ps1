& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsWorkItemTag
        #  [[-Tag] <Object>]
        #  [-IncludeInactive]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return all tags' {
            $tags = Get-TfsWorkItemTag -Project $tfsProject
            $tags.Name | Sort-Object | Should -Be @('MyTag1', 'MyTag2', 'MyTag3')
        }

        # It 'Should return all tags including inactive' {
        #     $tags = Get-TfsWorkItemTag -IncludeInactive -Project $tfsProject
        #     $tags.Name | Sort-Object | Should -Be @('MyTag1', 'MyTag2', 'MyTag3')
        # }

        It 'Should return a tag' {
            $tag = Get-TfsWorkItemTag -Tag 'MyTag1' -Project $tfsProject
            $tag.Name | Should -Be 'MyTag1'
        }

        # It 'Should return a tag including inactive' {
        #     $tag = Get-TfsWorkItemTag -Tag 'MyTag3' -IncludeInactive -Project $tfsProject
        #     $tag.Name | Should -Be 'MyTag3'
        # }

        It 'Should return multiple tags from a list' {
            $tags = Get-TfsWorkItemTag 'MyTag1', 'MyTag3' -Project $tfsProject
            $tags.Name | Sort-Object | Should -Be @('MyTag1', 'MyTag3')
        }

        # It 'Should return multiple tags from a list including inactive' {
        #     $tags = Get-TfsWorkItemTag 'MyTag1', 'MyTag3' -Project $tfsProject -IncludeInactive
        #     $tags.Name | Sort-Object | Should -Be @('MyTag1', 'MyTag3')
        # }
    }
}