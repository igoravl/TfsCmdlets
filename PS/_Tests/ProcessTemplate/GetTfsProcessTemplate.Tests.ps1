& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context 'Get by name' {
        # Get-TfsProcessTemplate
        #  [[-ProcessTemplate] <Object>]
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return all processes' {
            $result = Get-TfsProcessTemplate
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Core.WebApi.Process'
            $result.Name | Sort-Object | Should -Be @('Agile', 'Agile TfsCmdlets', 'Agile-TfsCmdlets', 'Basic', 'CMMI', 'Scrum')
        }

        It 'Should support wildcards' {
            $result = Get-TfsProcessTemplate -ProcessTemplate 'Agile*'
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Core.WebApi.Process'
            $result.Name | Sort-Object | Should -Be @('Agile', 'Agile TfsCmdlets', 'Agile-TfsCmdlets')
        }
    }
    
    Context 'Get default process' {
        # Get-TfsProcessTemplate
        #  -Default
        #  [-Collection <Object>]
        #  [-Server <Object>] [<CommonParameters>]

        It 'Should return the default process' {
            $result = Get-TfsProcessTemplate -Default
            $result | Should -BeOfType 'Microsoft.TeamFoundation.Core.WebApi.Process'
            $result.Name | Should -Be 'Agile TfsCmdlets'
        }
    } 
}