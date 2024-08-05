& "$($PSScriptRoot.Split('_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Unit Tests' {
        It 'Should get version from hosted service' -Tag 'Hosted' {
            $version = Get-TfsVersion

            $version.Version.Major | Should -BeGreaterOrEqual 18
            $version.Version.Minor | Should -BeGreaterOrEqual 0
            $version.Version.Build | Should -BeGreaterOrEqual 0
            $version.LongVersion | Should -BeLike '*(AzureDevOps_M*'
            $version.IsHosted | Should -BeTrue
        }
    }

}