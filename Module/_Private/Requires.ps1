Function _Requires
{
    Param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [string[]]
        $Assembly
    )

    foreach($asm in $Assembly)
    {
        Add-Type -AssemblyName $Assembly
    }
}