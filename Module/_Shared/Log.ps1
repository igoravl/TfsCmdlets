Function _Log
{
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Message
    )

    if($VerbosePreference -ne 'Continue')
    {
        # No verbose set. Exit now to avoid expensive/unnecessary calls to Get-PSCallStack and Write-Verbose
        return
    }

	$caller = (Get-PSCallStack)[1].Command

    Write-Verbose "[$caller] $Message"
}
