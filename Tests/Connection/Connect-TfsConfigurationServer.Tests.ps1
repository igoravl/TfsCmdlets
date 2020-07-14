. "$(Join-Path $PSScriptRoot '/../_TestSetup.ps1')"

InModuleScope 'TfsCmdlets' {

    BeforeAll {
        $expectedUri = 'https://foo/bar'
        $expectedInteractiveCred = 'InteractiveCred'
        $expectedCred = 'usr:pwd'

        Mock 'Get-TfsCredential' -ParameterFilter { $Interactive.IsPresent } -MockWith { return $expectedInteractiveCred } 
    }

    Describe 'Connect-TfsConfigurationServer' -Tag PSDesktop {

        Context 'When connecting interactively' {

            Mock 'Get-TfsConfigurationServer' -ParameterFilter { ($Server -eq $expectedUri) -and ($Credential -eq $expectedInteractiveCred) } -MockWith { return @{ Uri = $Server; Credential = $Credential } }
            Mock '_SetMru' -ParameterFilter { $ListName -eq 'Server' -and $Value -eq $expectedUri } -Verifiable

            Connect-TfsConfigurationServer -Server $expectedUri -Interactive

            It 'Should call mocked cmdlets' {
                Assert-MockCalled 'Get-TfsCredential' -Times 1 -Exactly
                Assert-MockCalled 'Get-TfsConfigurationServer' -Times 1 -Exactly
            }

            It 'Should set server connection object' {
                [TfsCmdlets.CurrentConnections]::Server.Uri | Should Be $expectedUri
                [TfsCmdlets.CurrentConnections]::Server.Credential | Should Be $expectedInteractiveCred
            }

            It 'Should clear other connection objects' {
                [TfsCmdlets.CurrentConnections]::Collection | Should Be $null
                [TfsCmdlets.CurrentConnections]::Project | Should Be $null
                [TfsCmdlets.CurrentConnections]::Team | Should Be $null
            }

            It 'Should update MRU list' {
                Assert-VerifiableMock
            }
        }
    }

    Describe 'Connect-TfsConfigurationServer' {

        Context 'When connecting with default credential' {

            Mock 'Get-TfsConfigurationServer' -ParameterFilter { ($Server -eq $expectedUri) -and ($Credential -eq $null)} -MockWith { return @{ Uri = $Server; Credential = $Credential } } -Verifiable
            Mock '_SetMru' -ParameterFilter { $ListName -eq 'Server' -and $Value -eq $expectedUri } -Verifiable

            [TfsCmdlets.CurrentConnections]::Reset()

            Connect-TfsConfigurationServer -Server $expectedUri

            It 'Should call mocked cmdlets' {
                Assert-MockCalled 'Get-TfsCredential' -Times 0
                Assert-MockCalled 'Get-TfsConfigurationServer' -Times 1 -Exactly
            }

            It 'Should set server connection object' {
                [TfsCmdlets.CurrentConnections]::Server.Uri | Should Be $expectedUri
                [TfsCmdlets.CurrentConnections]::Server.Credential | Should Be $null
            }

            It 'Should clear other connection objects' {
                [TfsCmdlets.CurrentConnections]::Collection | Should Be $null
                [TfsCmdlets.CurrentConnections]::Project | Should Be $null
                [TfsCmdlets.CurrentConnections]::Team | Should Be $null
            }

            It 'Should update MRU list' {
                Assert-VerifiableMock
            }
        }

        Context 'When connecting with explicit credential' {

            Mock 'Get-TfsConfigurationServer' -ParameterFilter { ($Server -eq $expectedUri) -and ($Credential -eq $expectedCred)} -MockWith { return @{ Uri = $Server; Credential = $Credential } } -Verifiable
            Mock '_SetMru' -ParameterFilter { $ListName -eq 'Server' -and $Value -eq $expectedUri } -Verifiable

            [TfsCmdlets.CurrentConnections]::Reset()

            Connect-TfsConfigurationServer -Server $expectedUri -Credential $expectedCred

            It 'Should call mocked cmdlets' {
                Assert-MockCalled 'Get-TfsCredential' -Times 0
                Assert-MockCalled 'Get-TfsConfigurationServer' -Times 1 -Exactly
            }

            It 'Should set server connection object' {
                [TfsCmdlets.CurrentConnections]::Server.Uri | Should Be $expectedUri
                [TfsCmdlets.CurrentConnections]::Server.Credential | Should Be $expectedCred
            }

            It 'Should clear other connection objects' {
                [TfsCmdlets.CurrentConnections]::Collection | Should Be $null
                [TfsCmdlets.CurrentConnections]::Project | Should Be $null
                [TfsCmdlets.CurrentConnections]::Team | Should Be $null
            }

            It 'Should update MRU list' {
                Assert-VerifiableMock
            }
        }

        Context 'When connecting to invalid server' {

            Mock 'Get-TfsConfigurationServer' -MockWith { return $null }
            Mock '_SetMru'

            It 'Should throw' {
                { Connect-TfsConfigurationServer -Server $expectedUri -Interactive } | Should Throw
                { Connect-TfsConfigurationServer -Server $expectedUri -Credential $expectedCred } | Should Throw
                { Connect-TfsConfigurationServer -Server $expectedUri } | Should Throw
            }

            It 'Should not update MRU list' {
                Assert-MockCalled '_SetMru' -Times 0
            }
        }
    }
}