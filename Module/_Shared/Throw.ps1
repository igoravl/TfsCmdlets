Function _Throw
{
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [object]
        $Message,

        [Parameter(Position=1)]
        [object]
        $Exception
    )

    $caller = (Get-PSCallStack)[1].Command

    if ($Exception)
    {
        $Message += "`r`rAdditional error information: $Exception"
    }

    throw "[$caller] $Message"
}
