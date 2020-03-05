#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition
<#
.SYNOPSIS
    Gets one or more work item tags
.DESCRIPTION
    
.EXAMPLE

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
.OUTPUTS
    ITEM_TYPE
.NOTES
#>
Function Get-TfsWorkItemTag
{

    [CmdletBinding()]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias('Name')]
        [object] 
        $Tag = '*',

        [Parameter()]
        [switch]
        $IncludeInactive,

        [Parameter(ValueFromPipeline=$true)]
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
        CHECK_ITEM($Tag)

        GET_TEAM_PROJECT($tp,$tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient' -Collection $tpc

        CALL_ASYNC($client.GetTagsAsync($tp.Guid, $IncludeInactive.IsPresent),"Error retrieving work item tag '$Tag'")

        return $result | Where-Object Name -like $Tag | Add-Member -Name TeamProject -MemberType NoteProperty -Value $TP.Name -PassThru
    }
}