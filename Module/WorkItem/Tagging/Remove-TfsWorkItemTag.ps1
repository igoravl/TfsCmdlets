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
Function Remove-TfsWorkItemTag
{

    [CmdletBinding(ConfirmImpact='High', SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(Position=0,ValueFromPipeline=$true)]
        [SupportsWildcards()]
        [Alias('Name')]
        [object] 
        $Tag = '*',

        [Parameter()]
        [switch]
        $IncludeInactive,

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
            GET_TEAM_PROJECT_FROM_ITEM($tp,$tpc,$t.TeamProject)

            if(-not $PSCmdlet.ShouldProcess($tp.Name, "Delete work item tag [$($t.Name)]"))
            {
                continue
            }

            $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient' -Collection $tpc

            CALL_ASYNC($client.DeleteTagAsync($tp.Guid, $t.Id),"Error deleting work item tag [$($t.Name)]'")
        }
    }
}