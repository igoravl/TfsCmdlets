<#
.SYNOPSIS
    Gets one or more Team Foundation Server addresses registered in the current computer.

.PARAMETER Name
    Specifies the name of a registered server. When omitted, all registered servers are returned. Wildcards are permitted.

.INPUTS
    System.String
#>
Function Get-TfsRegisteredConfigurationServer
{
    [CmdletBinding()]
    [OutputType('Microsoft.TeamFoundation.Client.RegisteredConfigurationServer' )]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [string]
        $Name = "*"
    )

    Begin
    {
        # Import-RequiredAssembly 'Microsoft.TeamFoundation.Common'
        Import-RequiredAssembly 'Microsoft.TeamFoundation.Client'
    }

    Process
    {
        if(($Name -eq "localhost") -or ($Name -eq "."))
        {
            $Name = $env:COMPUTERNAME
        }

        return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetConfigurationServers() | Where-Object Name -Like $Name
    }
}
