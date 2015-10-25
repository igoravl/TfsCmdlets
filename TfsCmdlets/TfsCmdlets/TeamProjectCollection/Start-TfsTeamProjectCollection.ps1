<#

.PARAMETER Credential
    ${HelpParam_Credential}

#>
Function Start-TfsTeamProjectCollection
{
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[object] 
		$Collection,

		[Parameter(ValueFromPipeline=$true)]
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
