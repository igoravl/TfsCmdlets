. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe 'Get-TfsInstallationPath' {

        Context 'Unit Tests' {
            It 'Should throw on parameterless invocation' {
                { Get-Installation-Path } | Should Throw
            }
            It 'Should throw on unsupported version' {
                { Get-Installation-Path -Version 10 } | Should Throw
            }
            It 'Should throw on unsupported component name' {
                { Get-Installation-Path -Version 15.0 -Component 'Foo' } | Should Throw
            }
            It 'Returns installation path from correct registry keys' {
                Mock 'Get-RegistryValue' -ParameterFilter { $Component -eq 'BaseInstallation'}
            }
        }

        Context "Integration Tests" {
            It "Gets Installation Path" {
                Get-TfsInstallationPath -Version 15.0 -Computer $targetHost -Credential $defaultVmCreds | Should Be 'C:\Program Files\Microsoft Team Foundation Server 15.0\'
            }
        }
    }
}