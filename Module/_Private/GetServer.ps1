Function _GetServer
{
    [CmdletBinding()]
    Param
    (
        [Parameter()]
        [object]
        $Server,

        [Parameter()]
        [switch]
        $Passthru
    )

    if(-not $Server)
    {
        $Server = Get-Variable -Name 'Server' -Scope 1 -ValueOnly
    }

    $srv = Get-TfsConfigurationServer -Server $Server
    
    if (-not $srv -or ($srv.Count -ne 1))
    {
        throw "Invalid or non-existent server $Server."
    }

    if($Passthru.IsPresent)
    {
        return $srv
    }

    Set-Variable -Name 'srv' -Value $srv -Scope 1
}