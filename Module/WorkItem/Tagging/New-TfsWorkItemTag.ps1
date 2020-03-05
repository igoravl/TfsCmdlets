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
Function New-TfsWorkItemTag
{

    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position=0,ValueFromPipeline=$true)]
        [Alias('Name')]
        [string] 
        $Tag,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.Core.WebApi)
    }

    Process
    {
        GET_TEAM_PROJECT($tp,$tpc)

        if(-not $PSCmdlet.ShouldProcess($tp.Name, "Create work item tag '$Tag'"))
        {
            return
        }

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Core.WebApi.TaggingHttpClient' -Collection $tpc

        CALL_ASYNC($client.CreateTagAsync($tp.Guid, $Tag),"Error creating work item tag '$Tag'")

        if($Passthru)
        {
            return $result
        }
    }
}