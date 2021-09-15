. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_EscapeArgumentValue' {

        Context 'Unit Tests' {
            It 'Should escape when value has spaces' {
                _EscapeArgumentValue -InputObject 'Foo Bar' | Should Be "'Foo Bar'"
            }
            It 'Should escape when value has single quotes' {
                _EscapeArgumentValue -InputObject "Foo 'Bar'" | Should Be "'Foo ''Bar'''"
            }
            It 'Should escape when value has double quotes' {
                _EscapeArgumentValue -InputObject 'Foo "Bar"' | Should Be "'Foo ""Bar""'"
            }
        }
    }
}
