& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Integration Tests' {

        It 'Should parse a Azure DevOps REST API-style string' {
            Invoke-TfsRestApi 'GET https://dev.azure.com/{organization}/_apis/projects?api-version=6.1' `
            | ForEach-Object { $_.Name } `
            | Sort-Object `
            | Should -Be @('AgileGit', $tfsProject, 'TfsCmdlets')
        }

        It 'Should call an alternate host' {
            Invoke-TfsRestApi 'GET https://extmgmt.dev.azure.com/{organization}/_apis/extensionmanagement/installedextensions?api-version=6.1' `
            | ForEach-Object { $_.ExtensionName } `
            | Sort-Object `
            | Select-Object -First 3 `
            | Should -Be @('AdvancedSecurity', 'Aex Code Mapper', 'Aex platform')
        }

        It 'Should call multiple alternates hosts in sequence' {
            Invoke-TfsRestApi '/_apis/extensionmanagement/installedextensions' -UseHost 'extmgmt.dev.azure.com' -ApiVersion '6.1' `
            | ForEach-Object { $_.ExtensionName } `
            | Sort-Object `
            | Select-Object -First 3 `
            | Should -Be @('AdvancedSecurity', 'Aex Code Mapper', 'Aex platform')

            Invoke-TfsRestApi 'GET https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/definitions?api-version=6.1' -Project $tfsProject `
            | ForEach-Object { $_.name } `
            | Sort-Object `
            | Should -Be @('PartsUnlimitedE2E', 'TestProject-CD')

            Invoke-TfsRestApi 'GET https://dev.azure.com/{organization}/_apis/projects?api-version=6.1' `
            | ForEach-Object { $_.Name } `
            | Sort-Object `
            | Should -Be @('AgileGit', $tfsProject, 'TfsCmdlets')
        }

        It 'Should support standard parameter-based call' {
            Invoke-TfsRestApi -Path '/_apis/projects?api-version=6.1' -Method 'GET' -ApiVersion '6.1' -RequestContentType 'application/json' -ResponseContentType 'application/json' `
            | ForEach-Object { $_.Name } `
            | Sort-Object `
            | Should -Be @('AgileGit', $tfsProject, 'TfsCmdlets')
        }

        It 'Should support NoAutoUnwrap' {
            $result = Invoke-TfsRestApi -Path '/_apis/projects?api-version=6.1' -Method 'GET' -ApiVersion '6.1' -RequestContentType 'application/json' -ResponseContentType 'application/json' -NoAutoUnwrap
            $result[0].Count | Should -Be 3
        }

        It 'Should support Raw' {
            $result = Invoke-TfsRestApi -Path '/_apis/projects?api-version=6.1' -Method 'GET' -ApiVersion '6.1' -RequestContentType 'application/json' -ResponseContentType 'application/json' -Raw
            $result | Should -BeOfType [string]
            $result | ConvertFrom-Json | Select-Object -ExpandProperty Count | Should -Be 3
        }

    }
}
