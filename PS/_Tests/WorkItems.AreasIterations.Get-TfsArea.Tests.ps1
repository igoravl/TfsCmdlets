. $PSScriptRoot/_TestSetup.ps1

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should throw on parameterless invocation' {
            { Get-TfsArea } | Should -Throw
        }

        It 'Should get areas (recursively)' {
            Get-TfsArea '**' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('PUL', 'PUL-APP', 'PUL-DB')
        }

        # It 'Should get areas non-recursively' {
        #     Write-Host "Project: $tfsProject"
        #     Get-TfsArea -Node '\*\' -Project $tfsProject | Select-Object -ExpandProperty Name | Sort-Object | Should -Be @('PUL', 'PUL-DB')
        # }
    }
}
