& "$($PSScriptRoot.Split('_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        BeforeAll {
            Connect-TfsTeamProject $tfsProject
        }

        It 'Should get all folders' {
            
            (Get-TfsWorkItemQueryFolder).Path | Sort-Object | Should -Be @(
                "My Queries/Personal Queries", 
                "Shared Queries/Dashboard Queries", 
                "Shared Queries/Team Queries", 
                "Shared Queries/Team Queries/Team1 Queries", 
                "Shared Queries/Team Queries/Team2 Queries"
            )
        }

        It 'Should get folders by scope' {
            
            (Get-TfsWorkItemQueryFolder -Scope Personal).Path | Sort-Object | Should -Be @(
                "My Queries/Personal Queries"
            )

            (Get-TfsWorkItemQueryFolder -Scope Shared).Path | Sort-Object | Should -Be @(
                "Shared Queries/Dashboard Queries", 
                "Shared Queries/Team Queries", 
                "Shared Queries/Team Queries/Team1 Queries", 
                "Shared Queries/Team Queries/Team2 Queries"
            )
        }

        It 'Should get root folders' {

            (Get-TfsWorkItemQueryFolder '/').Path | Sort-Object | Should -Be @(
                "My Queries", 
                "Shared Queries"
            )

        }

        It 'Should get root folders by scope' {

            (Get-TfsWorkItemQueryFolder '/' -Scope Personal).Path | Should -Be "My Queries"

            (Get-TfsWorkItemQueryFolder '/' -Scope Shared).Path | Should -Be "Shared Queries"

        }

        It 'Should get folders by name' {
            
            (Get-TfsWorkItemQueryFolder "Personal Queries").Path | Should -Be "My Queries/Personal Queries"

            (Get-TfsWorkItemQueryFolder "Team Queries").Path | Should -Be "Shared Queries/Team Queries"
        }

        It 'Should get folders by wildcard' {
            
            (Get-TfsWorkItemQueryFolder "Pers*").Path | Should -Be "My Queries/Personal Queries"

            (Get-TfsWorkItemQueryFolder "Team*").Path | Sort-Object | Should -Be @(
                "Shared Queries/Team Queries"
            )

            (Get-TfsWorkItemQueryFolder "Team*/*").Path | Sort-Object | Should -Be @(
                "Shared Queries/Team Queries/Team1 Queries", 
                "Shared Queries/Team Queries/Team2 Queries"
            )

            (Get-TfsWorkItemQueryFolder "*/*2*").Path | Sort-Object | Should -Be @(
                "Shared Queries/Team Queries/Team2 Queries"
            )

            (Get-TfsWorkItemQueryFolder "**/*1*").Path | Sort-Object | Should -Be @(
                "Shared Queries/Team Queries/Team1 Queries"
            )
        }

        AfterAll {
            Disconnect-TfsTeamProject
        }
    }
}
