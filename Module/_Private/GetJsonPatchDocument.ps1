Function _GetJsonPatchDocument
{
    [CmdletBinding()]
    [OutputType([Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument])]
    Param
    (
        $operations
    )
    
    $doc = New-Object 'Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument'

    foreach($op in $operations)
    {
        if ($op -is [Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchOperation])
        {
            $doc.Add($op)
            continue
        }

        $jsonOp = New-Object 'Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchOperation'
        $jsonOp.Operation = $op.Operation
        $jsonOp.Path = $op.Path
        $jsonOp.Value = $op.Value

        $doc.Add($jsonOp)
    }

    Write-Output -NoEnumerate $doc
}
