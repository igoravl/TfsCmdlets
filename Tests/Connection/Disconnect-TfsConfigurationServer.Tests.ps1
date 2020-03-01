. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe 'Disconnect-TfsConfigurationServer' {

        Context 'When invoking cmdlet' {

            [TfsCmdlets.CurrentConnections]::Server = 'foo'
            [TfsCmdlets.CurrentConnections]::Collection = 'foo'
            [TfsCmdlets.CurrentConnections]::Project = 'foo'
            [TfsCmdlets.CurrentConnections]::Team = 'foo'

            Disconnect-TfsConfigurationServer

            It 'Should clear connection objects' {
                [TfsCmdlets.CurrentConnections]::Server | Should Be $null
                [TfsCmdlets.CurrentConnections]::Collection | Should Be $null
                [TfsCmdlets.CurrentConnections]::Project | Should Be $null
                [TfsCmdlets.CurrentConnections]::Team | Should Be $null
            }
        }
    }
}