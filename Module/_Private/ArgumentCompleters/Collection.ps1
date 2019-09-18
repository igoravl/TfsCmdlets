Register-ArgumentCompleter -ParameterName Collection -Verbose -ScriptBlock {
    
    param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameter)

    if($commandName -notlike '*-Tfs*')
    {
        return
    }

    # if($fakeBoundParameter['Server'])
    # {
    #     $srv = Get-TfsConfigurationServer -Server $fakeBoundParameter['Server'] -Credential $fakeBoundParameter['Server']
    # }
    # elseif((Get-TfsConfigurationServer -Current))
    # {
    #     $srv = Get-TfsConfigurationServer -Current
    # }

    $tpcNames = @()

    if($srv)
    {
        $tpcNames += (Get-TfsTeamProjectCollection -Server $srv | Select-Object -ExpandProperty Name)
    }

    $tpcNames += (Get-TfsRegisteredTeamProjectCollection | Select-Object -ExpandProperty Name | Where-Object { $tpcNames -notcontains $_ })

    return $tpcNames
}
