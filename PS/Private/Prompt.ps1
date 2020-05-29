Function prompt {
 
    $defaultPsPrompt = "$($ExecutionContext.SessionState.Path.CurrentLocation)$('>' * ($NestedPromptLevel + 1)) "
    $backColor = 'DarkGray'
    $foreColor = 'White'

    try {
        $tpc = (Get-TfsTeamProjectCollection -Current)
        $tp = (Get-TfsTeamProject -Current)
        $t = (Get-TfsTeam -Current)

        if (-not $tpc) {
            Write-Host -Object '[Not connected]' -ForegroundColor $foreColor -BackgroundColor $backColor
            return $defaultPsPrompt
        }

        $serverName = $tpc.Uri.Host;
        
        if($tpc.AuthorizedIdentity.UniqueName)
        {
            $userName = $tpc.AuthorizedIdentity.UniqueName
        }
        else
        {
            $userName = $tpc.AuthorizedIdentity.Properties['Account']
        }

        if ($serverName -like '*.visualstudio.com') {
            $tpcName = $serverName.SubString(0, $serverName.IndexOf('.'))
            $promptPrefix = "[AzDev: $tpcName"
            $backColor = 'DarkBlue'
            $foreColor = 'White'
        }
        elseif ($serverName -eq 'dev.azure.com') {
            $tpcName = $tpc.Uri.Segments[1]
            $promptPrefix = "[AzDev: $tpcName"
            $backColor = 'DarkBlue'
            $foreColor = 'White'
        }
        else {
            $promptPrefix = "[TFS: $($tpc.Uri.Host)"
            $backColor = 'DarkMagenta'
            $foreColor = 'White'

            if ($tpc) {
                $promptPrefix += " > $($tpc.Name)"
            }
        }

        if ($tp) {
            $promptPrefix += " > $($tp.Name)"
        }

        if ($t) {
            $promptPrefix += " > $($t.Name)"
        }

        if ($userName) {
            $promptPrefix += " ($userName)"
        }

        $promptPrefix += ']'

        Write-Host -Object $promptPrefix -ForegroundColor $foreColor -BackgroundColor $backColor # -NoNewline

    }
    catch {
        Write-Warning $_
    }

    return $defaultPsPrompt
}