BeforeAll {
    $setupFilePath = (Join-Path $PSCommandPath.Substring(0, $PSCommandPath.IndexOf('_Tests') + 6) '_TestSetup.ps1')
    . $setupFilePath
}

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