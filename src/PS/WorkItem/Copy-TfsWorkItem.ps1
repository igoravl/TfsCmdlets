<#

.SYNOPSIS
    Creates a copy of a work item, optionally changing its type

.PARAMETER Collection
    ${HelpParam_Collection}

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
		[ValidateSet('None', 'Original', 'Copy')]
        [string]
        $Passthru = 'None',

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
