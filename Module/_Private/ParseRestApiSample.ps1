Function _ParseRestApiSample
{
    Param
    (
        [Parameter(Mandatory=$true)]
        [string]
        $Sample
    )

    $result = [Ordered] @{
        Operation = ''
        Host = ''
        Path = ''
        QueryParameters = @{}
        ApiVersion = ''
        Scope = ''
    }

    if($sample -notmatch '^((GET|POST|PUT|DELETE|PATCH|OPTIONS) )?(https.+)')
    {
        _Log "Input value '$sample' is not a valid code sample / URL"
        return $result
    }

    $url = [uri] $Matches[3]
    
    switch($url.Host)
    {
        { $_ -like '*dev.azure.com' } {
            $startIndex = 0
        }
        { $_ -like '*.visualstudio.com' } {
            $startIndex = 1
        }
        default {
            _Log "Host name '$($url.Host)' is not a service host. Service hosts must match '*dev.azure.com' or '*.visualstudio.com'"
            return $result
        }
    }

    if($Matches.Count -eq 4)
    {
        $result.Operation = $Matches[2]
    }

    $result.Host = $url.Host
    $result.Scope = &{ if ($url.LocalPath.StartsWith('/{organization}')) { return 'Organization' } else { return 'Service'} }
    $result.Path = $url.LocalPath.Replace('/{organization}', '')

    if($url.Query)
    {
        $query = [uri]::UnescapeDataString($url.Query.Substring(1))
        
        foreach($paramString in ($query -split '&'))
        {
            $paramArray = $paramString -split '='

            if(($paramArray[1] -ne "{$($paramArray[0])}"))
            {
                if($paramArray[0] -eq 'api-version')
                {
                    $result.ApiVersion = $paramArray[1]
                }
                else
                {
                    $result.QueryParameters[$paramArray[0]] = $paramArray[1]
                }
            }
        }
    }

    return $result
}