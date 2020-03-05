#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
<#
.SYNOPSIS

.DESCRIPTION

.EXAMPLE

.INPUTS

.OUTPUTS

.NOTES

#>
Function New-TfsClassificationNode
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('ITEM_TYPE')]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias("Area")]
        [Alias("Iteration")]
        [Alias("Path")]
        [string]
        $Node,

        [Parameter()]
        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
        $StructureGroup,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru,

        [Parameter()]
        [switch]
        $Force
    )

    Process
    {
        GET_STRUCTURE_GROUP

        GET_TEAM_PROJECT($tp,$tpc)

        $Node = _NormalizeNodePath $Node -Project $tp.Name -Scope $StructureGroup

        if(-not $PSCmdlet.ShouldProcess($tp.Name, "Create node '$Node'"))
        {
            return
        }

        $client = Get-TfsRestClient 'Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient' -Collection $tpc

        $parentPath = (Split-Path $Node -Parent)
        $nodeName = (Split-Path $Node -Leaf)

        if(-not (Test-TfsClassificationNode -Node $parentPath -StructureGroup $StructureGroup -Project $Project))
        {
            _Log "Parent node '$parentPath' does not exist"

            if(-not $Force.IsPresent)
            {
                _throw "Parent node '$parentPath' does not exist. Check the path or use -Force the create any missing parent nodes."
            }

            _Log "Creating missing parent path '$parentPath'"

            $PSBoundParameters['Node'] = $parentPath
            $PSBoundParameters['StructureGroup'] = $StructureGroup

            New-TfsClassificationNode @PSBoundParameters
        }

        $patch = New-Object 'ITEM_TYPE' -Property @{
            Name = $nodeName
        }
    
        CALL_ASYNC($client.CreateOrUpdateClassificationNodeAsync($patch, $tp.Name, $structureGroup, $parentPath, "Error creating node $node"))

        if ($Passthru)
        {
            return $node
        }
    }
}

Set-Alias -Name New-TfsArea -Value New-TfsClassificationNode
Set-Alias -Name New-TfsIteration -Value New-TfsClassificationNode