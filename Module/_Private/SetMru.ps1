Function _SetMru
{
    [CmdletBinding()]
    param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]
        $ListName,

        [Parameter(Position=1, Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [object]
        $Value,

        [Parameter()]
        [int]
        $Limit = 10
    )

    $filePath = _GetMruPath $ListName

    _Log "Loading list '$ListName' from path '$filePath'"

    $list = [System.Collections.ArrayList]::new()

    _GetMru $ListName | ForEach-Object { [void] $list.Add($_) }

    _Log "Removing '$Value' from the list (if present)"
    
    while($list.Contains($Value))
    {
        [void] $list.Remove($Value)
    }

    _Log "Adding '$Value' to the top of the list."
    
    $list.Insert(0, $Value)

    if($list.Count -gt $Limit)
    {
        _Log "Trimming list to reduce from $($list.Count) items to the specified limit of $Limit"

        $list.RemoveRange($Limit, $list.Count - $Limit)
    }

    $dir = Split-Path $filePath -Parent

    if (-not (Test-Path($dir)))
    {
        [void] (New-Item $dir -ItemType Directory -Force)
    }

    "[$(($list | ForEach-Object {'"' + $_ + '"'}) -Join ',')]" | Set-Content $filePath -Encoding utf8 -Force
}