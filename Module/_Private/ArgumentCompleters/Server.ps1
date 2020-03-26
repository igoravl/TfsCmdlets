Register-ArgumentCompleter -ParameterName Server -Verbose -ScriptBlock {
    
    param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameter)

    if($commandName -notlike '*-Tfs*')
    {
        return
    }

    $srvNames = (Get-TfsRegisteredConfigurationServer | Select-Object -ExpandProperty Name)

    return $srvNames
}
