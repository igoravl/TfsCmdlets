<#
.SYNOPSIS
	Specifies that a new work item of the type entered be created. The object WorkItem of the new work item is displayed.

.PARAMETER Type
	Represents the name of the work item type to create.

.PARAMETER Title
	Specifies a Title field of new work item type that will be created.

.PARAMETER Fields
	Specifies the fields that are changed and the new values to give to them.
	FieldN The name of a field to update.
	ValueN The value to set on the fieldN.
	[field1=value1[;field2=value2;...]

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	New-TfsWorkItem -Type Task -Title "Task 1" -Project "MyTeamProject"
	This example creates a new Work Item on Team Project "MyTeamProject".
#>
Function New-TfsWorkItem
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])]
	Param
	(
		[Parameter(ValueFromPipeline=$true, Mandatory=$true, Position=0)]
		[object] 
		$Type,

		[Parameter(Position=1)]
		[string]
		$Title,

		[Parameter()]
		[hashtable]
		$Fields,

		[Parameter()]
		[object]
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		$wit = Get-TfsWorkItemType -Type $Type -Project $Project -Collection $Collection

		$wi = $wit.NewWorkItem()

        if ($Title)
        {
		    $wi.Title = $Title
        }

		foreach($field in $Fields)
		{
			$wi.Fields[$field.Key] = $field.Value
		}

		$wi.Save()

		return $wi
	}
}
