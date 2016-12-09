<#

.SYNOPSIS
    Gets one or more Team Foundation Server addresses registered in the current computer.

.INPUTS
    System.String
#>
Function Get-TfsRegisteredConfigurationServer
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.Client.RegisteredConfigurationServer])]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [string]
        $Name = "*"
    )

    Process
    {
        if(($Name -eq "localhost") -or ($Name -eq "."))
        {
            $Name = $env:COMPUTERNAME
        }

        return [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetConfigurationServers() | ? Name -Like $Name
    }
}
