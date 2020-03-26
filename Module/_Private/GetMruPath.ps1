Function _GetMruPath
{
    [CmdletBinding()]
    param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]
        $ListName
    )

    return (Join-Path $HOME (Join-Path '.tfscmdlets' "$ListName.Mru.json"))
}