Function _Throw
{
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [object]
        $Message,

        [Parameter(Position=1)]
        [object]
        $Exceptions
    )

    $caller = (Get-PSCallStack)[1].Command

    if ($Exceptions)
    {
        $Message += "`n`nAdditional error information: $($Exceptions | ForEach-Object{ "$_"})"
    }

    throw "[$caller] $Message`n`n"
}
