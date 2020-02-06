. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_TestGuid' {

        Context 'Unit Tests' {
            It 'Should return true for valid GUID' {
                _TestGuid -Guid ([guid]::NewGuid().ToString()) | Should Be $true
            }
            It 'Should return false for invalid GUID' {
                _TestGuid -Guid 1234 | Should Be $false
                _TestGuid -Guid '000-000-000' | Should Be $false
                _TestGuid -Guid '' | Should Be $false
            }
        }
    }
}
