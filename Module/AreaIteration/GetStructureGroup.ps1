Function _GetStructureGroup
{
    _Log "Getting structure group from call stack"
    
    foreach($frame in Get-PSCallStack)
    {
        if ($frame.Command.EndsWith('Area'))
        {
            return [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]::Areas
        }
        elseif ($frame.Command.EndsWith('Iteration'))
        {
            return [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]::Iterations
        }
    }
}