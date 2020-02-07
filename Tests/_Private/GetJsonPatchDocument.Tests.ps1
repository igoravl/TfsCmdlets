. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetJsonPatchDocument' {

        $actual = @(
            [ordered]@{
                Operation = 'Add'
                Path = '/bar'
                From = $null
                Value = 'baz'
            },
            [ordered]@{
                Operation = 'Remove'
                Path = '/bar'
                From = $null
                Value = 'baz'
            }
        )

        $expected = @(
            [ordered]@{
                Operation = 0
                Path = '/bar'
                From = $null
                Value = 'baz'
            },
            [ordered]@{
                Operation = 1
                Path = '/bar'
                From = $null
                Value = 'baz'
            }
        )

        Context 'Unit Tests' {
            It 'Should create a document' {
                _GetJsonPatchDocument -Operations $actual | ConvertTo-Json | Should Be ($expected | ConvertTo-Json)
            }
        }
    }
}