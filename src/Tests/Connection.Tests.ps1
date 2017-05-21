. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe 'Get-TfsInstallationPath' {
        Context 'Integration Tests' {
            It 'Gets Installation Path from VM' {
                Get-TfsInstallationPath -Version 15.0 -Computer 'vsalm.mshome.net' -Credential $defaultVmCreds | Should Be 'C:\Program Files\Microsoft Team Foundation Server 15.0'
            }
        }
    }

}