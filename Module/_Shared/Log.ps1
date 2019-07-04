Function _Log
{
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Message,

        [Parameter()]
        [string]
        $Caller,

        [Parameter()]
        [switch]
        $Force
    )

    if(($VerbosePreference -ne 'Continue') -and (-not $Force.IsPresent))
    {
        # No verbose set. Exit now to avoid expensive/unnecessary calls to Get-PSCallStack and Write-Verbose
        return
    }

    if(-not $Caller)
    {
        $caller = _GetLogCallStack
    }

    Write-Verbose "[$caller] $Message"
}

Function _GetLogCallStack
{
    $cs = [System.Collections.Stack]::new()

    foreach($frame in Get-PSCallStack)
    {
        if ($frame.Command -in @('_Log', '_GetLogCallStack', '', $null))
        {
            continue
        }
        
        $cs.Push($frame.Command.Trim('_'))

        if ($frame.Command -like '*-*')
        {
            break
        }
    }

    return $cs.ToArray() -join ':'
}

Function _DumpObj
{
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [object]
        $InputObject,

        [Parameter()]
        $Depth = 5
    )

    return $InputObject | ConvertTo-Json -Depth $Depth -Compress
}