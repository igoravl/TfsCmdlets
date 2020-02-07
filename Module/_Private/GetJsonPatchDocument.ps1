Function _GetJsonPatchDocument
{
    [CmdletBinding()]
    [OutputType('Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchDocument')]
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

        $jsonOp = New-Object 'Microsoft.VisualStudio.Services.WebApi.Patch.Json.JsonPatchOperation' -Property @{
            Operation = $op.Operation
            From = $op.From
            Path = $op.Path
            Value = $op.Value
        }

        $doc.Add($jsonOp)
    }

    Write-Output -NoEnumerate $doc
}
