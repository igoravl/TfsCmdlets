. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_IsWildcard' {

        Context 'Unit Tests' {
            It 'Should match an wildcard at the beginning of string' {
                _IsWildcard -Item '*Foo/Bar' | Should Be $true
            }
            It 'Should match an wildcard at the end of string' {
                _IsWildcard -Item 'Foo/Bar*' | Should Be $true
            }
            It 'Should match an wildcard at both ends of string' {
                _IsWildcard -Item '*Foo/Bar*' | Should Be $true
            }
            It 'Should match an wildcard in the middle of string' {
                _IsWildcard -Item 'Foo*Bar' | Should Be $true
            }
            It 'Should match one character range' {
                _IsWildcard -Item 'Foo[bB]ar' | Should Be $true
            }
            It 'Should match multiple character ranges' {
                _IsWildcard -Item 'F[oO]o[bB]ar' | Should Be $true
            }
            It 'Should match character ranges and wildcards' {
                _IsWildcard -Item '*F[oO]o[bB]ar' | Should Be $true
                _IsWildcard -Item 'F[oO]o[bB]ar*' | Should Be $true
                _IsWildcard -Item '*F[oO]o[bB]ar*' | Should Be $true
            }
        }
    }
}
