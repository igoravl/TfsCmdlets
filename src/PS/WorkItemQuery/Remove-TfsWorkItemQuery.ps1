<#
.SYNOPSIS
    Deletes one or more work item queries from the specified Team Project..

.PARAMETER Query
	Specifies the path of a saved query. Wildcards are supported.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Remove-TfsWorkItemQuery
{
    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [ValidateNotNull()] 
        [object]
        $Query,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $queries = Get-TfsWorkItemQuery -Query $Query -Project $Project -Collection $Collection

        foreach($q in $queries)
        {
            if ($PSCmdlet.ShouldProcess($q.Path, "Delete Query"))
            {
				$q.Delete()
            }
        }
    }
}
