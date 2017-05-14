<#

.PARAMETER Credential
    ${HelpParam_Credential}


.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
#>
Function Stop-TfsTeamProjectCollection
{
    [CmdletBinding(ConfirmImpact='High')]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[object] 
		$Collection,

		[Parameter()]
		[string]
		$Reason,
	
		[Parameter()]
		[object] 
		$Server,
	
		[Parameter()]
		[System.Management.Automation.Credential()]
		[System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		throw "Not implemented"
	}
}
