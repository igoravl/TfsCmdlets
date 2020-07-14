. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_NormalizeNodePath' {

        Context 'Unit Tests' {
            It 'Should include project' {
                _NormalizeNodePath -Path '\Foo\Bar\Baz' -Project 'Prj' -IncludeTeamProject | Should Be 'Prj\Foo\Bar\Baz'
            }
            It 'Should include scope' {
                _NormalizeNodePath -Path '\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' -IncludeTeamProject -IncludeScope | Should Be 'Prj\MyScope\Foo\Bar\Baz'
            }
            It 'Should include leading separator' {
                _NormalizeNodePath -Path '\Foo\Bar\Baz' -Project 'Prj' -IncludeTeamProject -IncludeLeadingSeparator | Should Be '\Prj\Foo\Bar\Baz'
                _NormalizeNodePath -Path 'Foo\Bar\Baz' -Project 'Prj' -IncludeLeadingSeparator | Should Be '\Foo\Bar\Baz'
            }
            It 'Should include trailing separator' {
                _NormalizeNodePath -Path '\Foo\Bar\Baz' -Project 'Prj' -IncludeTeamProject -IncludeTrailingSeparator | Should Be 'Prj\Foo\Bar\Baz\'
                _NormalizeNodePath -Path 'Foo\Bar\Baz' -Project 'Prj' -IncludeTrailingSeparator | Should Be 'Foo\Bar\Baz\'
            }
            It 'Should include project' {
                _NormalizeNodePath -Path '\Foo\Bar\Baz' -Project 'Prj' -IncludeTeamProject | Should Be 'Prj\Foo\Bar\Baz'
                _NormalizeNodePath -Path 'Foo\Bar\Baz' -Project 'Prj' -IncludeTeamProject | Should Be 'Prj\Foo\Bar\Baz'
            }
            It 'Should strip project' {
                _NormalizeNodePath -Path '\Prj\Foo\Bar\Baz' -Project 'Prj' | Should Be 'Foo\Bar\Baz'
                _NormalizeNodePath -Path 'Prj\Foo\Bar\Baz' -Project 'Prj' | Should Be 'Foo\Bar\Baz'
            }
            It 'Should strip scope' {
                _NormalizeNodePath -Path '\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' | Should Be 'Foo\Bar\Baz'
                _NormalizeNodePath -Path 'MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' | Should Be 'Foo\Bar\Baz'
            }
            It 'Should strip project and scope' {
                _NormalizeNodePath -Path '\Prj\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' | Should Be 'Foo\Bar\Baz'
                _NormalizeNodePath -Path 'Prj\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' | Should Be 'Foo\Bar\Baz'
            }
            It 'Should strip path' {
                _NormalizeNodePath -Path '\Prj\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' -ExcludePath | Should Be ''
                _NormalizeNodePath -Path '\Prj\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' -ExcludePath -IncludeTeamProject | Should Be 'Prj'
                _NormalizeNodePath -Path '\Prj\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' -ExcludePath -IncludeScope | Should Be 'MyScope'
                _NormalizeNodePath -Path '\Prj\MyScope\Foo\Bar\Baz' -Project 'Prj' -Scope 'MyScope' -ExcludePath -IncludeTeamProject -IncludeScope | Should Be 'Prj\MyScope'
            }
            It 'Should coalesce separators' {
                _NormalizeNodePath -Path '\\Foo\\\Bar\\\\Baz' -Project 'Prj' | Should Be 'Foo\Bar\Baz'
                _NormalizeNodePath -Path '/Foo//Bar///Baz' -Project 'Prj' | Should Be 'Foo\Bar\Baz'
                _NormalizeNodePath -Path '\\Foo//Bar/\Baz' -Project 'Prj' | Should Be 'Foo\Bar\Baz'
            }
        }
    }
}
