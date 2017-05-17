<#
.SYNOPSIS
Creates a copy of a work item, optionally changing its type

.DESCRIPTION
Use this cmdlet to create a copy of a work item (using its latest saved state/revision data) that is of the specified work item type. By default, the copy retains the same type of the original work item, unless the Type argument is specified

.PARAMETER WorkItem
Specifies the work item to be copied. Can be either a work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem

.PARAMETER Type
Specifies the new type for the copy of the original work item. It can be provided as either a string representing the work item type name (e.g. "Bug" or "Task") or an instance of Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType. When an instance of WorkItemType is provided, the team project from where that WorkItemType object was retrieved will be used to define where to copy the work item into, unless the Project argument is specified 

.PARAMETER IncludeAttachments
Includes attachments as part of the copy process. By default, only field values are copied

.PARAMETER IncludeLinks
Includes work item links as part of the copy process. By default, only field values are copied

.PARAMETER Project
Specified the team project where the work item will be copied into. If omitted, the copy will be created in the same team project of the source work item. The value provided to this argument takes precedence over both the source team project and the team project of an instance of WorkItemType provided to the Type argument

.PARAMETER Collection
${HelpParam_Collection}

.PARAMETER SkipSave
Leaves the new work item in a "dirty" (unsaved) state, by not calling its Save() method. It is useful for when subsequents changes need to be made to the work item object before saving it. In that case, it is up to the user to later invoke the Save() method on the new work item object to persist the copy.

.PARAMETER Passthru
Returns the results of the command. It takes one of the following values: Original (returns the original work item), Copy (returns the newly created work item copy) or None.

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem
System.Int32

.EXAMPLE
Copy-TfsWorkItem -WorkItem 123
Creates (and returns) a copy of a work item with ID #123

.EXAMPLE
Get-TfsWorkItem -Filter '[System.WorkItemType] = "Bug"' | Copy-TfsWorkItem -Type Task
Retrieves all work item of Bug type and copy them as Tasks

.LINK
https://msdn.microsoft.com/en-us/library/ff738070.aspx
#>
Function Copy-TfsWorkItem
{
    [CmdletBinding()]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])]
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter()]
        [object] 
        $Type,

        [Parameter()]
        [object] 
        $Project,

        [Parameter()]
        [switch] 
        $IncludeAttachments,

        [Parameter()]
        [switch] 
        $IncludeLinks,

        [Parameter()]
        [switch] 
        $SkipSave,

        [Parameter()]
		[ValidateSet('Original', 'Copy', 'None')]
        [string]
        $Passthru = 'Copy',

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
		$wi = Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection
		#$store = $wi.Store

		if($Type)
		{
			if ($Project)
			{
				$tp = $Project
			}
			else
			{
				$tp = $wi.Project
			}
			$witd = Get-TfsWorkItemType -Type $Type -Project $tp -Collection $wi.Store.TeamProjectCollection
		}
		else
		{
			$witd = $wi.Type
		}

		$flags = [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags]::None

		if ($IncludeAttachments)
		{
			$flags = $flags -bor [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags]::CopyFiles
		}

		if ($IncludeLinks)
		{
			$flags = $flags -bor [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCopyFlags]::CopyLinks
		}

		$copy = $wi.Copy($witd, $flags)

		if(-not $SkipSave)
		{
			$copy.Save()
		}

		if ($Passthru -eq 'Original')
		{
			return $wi
		}
		
		if($Passthru -eq 'Copy')
		{
			return $copy
		}
    }
}
