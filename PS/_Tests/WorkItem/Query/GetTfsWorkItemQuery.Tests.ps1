& "$($PSScriptRoot.Split('_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        BeforeAll {
            Connect-TfsTeamProject $tfsProject
        }

        It 'Should get all queries' {
            
            (Get-TfsWorkItemQuery).Path | Sort-Object | Should -Be @(
                "My Queries/Followed work items",
                "My Queries/Personal Queries/Assigned to me",
                "Shared Queries/All bugs",
                "Shared Queries/Critical Bugs",
                "Shared Queries/Dashboard Queries/User Stories", 
                "Shared Queries/Feedback_WI",	
                "Shared Queries/Team Queries/All Items_WI", 
                "Shared Queries/Team Queries/Team1 Queries/Unfinished Work_WI", 
                "Shared Queries/Team Queries/Team2 Queries/Work in progress_WI",
                "Shared Queries/Test Case-Readiness"
            )
        }

        It 'Should get queries by scope' {
            
            (Get-TfsWorkItemQuery -Scope Personal).Path | Sort-Object | Should -Be @(
                "My Queries/Followed work items",
                "My Queries/Personal Queries/Assigned to me"
            )

            (Get-TfsWorkItemQuery -Scope Shared).Path | Sort-Object | Should -Be @(
                "Shared Queries/All bugs",
                "Shared Queries/Critical Bugs",
                "Shared Queries/Dashboard Queries/User Stories", 
                "Shared Queries/Feedback_WI",	
                "Shared Queries/Team Queries/All Items_WI", 
                "Shared Queries/Team Queries/Team1 Queries/Unfinished Work_WI", 
                "Shared Queries/Team Queries/Team2 Queries/Work in progress_WI",
                "Shared Queries/Test Case-Readiness"
            )
        }

        It 'Should get queries by name' {

            (Get-TfsWorkItemQuery 'Critical Bugs').Path | Should -Be "Shared Queries/Critical Bugs"
            (Get-TfsWorkItemQuery 'Assigned to me').Path | Should -BeNullOrEmpty
            (Get-TfsWorkItemQuery 'Personal Queries/Assigned to me').Path | Should -Be "My Queries/Personal Queries/Assigned to me"
            (Get-TfsWorkItemQuery 'Unfinished Work_WI').Path | Should -BeNullOrEmpty
            (Get-TfsWorkItemQuery 'Shared Queries/Team Queries/Team1 Queries/Unfinished Work_WI').Path | Should -Be "Shared Queries/Team Queries/Team1 Queries/Unfinished Work_WI"

        }

        It 'Should get queries by wildcard' {

            (Get-TfsWorkItemQuery D*/*).Path | Sort-Object | Should -Be @(
                "Shared Queries/Dashboard Queries/User Stories"
            )
            
            (Get-TfsWorkItemQuery *_WI).Path | Sort-Object | Should -Be @(
                "Shared Queries/Feedback_WI"
            )

            (Get-TfsWorkItemQuery 'Team Queries/*/*').Path | Sort-Object | Should -Be @(
                "Shared Queries/Team Queries/Team1 Queries/Unfinished Work_WI", 
                "Shared Queries/Team Queries/Team2 Queries/Work in progress_WI"
            )

            (Get-TfsWorkItemQuery */*_WI).Path | Sort-Object | Should -Be @(
                "Shared Queries/Team Queries/All Items_WI", 
                "Shared Queries/Team Queries/Team1 Queries/Unfinished Work_WI", 
                "Shared Queries/Team Queries/Team2 Queries/Work in progress_WI"
            )
        }

        AfterAll {
            Disconnect-TfsTeamProject
        }
    }
}
