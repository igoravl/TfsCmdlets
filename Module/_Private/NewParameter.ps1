Function _NewParameter
{
    Param
    (
        [Parameter(Position=0, Mandatory=$true)]
        [hashtable]
        $Parameters
    )

    $paramDict = New-Object 'System.Management.Automation.RuntimeDefinedParameterDictionary'

    foreach($paramName in $Parameters.Keys)
    {
        $paramDef = $Parameters[$paramName]
        $paramAttribs = New-Object 'System.Collections.ObjectModel.Collection[System.Attribute]'
        $paramAttribs.Add((New-Object 'System.Management.Automation.ParameterAttribute' -Property $paramDef.ParameterAttribute))
        $paramDef.Attributes | ForEach-Object { $paramAttribs.Add($_) }

        $param = New-Object 'System.Management.Automation.RuntimeDefinedParameter' -ArgumentList $paramName, $paramDef.Type, $paramAttribs

        $paramDict.Add($paramName, $param)
    }

    return $paramDict
}