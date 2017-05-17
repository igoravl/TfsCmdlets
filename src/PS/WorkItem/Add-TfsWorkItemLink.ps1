<#
.SYNOPSIS
Add a link between two work items

.DESCRIPTION
Add-TfsWorkItemLink permits the creation of a link between two work items. By linking work items and other objects, you can track related work, dependencies, and changes made over time. All links are defined with a specific link type. For example, you can use Parent/Child links to link work items to support a hierarchical tree structure. 

.PARAMETER SourceWorkItem
The first work item (the "leftmost") in a link relationship. For instance, in a Parent-Child relationship, that would be the Parent.

.PARAMETER TargetWorkItem
The second work item (the "rightmost") in a link relationship. For instance, in a Parent-Child relationship, that would be the Child.

.PARAMETER EndLinkType
Determines the kind of link to be created. This argument always refer to the Target (or "end") work item, in order to define directionality. In other words, to create a Parent-Child between a Source (parent) and a Target (child) work items, EndLinkType would be "Child". To learn more about the supported link types, see Get-TfsWorkItemLinkEndType.

.PARAMETER Comment
Add a comment to a link. Link comments can be seen in a work item form's "Links" tab.

.PARAMETER SkipSave
Leaves the source work item in a "dirty" (unsaved) state, by not calling its Save() method. It is useful for when subsequents changes need to be made to the work item object before saving it. In that case, it is up to the user to later invoke the Save() method on the source work item object to persist the newly created link.

.PARAMETER Project
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Add-TfsWorkItemLink -SourceWorkItem 1 -TargetWorkItem 2 -EndLinkType Child
Creates a parent-child relationship between work items with IDs #1 (source, parent) and #2 (target, child)

.LINK
https://www.visualstudio.com/en-us/docs/work/track/link-work-items-support-traceability

.LINK
Get-TfsWorkItemLinkEndType
#>
Function Add-TfsWorkItemLink
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("Id")]
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
        [Alias("LinkType")]
        [Alias("Type")]
        [object]
        $EndLinkType,

        [Parameter()]
        [string]
        $Comment,

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

        
        if ($EndLinkType -isnot [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd])
        {
            try
            {
                $EndLinkType = $sourceWi.Store.WorkItemLinkTypes.LinkTypeEnds[$EndLinkType]
            }
            catch
            {
                throw "Error retrieving work item link type $EndLinkType`: $_"
            }
        }

        $link = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLink' -ArgumentList $EndLinkType, $targetWi.Id
        $link.Comment = $Comment

        $i = $sourceWi.WorkItemLinks.Add($link)

        if (-not $SkipSave)
        {
            $sourceWi.Save()
        }

        return $sourceWi.WorkItemLinks[$i]        
    }
}
