<#
#>
Function Stop-TfsTeamProjectCollection
{
	Param
	(
		[Parameter(Mandatory=$true, Position=0)]
		[object] 
		$Collection,

		[Parameter()]
		[string]
		$Reason,
	
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
