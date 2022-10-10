# Project

Register-ArgumentCompleter -ParameterName Project -Verbose -ScriptBlock {
    
    param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameter)

    if ($commandName -notlike '*-Tfs*') {
        return
    }

    if ($fakeBoundParameter['Collection']) {
        $tpc = Get-TfsTeamProjectCollection -Collection $fakeBoundParameter['Collection'] -Server $fakeBoundParameter['Server']
    }
    elseif ((Get-TfsTeamProjectCollection -Current)) {
        $tpc = (Get-TfsTeamProjectCollection -Current)
    }
    else {
        return
    }
 
    if ($tpc) {
        return Get-TfsTeamProject -Project "$wordToComplete*" -Collection $tpc -Deleted:($commandName -eq 'Undo-TfsTeamProjectRemoval') | Select-Object -ExpandProperty Name | Sort-Object | _EscapeArgumentValue
    }
}
