Function Prompt
{
    $promptPrefix = '[Not connected]'
    $defaultPsPrompt = "$($executionContext.SessionState.Path.CurrentLocation)$('>' * ($nestedPromptLevel + 1)) "

    $server = (Get-TfsConfigurationServer -Current)
    $tpc = (Get-TfsTeamProjectCollection -Current)
    $tp = (Get-TfsTeamProject -Current)

    if(-not $server)
    {
        return "$promptPrefix $defaultPsPrompt"
    }

    $serverName = $server.Name
    $tpcName = $tpc.Name
    $tpName = $tp.Name

    if ($serverName -like '*.visualstudio.com')
    {
        $tpcName = $serverName.SubString(0, $serverName.IndexOf('.'))
        $promptPrefix = "[AzDev: "
    }
    elseif ($serverName -eq 'dev.azure.com')
    {
        $promptPrefix = "[AzDev: "
    }
    else
    {
        $promptPrefix = "[TFS: $($server.Uri.AbsoluteUri.TrimEnd('/'))/"
    }

    if ($tpcName)
    {
        $promptPrefix += "$($tpc.Name)"
    }

    if ($tpName)
    {
        $promptPrefix += "/$($tp.Name)"
    }

    $promptPrefix += ']'

    return "$promptPrefix $defaultPsPrompt"
}
