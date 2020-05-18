#define ITEM_TYPE Microsoft.TeamFoundation.Core.WebApi.TeamProject
<#
.SYNOPSIS
Gets information about one or more team projects. 

.DESCRIPTION
The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of Microsoft.TeamFoundation.WorkItemTracking.Client.Project) from the supplied Team Project Collection.

.PARAMETER Project
Specifies the name of a Team Project. Wildcards are supported.

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.NOTES
As with most cmdlets in the TfsCmdlets module, this cmdlet requires a TfsTeamProjectCollection object to be provided via the -Collection argument. If absent, it will default to the connection opened by Connect-TfsTeamProjectCollection.

#>
Function Get-TfsTeamProject
{
    [CmdletBinding(DefaultParameterSetName = 'Get by project')]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Position = 0, ParameterSetName = 'Get by project')]
        [object] 
        $Project = '*',

        [Parameter(ValueFromPipeline = $true, Position = 1, ParameterSetName = 'Get by project')]
        [object]
        $Collection,

        [Parameter(Mandatory = $true, ParameterSetName = "Get current")]
        [switch]
        $Current
    )

    Begin
    {
        _Requires 'Microsoft.TeamFoundation.Core.WebApi'
    }

    Process
    {
        if ($Current)
        {
            return [TfsCmdlets.CurrentConnections]::Project
        }

        CHECK_ITEM($Project)

        GET_COLLECTION($tpc)

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient' -Collection $tpc

        if ((_TestGuid $Project) -or (-not (_IsWildcard $Project)))
        {
            CALL_ASYNC($client.GetProject([string] $Project, $true), "Error getting team project '$Project'")

            return $result
        }

        CALL_ASYNC($client.GetProjects(), "Error getting team project(s) matching '$Project'")

        $result | Where-Object Name -like $Project | ForEach-Object {
            CALL_ASYNC($client.GetProject($_.Id, $true), "Error getting team project '$($_.Id)'")
            Write-Output $result
        }
    }
}