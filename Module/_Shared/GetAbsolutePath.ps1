function _GetAbsolutePath 
{
    [CmdletBinding()]
    Param
    (
        $Path, 

        [switch]$CreateFolder
    )

    Process
    {
        $Path = [System.IO.Path]::Combine($pwd.Path, $Path)
        $Path = [System.IO.Path]::GetFullPath($Path)

        $folder = Split-Path $Path -Parent

        if ((-not (Test-Path $folder)) -and ($CreateFolder.IsPresent))
        {
            New-Item $folder -ItemType Directory -Force | _Log
        }

        return $Path
    }
}