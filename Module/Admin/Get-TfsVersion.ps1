Function Get-TfsVersion
{
    [CmdletBinding()]
    [OutputType('PSCustomObject')]
    Param
    (
        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Process
    {
        GET_COLLECTION($tpc)

        if($tpc.IsHostedServer)
        {
            _Log "Collection is hosted (Azure DevOps Services)"

            $html = (Invoke-TfsRestApi -Path '/' -Collection $tpc)

            if(-not ($html -match '"serviceVersion":"(.+?) \\((.+?)\\)"'))
            {
                _Log "Response does not contain 'serviceVersion' information"

                throw "Azure DevOps Services version not found in response."
            }

            $version = [version] ( "$($Matches[1] -replace "[a-zA-Z]", '').0")
            $longVersion = "$($Matches[1]) ($($Matches[2]))"
            $sprint = $version.Minor
            $friendlyVersion = "Azure DevOps Services, Sprint $sprint ($($Matches[2]))"

        }
        else
        {
            _Log "Collection is not hosted (Azure DevOps Server / TFS)"
            throw "Unsupported server version"
        }

        return [PSCustomObject][Ordered] @{
            Version = $version
            LongVersion = $longVersion
            FriendlyVersion = $friendlyVersion
            IsHosted = $tpc.IsHostedServer
            Sprint = $sprint
            Update = $update
        }
    }
}