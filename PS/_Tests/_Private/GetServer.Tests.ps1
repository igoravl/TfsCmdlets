. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetServer' {

        Mock Get-TfsConfigurationServer -ParameterFilter { $Server -eq 'https://foo/bar' } -MockWith { return 'foo-bar' }
        Mock Get-TfsConfigurationServer -ParameterFilter { $Server -eq 'https://foo/bar/baz' } -MockWith { return 'foo-bar-baz' }
        Mock Get-TfsConfigurationServer -ParameterFilter { $Server -eq 'https://foo/bar/null' } -MockWith { return $null }
        Mock Get-TfsConfigurationServer -ParameterFilter { $Server -eq 'https://foo/bar/*' } -MockWith { return @('foo', 'bar') }

        Function CallerImplicit {

            $Server = 'https://foo/bar'
            $srv = $null
            $srv2 = _GetServer

            return @{implicit = $srv; Passthru = $srv2}
        }

        Function CallerPassthru {

            $Server = 'https://foo/bar'
            $srv = $null
            $srv2 = _GetServer -Passthru

            return @{implicit = $srv; Passthru = $srv2}
        }

        Function CallerExplicit {

            $Server = 'https://foo/bar'
            $srv = $null
            $srv2 = _GetServer -Server 'https://foo/bar/baz'

            return @{implicit = $srv; Passthru = $srv2}
        }

        Function CallerExplicitPassthru {

            $Server = 'https://foo/bar'
            $srv = $null
            $srv2 = _GetServer -Server 'https://foo/bar/baz' -Passthru

            return @{implicit = $srv; Passthru = $srv2}
        }

        Context 'When using implicit variables' {

            It 'Should get from and set variables in caller scope' {

                $actual = CallerImplicit

                $actual.Implicit | Should Be 'foo-bar'
                $actual.Passthru | Should BeNullOrEmpty
            }

            It 'Should get from variables in caller scope and return with Passthru' {

                $actual = CallerPassthru

                $actual.Implicit | Should BeNullOrEmpty
                $actual.Passthru | Should Be 'foo-bar'
            }
        }

        Context 'When using explicit variables' {

            It 'Should ignore variables in caller scope and use parameters instead' {

                $actual = CallerExplicit

                $actual.Implicit | Should Be 'foo-bar-baz'
                $actual.Passthru | Should BeNullOrEmpty
            }

            It 'Should ignore variables in caller scope and return with Passthru' {

                $actual = CallerExplicitPassthru

                $actual.Implicit | Should BeNullOrEmpty
                $actual.Passthru | Should Be 'foo-bar-baz'
            }
        }

        Context 'When specifying an invalid server' {

            It 'Should throw' {
                { _GetServer -Server 'https://foo/bar/null' } | Should Throw
                { _GetServer -Server 'https://foo/bar/*' } | Should Throw
            }
        }
    }
}
