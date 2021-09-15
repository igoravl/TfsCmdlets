. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetMru' {

        Mock '_GetMruPath' -MockWith { Join-Path 'TestDrive:' 'MyList.json'}

        Context 'When there is are items in the list' {
            $filePath = (Join-Path 'TestDrive:' 'MyList.json')
            $listJson = @('foo', 'bar', 'baz') | ConvertTo-Json -Compress
            $listJson | Set-Content $filePath -Encoding utf8

            $list = _GetMru 'MyList'

            It 'Should return the saved list' {
                ($list | ConvertTo-Json -Compress) | Should Be $listJson
            }
        }

        Context 'When the list is empty' {

            It 'Should return an empty list' {
                $list = _GetMru 'MyList'
                $list.Count | Should Be 0
            }
        }
    }
}
