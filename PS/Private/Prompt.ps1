Function prompt {
 
    $defaultPsPrompt = "$($ExecutionContext.SessionState.Path.CurrentLocation)$('>' * ($NestedPromptLevel + 1)) "

    $escBgBlue = "$([char]0x1b)[44m"
    $escBgMagenta = "$([char]0x1b)[45m"
    $escBgGray = "$([char]0x1b)[40;1m"

    $escFgGray = "$([char]0x1b)[30;1m"
    $escFgWhite = "$([char]0x1b)[37;1m"
    $escReset = "$([char]0x1b)[40m$([char]0x1b)[0m"

    try {
        $tpc = (Get-TfsTeamProjectCollection -Current)
        $tp = (Get-TfsTeamProject -Current)
        $t = (Get-TfsTeam -Current)

        if (-not $tpc) {
            return "${escBgGray}${escFgGray}[Not connected]$escReset" + [System.Environment]::NewLine + $defaultPsPrompt
        }

        $serverName = $tpc.Uri.Host;
        
        if ($tpc.AuthorizedIdentity.UniqueName) {
            $userName = $tpc.AuthorizedIdentity.UniqueName
        }
        else {
            $userName = $tpc.AuthorizedIdentity.Properties['Account']
        }

        if ($serverName -like '*.visualstudio.com') {
            $tpcName = $serverName.SubString(0, $serverName.IndexOf('.'))
            $promptPrefix = "${escBgBlue}${escFgWhite}[$tpcName.visualstudio.com"
        }
        elseif ($serverName -eq 'dev.azure.com') {
            $tpcName = $tpc.Uri.Segments[1]
            $promptPrefix = "${escBgBlue}${escFgWhite}[dev.azure.com/$tpcName"
        }
        else {
            $promptPrefix = "${escBgMagenta}${escFgWhite}[$($tpc.Uri.Host)"

            if ($tpc) {
                if ($tpc.Name) {
                    $promptPrefix += " > $($tpc.Name)"
                }
                else {
                    $promptPrefix += " > $($tpc.Uri.Segments[-1])"
                }
            }
        }

        if ($tp) {
            $promptPrefix += " > $($tp.Name)"
        }

        if ($t) {
            $promptPrefix += " > $($t.Name)"
        }

        if ($userName) {
            $promptPrefix += " ${escFgGray}(${userName})$escFgWhite"
        }

        $promptPrefix += "]$escReset"
    }
    catch { }

    return $promptPrefix + [System.Environment]::NewLine + $defaultPsPrompt
}