#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
Function Test-TfsClassificationNode
{
    [CmdletBinding()]
    Param
    (
        [Parameter(ValueFromPipeline=$true, ValueFromPipelineByPropertyName=$true)]
        [Alias('Area')]
        [Alias('Iteration')]
        [Alias('Path')]
        [SupportsWildcards()]
        [object]
        $Node,

        [Parameter()]
        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
        $StructureGroup,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        GET_STRUCTURE_GROUP
        
        try
        {
            return (Get-TfsClassificationNode @PSBoundParameters).Count -gt 0
        }
        catch
        {
            _Log "Error testing node '$Node': $($_.Exception)"
            
            return $false
        }
    }
}

Set-Alias -Name Test-TfsArea -Value Test-TfsClassificationNode
Set-Alias -Name Test-TfsIteration -Value Test-TfsClassificationNode