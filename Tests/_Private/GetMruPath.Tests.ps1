. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetMruPath' {

        Context 'When supplying a list name' {

            $expected = (Join-Path $HOME (Join-Path '.tfscmdlets' 'foo.Mru.json'))
            $actual = _GetMruPath 'foo'

            It 'Should return a file name under HOME directory' {
                $actual | Should Be $expected
            }
        }

        Context 'When not supplying a list name' {

            It 'Should throw' {
                { _GetMruPath '' } | Should Throw
                { _GetMruPath $null } | Should Throw
            }
        }
    }
}
