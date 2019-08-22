#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition
<#
.SYNOPSIS
    Deletes one or more work item tags
.DESCRIPTION
    
.EXAMPLE

.INPUTS
    ITEM_TYPE
    System.String
.NOTES
#>
Function Rename-TfsWorkItemTag
{

    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Position=0,ValueFromPipeline=$true)]
        [Alias('Name')]
        [object] 
        $Tag,

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.Core.WebApi)
    }

    Process
    {
        $tags = Get-TfsWorkItemTag -Tag $Tag -Project $Project -Collection $Collection

        foreach($t in $tags)
        {
            if(-not $PSCmdlet.ShouldProcess($t.Name, "Rename work item tag to [$NewName]"))
            {
                continue
            }

            GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$t.TeamProject)

            GET_CLIENT('Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient')

            CALL_ASYNC($client.UpdateTagAsync($tp.Guid, $t.Id, $NewName, $t.Active),"Error renaming work item tag [$($t.Name)]'")
        }
    }
}