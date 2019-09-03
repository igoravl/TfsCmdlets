#define ITEM_TYPE Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode
Function _TestNode
{
    [CmdletBinding()]
    Param
    (
        [Parameter()]
        [Alias('Area')]
        [Alias('Iteration')]
        [Alias('Path')]
        [SupportsWildcards()]
        [object]
        $Node,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        try
        {
            return [bool] (_GetNode @PSBoundParameters)
        }
        catch
        {
            _Log "Error testing node '$Node': $($_.Exception)"
            
            return $false
        }
    }
}
