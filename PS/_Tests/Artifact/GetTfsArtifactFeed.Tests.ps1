& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {
        
        It 'Should Return only Org-level feeds' {
            $feeds = Get-TfsArtifactFeed -Scope Collection
            $feeds.Count   | Should -BeGreaterOrEqual 1
            $feeds.Project | Should -BeNullOrEmpty
        }

        It 'Should return feeds from all scopes' {
            (Get-TfsArtifactFeed).Name | Sort-Object -Unique | Should -Be @('Default', 'TestProjectFeed', 'TfsCmdletsAAD', 'TfsCmdletsFeed')
        }

        It 'Should filter feeds by name' {
            (Get-TfsArtifactFeed -Feed 'Default').Name | Should -Be 'Default'
            (Get-TfsArtifactFeed 'T*').Name  | Sort-Object -Unique | Should -Be @('TestProjectFeed', 'TfsCmdletsAAD', 'TfsCmdletsFeed')
        }

        It 'Should Return All Project-Level feeds' {
            $feeds = Get-TfsArtifactFeed -Scope Project
            $feeds.Count | Should -Be 3
            $feeds.Project.Name | Sort-Object -Unique | Should -Be @('AgileGit', $tfsProject)
        }

        It 'Should Filter feeds by project' {
            (Get-TfsArtifactFeed -Scope Project -Project 'AgileGit').Project.Name | Select-Object -Unique | Should -Be 'AgileGit'
            (Get-TfsArtifactFeed -Scope Project -Project 'A*').Project.Name | Select-Object -Unique | Should -Be 'AgileGit'
            (Get-TfsArtifactFeed -Scope Project -Project $tfsProject).Project.Name | Select-Object -Unique | Should -Be $tfsProject
        }
    } 
}