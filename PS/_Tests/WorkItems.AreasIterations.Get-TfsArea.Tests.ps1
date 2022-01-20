BeforeAll {
    $setupFilePath = (Join-Path $PSCommandPath.Substring(0, $PSCommandPath.IndexOf('_Tests') + 6) '_TestSetup.ps1')
    . $setupFilePath
}

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        BeforeAll  {
            Connect-TfsTeamProjectCollection -Collection $tfsCollectionUrl -PersonalAccessToken $tfsAccessToken
            $PSDefaultParameterValues['*:ErrorAction'] = 'Stop'
        }

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

        AfterAll {
            Disconnect-TfsTeamProjectCollection
        }
    }
}
