BeforeAll {
    $setupFilePath = (Join-Path $PSCommandPath.Substring(0, $PSCommandPath.IndexOf('_Tests') + 6) '_TestSetup.ps1')
    . $setupFilePath
}

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Unit Tests' {
        It 'Should get connection string from local server' -Tag 'Desktop' {
        }
    }
}