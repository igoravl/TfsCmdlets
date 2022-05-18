. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should get by ID and revision' {
            (Get-TfsWorkItem -ID 150).Rev | Should -Be 4
            (Get-TfsWorkItem -ID 150 -Revision 2).Rev | Should -Be 2
        }

        It 'Should get by Where' {
            (Get-TfsWorkItem -Where '[System.Id] = 150').Id | Should -Be 150
            (Get-TfsWorkItem -Where '[System.Id] = 150').Rev | Should -Be 2
        }

        It 'Should support ASOF when getting by ID' {
            (Get-TfsWorkItem 150 -AsOf (Get-Date)).Rev | Should -Be 4
            (Get-TfsWorkItem -ID 150 -Revision 2).Rev | Should -Be 2
        }

        It 'Should throw when getting deleted WIs without a project' {
            {Get-TfsWorkItem -Deleted} | Should -Throw
        }

        It 'Should get deleted WIs' {
            (Get-TfsWorkItem -Project $tfsProject -Deleted).Id | Should -Be 180
        }

        It 'Should support EVER for simple queries' {
            (Get-TfsWorkItem -BoardColumnDone $true -Ever).Id | Should -Be 41
        }

        It 'Should support ASOF for simple queries' {
            (Get-TfsWorkItem -Title 'As a customer, I would like to store my credit card details securely' -AsOf (Get-Date '2022-01-21')).Rev | Should -Be 1
        }

        It 'Should support WIQL queries' {
            $result = Get-TfsWorkItem -Wiql 'SELECT System.Id, System.Title FROM WorkItems WHERE System.Id = 150'
            $result.Count | Should -Be 1
        }

        It 'Should support saved queries' {
            $result = Get-TfsWorkItem -SavedQuery 'Shared Queries\All Bugs' -Project $tfsProject
            $result.Count | Should -Be 2
        }

        It 'Should limit fields when getting by ID' {
            $result = Get-TfsWorkItem 150 -Fields Id, Title
            $result.Count | Should -Be 1
            $result.Fields.Count | Should -Be 2
        }

        It 'Should limit fields when getting by filter' {
            $result = Get-TfsWorkItem -Fields Id, Title -Where 'System.Id = 150'
            $result.Count | Should -Be 1
            $result.Fields.Count | Should -Be 2
        }

        It 'Should limit fields in WIQL queries' {
            $result = Get-TfsWorkItem -Wiql 'SELECT System.Id, System.Title FROM WorkItems WHERE System.Id = 150'
            $result.Count | Should -Be 1
            $result.Fields.Count | Should -Be 2
        }

        It 'Should limit fields in saved queries' {
            $result = Get-TfsWorkItem -SavedQuery 'Shared Queries\All Bugs' -Project $tfsProject
            $result.Fields.Count | Should -BeLessOrEqual 6
        }

        It 'Should not get links and relations when not requested' {
            $result = Get-TfsWorkItem 150
            $result.Links | Should -BeNullOrEmpty
            $result.Relations | Should -BeNullOrEmpty
        }

        It 'Should get links and relations when requested' {
            $result = Get-TfsWorkItem 150 -IncludeLinks
            $result.Links | Should -Not -BeNullOrEmpty
            $result.Relations | Should -Not -BeNullOrEmpty
        }

    }
}
