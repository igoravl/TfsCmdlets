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

# Team

Register-ArgumentCompleter -ParameterName Team -Verbose -ScriptBlock {
    
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

    return Get-TfsTeam -Team "$wordToComplete*" -Project $tp -Collection $tpc | Select-Object -ExpandProperty Name | Sort-Object | _EscapeArgumentValue
}

# Area / Iteration

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

    return Get-TfsArea -Node "\$wordToComplete*" -Project $tp -Collection $tpc | Select-Object -ExpandProperty RelativePath | Sort-Object | _EscapeArgumentValue
}

Function _EscapeArgumentValue {
    [CmdletBinding()]
    param
    (
        [Parameter(ValueFromPipeline = $true)]
        [string]
        $InputObject    
    )

    Process {
        if ($InputObject.Contains(' ') -or $InputObject.Contains("'") -or $InputObject.Contains('"')) {
            $InputObject = "'" + $InputObject.Replace("'", "''") + "'"
        }
        
        return $InputObject
    }
}

