. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe 'Disconnect-TfsTeam' {

        Context 'When invoking cmdlet' {

            [TfsCmdlets.CurrentConnections]::Server = 'server'
            [TfsCmdlets.CurrentConnections]::Collection = 'collection'
            [TfsCmdlets.CurrentConnections]::Project = 'project'
            [TfsCmdlets.CurrentConnections]::Team = 'team'

            Disconnect-TfsTeam

            It 'Should clear only Team object' {
                [TfsCmdlets.CurrentConnections]::Server | Should Be 'server'
                [TfsCmdlets.CurrentConnections]::Collection | Should Be 'collection'
                [TfsCmdlets.CurrentConnections]::Project | Should Be 'project'
                [TfsCmdlets.CurrentConnections]::Team | Should BeNullOrEmpty
            }
        }
    }
}