& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsRestClient
        # [-TypeName] <string>
        # [-Collection <Object>]
        # [-Server <Object>] [<CommonParameters>]

        It 'Should get a REST client' {
            $result = Get-TfsRestClient 'Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient'
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient'
        }
    } 
}