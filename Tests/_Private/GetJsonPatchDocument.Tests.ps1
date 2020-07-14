. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetJsonPatchDocument' {

        $actual = @(
            [ordered]@{
                Operation = 'Add'
                Path = '/bar'
                From = ''
                Value = 'baz'
            },
            [ordered]@{
                Operation = 'Remove'
                Path = '/bar'
                From = ''
                Value = 'baz'
            }
        )

        $expected = @(
            [ordered]@{
                Operation = 0
                Path = '/bar'
                From = ''
                Value = 'baz'
            },
            [ordered]@{
                Operation = 1
                Path = '/bar'
                From = ''
                Value = 'baz'
            }
        )

        Context 'Unit Tests' {
            It 'Should create a document' {
                _GetJsonPatchDocument -Operations $actual | ConvertTo-Json -Compress | Should Be ($expected | ConvertTo-Json -Compress)
            }
        }
    }
}