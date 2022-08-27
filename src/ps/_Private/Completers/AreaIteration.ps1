Register-ArgumentCompleter -ParameterName Node -Verbose -ScriptBlock {
    
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
 
    if ($fakeBoundParameter['Project']) {
        $tp = Get-TfsTeamProject -Collection $fakeBoundParameter['Project'] -Collection $tpc
    }
    elseif ((Get-TfsTeamProject -Current)) {
        $tp = (Get-TfsTeamProject -Current)
    }
    else {
        return
    }

    if ($commandName -like '*Area') {
        return Get-TfsArea -Node "\$wordToComplete*" -Project $tp -Collection $tpc | Select-Object -ExpandProperty RelativePath | Sort-Object | _EscapeArgumentValue
    }
    elseif ($commandName -like '*Iteration') {
        return Get-TfsIteration -Node "\$wordToComplete*" -Project $tp -Collection $tpc | Select-Object -ExpandProperty RelativePath | Sort-Object | _EscapeArgumentValue
    }
}
