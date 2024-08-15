& "$(($PSScriptRoot -split '_Tests')[0])/_Tests/_TestSetup.ps1"

Describe (($MyInvocation.MyCommand.Name -split '\.')[-3]) {

    Context '__AllParameterSets' {
        # Get-TfsProcessFieldDefinition
        # [[-Field] <Object>]
        # [-ReferenceName <string>]
        # [-Project <Object>]
        # [-IncludeExtensionFields]
        # [-IncludeDeleted]
        # [-Collection <Object>] # Pipeline input
        # [-Server <Object>] [<CommonParameters>]

        
    } 
}