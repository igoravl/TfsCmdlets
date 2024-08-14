& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {

        #  Get-TfsArtifact
        #  [[-Artifact] <Object>]
        #  -Feed <Object>
        #  [-IncludeDeleted]
        #  [-IncludeDescription]
        #  [-IncludePrerelease]
        #  [-IncludeDelisted]
        #  [-ProtocolType <string>]
        #  [-Project <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should throw when mandatory parameters are missing' {
            { Get-TfsArtifact -Artifact 'foo' } | Should -Throw
        }

        It 'Should return all artifacts' {
            (Get-TfsArtifact -Feed Default).Name | Sort-Object -Unique | Should -Be @('portable-installer', 'TfsCmdlets')
        }

        It 'Should return from a list of artifacts' {
            (Get-TfsArtifact 'TfsCmdlets', 'portable-installer' -Feed Default).Name | Sort-Object -Unique | Should -Be @('portable-installer', 'TfsCmdlets')
        }

        It 'Should support wildcards' {
            (Get-TfsArtifact 'Tfs*' -Feed Default).Name | Should -Be 'TfsCmdlets'
            (Get-TfsArtifact 'Tfs*', 'p*' -Feed Default).Name | Sort-Object -Unique | Should -Be @('portable-installer', 'TfsCmdlets')
        }

        It 'Should return delisted packages' {
            (Get-TfsArtifact -Feed Default -IncludeDelisted `
            | Where-Object IsListed -eq $false `
            | Select-Object -ExpandProperty Name) `
            | Should -Be 'Microsoft.TeamFoundationServer.Client'
        }

        It 'Should return deleted packages' {
            (Get-TfsArtifact -Feed Default -IncludeDeleted `
            | Where-Object IsDeleted -eq $true `
            | Select-Object -ExpandProperty Name) `
            | Should -Be @('old-portable-installer', 'tfscmdlets-portable')
        }

        It 'Should return prerelease packages' {
            (Get-TfsArtifact -Feed Default -IncludePrerelease `
            | Where-Object IsPreRelease -eq $true `
            | Select-Object -ExpandProperty Name) `
            | Should -Be TfsCmdlets
        }
    } 
}