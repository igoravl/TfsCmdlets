<#

.PARAMETER Credential
    ${HelpParam_Credential}

.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
#>
Function Start-TfsTeamProjectCollection
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
	Param
	(
		[Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
		[object] 
		$Collection,

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
		if($PSCmdlet.ShouldProcess($Collection, 'Start team project collection'))
		{
			throw "Not implemented"
		}
	}
}
