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
        [Alias('Name')]
        [string]
        $Server = "*"
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.Client)
    }

    Process
    {
        if(($Server -eq "localhost") -or ($Server -eq "."))
        {
            $Server = $env:COMPUTERNAME
        }

        return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetConfigurationServers() | Where-Object Name -Like $Server
    }
}
