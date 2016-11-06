<#

.SYNOPSIS
    Gets the links of a work item.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Add-TfsWorkItemLink
{
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("id")]
        [Alias("From")]
        [ValidateNotNull()]
        [object]
        $SourceWorkItem,

        [Parameter(Position=1, Mandatory=$true)]
        [Alias("To")]
        [ValidateNotNull()]
        [object]
        $TargetWorkItem,

        [Parameter(Position=2, Mandatory=$true)]
        [object]
        $TargetLinkType,

        [switch]
        $SkipSave,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $sourceWi = Get-TfsWorkItem -WorkItem $SourceWorkItem -Collection $Collection -Project $Project
        $targetWi = Get-TfsWorkItem -WorkItem $TargetWorkItem -Collection $Collection -Project $Project

        
        if ($TargetLinkType -isnot [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd])
        {
            try
            {
                $TargetLinkType = $sourceWi.Store.WorkItemLinkTypes.LinkTypeEnds[$TargetLinkType]
            }
            catch
            {
                throw "Error retrieving work item link type $TargetLinkType`: $_"
            }
        }

        $link = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLink' -ArgumentList $TargetLinkType, $targetWi.Id
        [void] $sourceWi.WorkItemLinks.Add($link)

        if (-not $SkipSave)
        {
            $sourceWi.Save()
        }
    }
}
