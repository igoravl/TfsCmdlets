echo $PSScriptRoot
& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Unit Tests' {
        # It 'Should throw on parameterless invocation' -Tag 'Server' {
        #     { Get-Installation-Path } | Should -Throw
        # }
        # It 'Should throw on unsupported version' -Tag 'Server' {
        #     { Get-Installation-Path -Version 10 } | Should -Throw
        # }
        # It 'Should throw on unsupported component name' -Tag 'Server' {
        #     { Get-Installation-Path -Version 15.0 -Component 'Foo' } | Should -Throw
        # }
    }

}