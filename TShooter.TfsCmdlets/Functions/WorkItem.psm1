#===================================
# Work Item Type cmdlets
#===================================

Function Get-TfsWorkItemTypeDefinition
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$ProjectName,
    
		[Parameter(ValueFromPipeline=$true)] [string] 
		$Name,

		[switch] 
		$UseDefaultCredentials,
    
		[switch] 
		$IncludeGlobalLists,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TfsTeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		$store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

		$project = $store.Projects[$ProjectName]

		foreach($witd in $project.WorkItemTypes)
		{ 
			if (($Name -eq "") -or ($witd.Name -eq $Name))
			{
				 return $witd
			}
		}
	}
}

