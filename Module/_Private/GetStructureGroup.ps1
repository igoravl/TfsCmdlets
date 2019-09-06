Function _GetStructureGroup([ref] $StructureGroup)
{
    if($StructureGroup -is [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup])
    {
        _Log "StructureGroup already set to '$StructureGroup'; returning"

        return $StructureGroup
    }

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

    throw "Invalid or missing StructureGroup argument"
}