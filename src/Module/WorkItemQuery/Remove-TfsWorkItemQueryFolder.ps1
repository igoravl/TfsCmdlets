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
Function Remove-TfsWorkItemQueryFolder
{
    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [Alias("Path")]
        [ValidateNotNull()] 
        [object]
        $Folder,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [switch]
        $Force,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $folder = Get-TfsWorkItemQueryFolder -Folder $Folder -Project $Project -Collection $Collection

        if (($folder.Count -ne 0) -and (-not $Force))
        {
            throw "Folder is not empty. To delete a folder and its contents, use the -Force switch"
        }

        if ($PSCmdlet.ShouldProcess($folder, "Delete work item query folder"))
        {
            $folder.Delete()
            $folder.Project.Hierarchy.Save()
        }
    }
}
