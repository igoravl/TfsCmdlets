Function _TfsCmdletsPrompt
{
    $promptPrefix = '[Not connected]'
    $defaultPsPrompt = " $($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "

    $server = (Get-TfsConfigurationServer -Current)

    if(-not $server)
    {
        return "$promptPrefix $defaultPsPrompt"
    }

    $tpc = (Get-TfsTeamProjectCollection -Current)
    $tp = (Get-TfsTeamProject -Current)
    $serverName = $server.Name
    $userName = $server.AuthorizedIdentity.UniqueName

    if ($serverName -like '*.visualstudio.com')
    {
        $tpcName = $serverName.SubString(0, $serverName.IndexOf('.'))
        $promptPrefix = "[AzDev:/"
        $backColor = 'DarkBlue'
        $foreColor = 'White'
    }
    elseif ($serverName -eq 'dev.azure.com')
    {
        $promptPrefix = "[AzDev:/"
        $backColor = 'DarkBlue'
        $foreColor = 'White'
    }
    else
    {
        $promptPrefix = "[TFS:/$($server.Uri.Host)/"
        $backColor = 'DarkMagenta'
        $foreColor = 'White'
    }

    if ($tpc)
    {
        $promptPrefix += "$($tpc.Name)"
    }

    if ($tp)
    {
        $promptPrefix += "/$($tp.Name)"
    }

    if($userName)
    {
        $promptPrefix += " ($userName)"
    }

    $promptPrefix += ']'

    Write-Host $promptPrefix -NoNewline -ForegroundColor $foreColor -BackgroundColor $backColor

    return $defaultPsPrompt
}
