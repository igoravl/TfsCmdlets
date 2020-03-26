Function _GetMru
{
    [CmdletBinding()]
    param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]
        $ListName
    )

    $filePath = _GetMruPath $ListName

    _Log "Loading list '$ListName' from path '$filePath'"

    $list = @()

    if(-not (Test-Path ($filePath)))
    {
        _Log "List '$ListName' does not exist."
    }
    else
    {
        try
        {
            $savedList = (Get-Content -Path $filePath -Encoding utf8 | ConvertFrom-Json)

            _Log "Loading MRU list '$ListName': $($savedList | ConvertTo-Json -Compress)"
            $list = $savedList
        }
        catch
        {
            _Log "Error loading MRU list '$ListName': $($_.Exception)"
        }
    }
    
    return $list
}