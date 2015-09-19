<#
.SYNOPSIS
	Exports a work item type on XML format.

.PARAMETER Name
	Uses this parameter to filter for an specific Work Item Type.
	If suppress, cmdlet will return all Work Item Types on XML format.

.PARAMETER IncludeGlobalLists
	 Exports the definitions of referenced global lists. If not specified, global list definitions are omitted.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.EXAMPLE
	$xml = Export-TfsWorkItemType -Name Task -Project "MyTeamProject" -IncludeGlobalLists
	This example export a Work Item Type Task of Team Project "MyTeamProject" including the respective GlobalLists.
#>
Function Export-TfsWorkItemType
{
	[CmdletBinding()]
	[OutputType([xml[]])]
	Param
	(
		[Parameter()]
        [SupportsWildcards()]
		[string] 
		$Name = "*",

		[Parameter()]
		[switch]
		$IncludeGlobalLists,

		[Parameter(ValueFromPipeline=$true)]
		[object]
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$types = Get-TfsWorkItemType -Name $Name -Project $Project -Collection $Collection

		foreach($type in $types)
		{
			$type.Export($IncludeGlobalLists)
		}
	}
}
