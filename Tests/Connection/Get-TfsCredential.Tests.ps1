. "$(Join-Path $PSScriptRoot '/../_TestSetup.ps1' -Resolve)"

InModuleScope 'TfsCmdlets' {

    Describe 'Get-TfsCredential' -Tag 'PSCore' {

        Context 'When connecting interactively' {

            It 'Should throw' {
                { $cred = Get-TfsCredential -Interactive } | Should -Throw
            }
        }

        Context 'When connecting with cached credentials' {

            $cred = Get-TfsCredential -Cached

            It 'Should use default credentials' {
                $cred.Windows.UseDefaultCredentials | Should -Be $true
            }
        }
    }

    Describe 'Get-TfsCredential' -Tag 'PSDesktop' {

        Context 'When connecting interactively' {

            It 'Should return an interactive credential' {
                $cred = Get-TfsCredential -Interactive

                $cred.PromptType.ToString() | Should -Be 'PromptIfNeeded'
            }

            It 'Should not use default credentials' {
                $cred = Get-TfsCredential -Interactive

                $cred.Windows.UseDefaultCredentials | Should -Be $false
                $cred.Federated.GetType().GetProperty('Storage', 36).GetValue($cred.Federated) | Should -Be $null
            }
        }

        Context 'When connecting with cached credentials' {

            $cred = Get-TfsCredential -Cached

            It 'Should use default credentials' {
                $cred.Windows.UseDefaultCredentials | Should -Be $true
                $cred.Federated.GetType().GetProperty('Storage', 36).GetValue($cred.Federated) | Should -Not -Be $null
            }
        }
    }
}    