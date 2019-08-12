Function _TestGuid([string]$guid)
{
    if([string]::IsNullOrEmpty($guid))
    {
        return $false
    }

    $parsedGuid = [guid]::Empty

    return [guid]::TryParse($guid, [ref] $parsedGuid)
}