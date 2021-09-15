. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_SetMru' {

        Mock '_GetMruPath' -MockWith { Join-Path 'TestDrive:' 'MyList.json' }
        $filePath = _GetMruPath 'MyList'

        Context 'When supplying an existing item' {

            @('foo', 'bar', 'baz') | ConvertTo-Json -Compress | Set-Content $filePath -Encoding utf8

            _SetMru 'MyList' 'baz'
            
            $actual = (Get-Content $filePath -Encoding utf8 -Raw)
            $expected = (@('baz', 'foo', 'bar') | ConvertTo-Json -Compress) + [System.Environment]::NewLine

            It 'Should reorder the list' {
                 $actual | Should Be $expected
            }
        }

        Context 'When supplying a new item' {

            @('foo', 'bar', 'baz') | ConvertTo-Json -Compress | Set-Content $filePath -Encoding utf8

            _SetMru 'MyList' 'xpto'
            
            $actual = (Get-Content $filePath -Encoding utf8 -Raw)
            $expected = (@('xpto', 'foo', 'bar', 'baz') | ConvertTo-Json -Compress) + [System.Environment]::NewLine

            It 'Should reorder the list' {
                 $actual | Should Be $expected
            }

            It 'Should respect the list limit ' {
                @('foo', 'bar', 'baz') | ConvertTo-Json -Compress | Set-Content $filePath -Encoding utf8

                _SetMru 'MyList' 'xpto' -Limit 3
                
                $actual = (Get-Content $filePath -Encoding utf8 -Raw)
                $expected = (@('xpto', 'foo', 'bar') | ConvertTo-Json -Compress) + [System.Environment]::NewLine
                
                $actual | Should Be $expected
            }
        }
    }
}
