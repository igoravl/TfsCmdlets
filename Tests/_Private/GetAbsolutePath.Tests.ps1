. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_GetAbsolutePath' {

        Context 'When supplying an existing path' {

            $path = 'TestDrive:\test\a\b\c'
            $rootPath =  [System.IO.Path]::GetFullPath((Get-PSDrive 'TestDrive').Root)

            New-Item $path -Force -ItemType Directory
            Push-Location "$rootPath\test\a\b\c"

            It 'Should be able to traverse up' {
                _GetAbsolutePath '..\foo.bar' | Should Be "$rootPath\test\a\b\foo.bar"
            }

            It 'Should be able to traverse down' {
                _GetAbsolutePath 'foo.bar' | Should Be "$rootPath\test\a\b\c\foo.bar"
            }

            Pop-Location
        }

        Context 'When supplying an non-existing path' {

            $path = 'TestDrive:\test\a\b\c'
            $rootPath =  [System.IO.Path]::GetFullPath((Get-PSDrive 'TestDrive').Root)

            New-Item $path -Force -ItemType Directory
            Push-Location "$rootPath\test\a\b\c"

            It 'Should be able to traverse up' {
                _GetAbsolutePath '..\d\foo.bar' | Should Be "$rootPath\test\a\b\d\foo.bar"
                "TestDrive:\test\a\b\d\foo.bar" | Should Not Exist
            }

            It 'Should be able to traverse down' {
                _GetAbsolutePath 'e\foo.bar' | Should Be "$rootPath\test\a\b\c\e\foo.bar"
                "TestDrive:\test\a\b\c\e\foo.bar" | Should Not Exist
            }

            It 'Should be able to create folder and traverse up' {
                Mock 'New-Item' -ParameterFilter { $Path -eq "$rootPath\test\a\b\d" } -Verifiable
                _GetAbsolutePath '..\d\foo.bar' -CreateFolder | Should Be "$rootPath\test\a\b\d\foo.bar"
                Assert-VerifiableMock
            }

            It 'Should be able to create folder and traverse down' {
                Mock 'New-Item' -ParameterFilter { $Path -eq "$rootPath\test\a\b\c\e" } -Verifiable
                _GetAbsolutePath 'e\foo.bar' -CreateFolder | Should Be "$rootPath\test\a\b\c\e\foo.bar"
                Assert-VerifiableMock
            }

            Pop-Location
        }
    }
}
