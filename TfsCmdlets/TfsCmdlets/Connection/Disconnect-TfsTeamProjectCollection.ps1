<#
.SYNOPSIS

	Disconnects from the currently connected TFS Team Project Collection

.DESCRIPTION

	The Disconnect-TfsTeamProjectCollection removes the global variable set by Connect-TfsTeamProjectCollection. Therefore, cmdlets relying on a "default collection" as provided by Connect-TfsTeamProjectCollection will no longer work after a call to this cmdlets, unless their -Collection argument is provided or a new call to Connect-TfsTeamProjectCollection is made.

.EXAMPLE

	Disconnect-TfsTeamProjectCollection

	Disconnects from the currently connected TFS Team Project Collection

#>
Function Disconnect-TfsTeamProjectCollection
{
	Process
	{
        Disconnect-TfsTeamProject

        if ($Global:TfsTpcConnection)
        {
		    Remove-Variable -Name TfsTpcConnection -Scope Global
		}
	}
}
