function _InvokeGenericMethod
{
    [CmdletBinding(DefaultParameterSetName = 'Instance')]
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'Instance')]
        $InputObject,

        [Parameter(Mandatory = $true, ParameterSetName = 'Static')]
        [Type]
        $Type,

        [Parameter(Mandatory = $true)]
        [string]
        $MethodName,

        [Parameter(Mandatory = $true)]
        [Type[]]
        $GenericType,

        [Object[]]
        $ArgumentList
    )

    process
    {
        switch ($PSCmdlet.ParameterSetName)
        {
            'Instance'
            {
                $_type  = $InputObject.GetType()
                $object = $InputObject
                $flags  = [System.Reflection.BindingFlags] 'Instance, Public'
            }

            'Static'
            {
                $_type  = $Type
                $object = $null
                $flags  = [System.Reflection.BindingFlags] 'Static, Public'
            }
        }

        if ($null -ne $ArgumentList)
        {
            $argList = $ArgumentList.Clone()
        }
        else
        {
            $argList = @()
        }

        $params = @{
            Type         = $_type
            BindingFlags = $flags
            MethodName   = $MethodName
            GenericType  = $GenericType
            ArgumentList = [ref]$argList
        }

        $method = _GetGenericMethod @params

        if ($null -eq $method)
        {
            Write-Error "No matching method was found"
            return
        }

        return [PSGenericMethods.MethodInvoker]::InvokeMethod($method, $object, $argList)
    } 
}

function _GetGenericMethod
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [Type]
        $Type,

        [Parameter(Mandatory = $true)]
        [string]
        $MethodName,

        [Parameter(Mandatory = $true)]
        [Type[]]
        $GenericType,

        [ref]
        $ArgumentList,

        [System.Reflection.BindingFlags]
        $BindingFlags = [System.Reflection.BindingFlags]::Default,

        [switch]
        $WithCoercion
    )

    if ($null -eq $ArgumentList.Value)
    {
        $originalArgList = @()
    }
    else
    {
        $originalArgList = @($ArgumentList.Value)
    }

    foreach ($method in $Type.GetMethods($BindingFlags))
    {
        $argList = $originalArgList.Clone()

        if (-not $method.IsGenericMethod -or $method.Name -ne $MethodName) { continue }
        if ($GenericType.Count -ne $method.GetGenericArguments().Count) { continue }

        if (_TestGenericMethodParameters -MethodInfo $method -ArgumentList ([ref]$argList) -GenericType $GenericType -WithCoercion:$WithCoercion)
        {
            $ArgumentList.Value = $argList
            return $method.MakeGenericMethod($GenericType)
        }
    }

    if (-not $WithCoercion)
    {
        $null = $PSBoundParameters.Remove('WithCoercion')
        return _GetGenericMethod @PSBoundParameters -WithCoercion
    }

}

function _TestGenericMethodParameters
{
    [CmdletBinding()]
    param (
        [System.Reflection.MethodInfo] $MethodInfo,

        [ref]
        $ArgumentList,

        [Parameter(Mandatory = $true)]
        [Type[]]
        $GenericType,

        [switch]
        $WithCoercion
    )

    if ($null -eq $ArgumentList.Value)
    {
        $argList = @()
    }
    else
    {
        $argList = @($ArgumentList.Value)
    }

    $parameterList = $MethodInfo.GetParameters()

    $arrayType = $null

    $_HasParamsArray = _HasParamsArray -ParameterList $parameterList

    if ($parameterList.Count -lt $argList.Count -and -not $_HasParamsArray)
    {
        return $false
    }

    $methodGenericType = $MethodInfo.GetGenericArguments()

    for ($i = 0; $i -lt $argList.Count; $i++)
    {
        $params = @{
            ArgumentList       = $argList
            ParameterList      = $ParameterList
            WithCoercion       = $WithCoercion
            RuntimeGenericType = $GenericType
            MethodGenericType  = $methodGenericType
            Index              = [ref]$i
            ArrayType          = [ref]$arrayType
        }

        $isOk = _TryMatchParameter @params

        if (-not $isOk) { return $false }
    }

    $defaults = New-Object System.Collections.ArrayList

    for ($i = $argList.Count; $i -lt $parameterList.Count; $i++)
    {
        if (-not $parameterList[$i].HasDefaultValue)  { return $false }
        $null = $defaults.Add($parameterList[$i].DefaultValue)
    }

    # When calling a method with a params array using MethodInfo, you have to pass in the array; the
    # params argument approach doesn't work.

    if ($_HasParamsArray)
    {
        $firstArrayIndex = $parameterList.Count - 1
        $lastArrayIndex = $argList.Count - 1

        $newArgList = $argList[0..$firstArrayIndex]
        $newArgList[$firstArrayIndex] = $argList[$firstArrayIndex..$lastArrayIndex] -as $arrayType
        $argList = $newArgList
    }

    $ArgumentList.Value = $argList + $defaults.ToArray()

    return $true

} 

function _TryMatchParameter
{
    param (
        [System.Reflection.ParameterInfo[]]
        $ParameterList,

        [object[]]
        $ArgumentList,

        [Type[]]
        $MethodGenericType,

        [Type[]]
        $RuntimeGenericType,

        [switch]
        $WithCoercion,

        [ref] $Index,
        [ref] $ArrayType
    )

    $params = @{
        ParameterType = $ParameterList[$Index.Value].ParameterType
        RuntimeType   = $RuntimeGenericType
        GenericType   = $MethodGenericType
    }

    $runtimeType = _ResolveRuntimeType @params

    if ($null -eq $runtimeType)
    {
        throw "Could not determine runtime type of parameter '$($ParameterList[$Index.Value].Name)'"
    }

    $_IsParamsArray = _IsParamsArray -ParameterInfo $ParameterList[$Index.Value]

    if ($_IsParamsArray)
    {
        $ArrayType.Value = $runtimeType
        $runtimeType     = $runtimeType.GetElementType()
    }

    do
    {
        $isOk = _TryMatchArgument @PSBoundParameters -RuntimeType $runtimeType
        if (-not $isOk) { return $false }

        if ($_IsParamsArray) { $Index.Value++ }
    }
    while ($_IsParamsArray -and $Index.Value -lt $ArgumentList.Count)

    return $true
}

function _TryMatchArgument
{
    param (
        [System.Reflection.ParameterInfo[]]
        $ParameterList,

        [object[]]
        $ArgumentList,

        [Type[]]
        $MethodGenericType,

        [Type[]]
        $RuntimeGenericType,

        [switch]
        $WithCoercion,

        [ref] $Index,
        [ref] $ArrayType,

        [Type] $RuntimeType
    )

    Function _GetType($object)
    {
        if ($null -eq $object) { return $null }
        return $object.GetType()
    }

    $argValue = $ArgumentList[$Index.Value]
    $argType = _GetType $argValue

    $isByRef = $RuntimeType.IsByRef
    if ($isByRef)
    {
        if ($ArgumentList[$Index.Value] -isnot [ref]) { return $false }

        $RuntimeType = $RuntimeType.GetElementType()
        $argValue = $argValue.Value
        $argType = _GetType $argValue
    }

    $isNullNullable = $false

    while ($RuntimeType.FullName -like 'System.Nullable``1*')
    {
        if ($null -eq $argValue)
        {
            $isNullNullable = $true
            break
        }

        $RuntimeType = $RuntimeType.GetGenericArguments()[0]
    }

    if ($isNullNullable) { continue }

    if ($null -eq $argValue)
    {
        return -not $RuntimeType.IsValueType
    }
    else
    {
        if ($argType -ne $RuntimeType)
        {
            $argValue = $argValue -as $RuntimeType
            if (-not $WithCoercion -or $null -eq $argValue)  { return $false }
        }

        if ($isByRef)
        {
            $ArgumentList[$Index.Value].Value = $argValue
        }
        else
        {
            $ArgumentList[$Index.Value] = $argValue
        }
    }

    return $true
}
function _HasParamsArray([System.Reflection.ParameterInfo[]] $ParameterList)
{
    return $ParameterList.Count -gt 0 -and (_IsParamsArray -ParameterInfo $ParameterList[-1])
}

function _IsParamsArray([System.Reflection.ParameterInfo] $ParameterInfo)
{
    return @($ParameterInfo.GetCustomAttributes([System.ParamArrayAttribute], $true)).Count -gt 0
}

function _ResolveRuntimeType
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [Type]
        $ParameterType,

        [Parameter(Mandatory = $true)]
        [Type[]]
        $RuntimeType,

        [Parameter(Mandatory = $true)]
        [Type[]]
        $GenericType
    )

    if ($ParameterType.IsByRef)
    {
        $elementType = _ResolveRuntimeType -ParameterType $ParameterType.GetElementType() -RuntimeType $RuntimeType -GenericType $GenericType
        return $elementType.MakeByRefType()
    }
    elseif ($ParameterType.IsGenericParameter)
    {
        for ($i = 0; $i -lt $GenericType.Count; $i++)
        {
            if ($ParameterType -eq $GenericType[$i])
            {
                return $RuntimeType[$i]
            }
        }
    }
    elseif ($ParameterType.IsArray)
    {
        $arrayType = $ParameterType
        $elementType = _ResolveRuntimeType -ParameterType $ParameterType.GetElementType() -RuntimeType $RuntimeType -GenericType $GenericType

        if ($ParameterType.GetElementType().IsGenericParameter)
        {
            $arrayRank = $arrayType.GetArrayRank()

            if ($arrayRank -eq 1)
            {
                $arrayType = $elementType.MakeArrayType()
            }
            else
            {
                $arrayType = $elementType.MakeArrayType($arrayRank)
            }
        }

        return $arrayType
    }
    elseif ($ParameterType.ContainsGenericParameters)
    {
        $genericArguments = $ParameterType.GetGenericArguments()
        $runtimeArguments = New-Object System.Collections.ArrayList

        foreach ($argument in $genericArguments)
        {
            $null = $runtimeArguments.Add((_ResolveRuntimeType -ParameterType $argument -RuntimeType $RuntimeType -GenericType $GenericType))
        }

        $definition = $ParameterType
        if (-not $definition.IsGenericTypeDefinition)
        {
            $definition = $definition.GetGenericTypeDefinition()
        }

        return $definition.MakeGenericType($runtimeArguments.ToArray())
    }
    else
    {
        return $ParameterType
    }
}

if (-not ([System.Management.Automation.PSTypeName]'PSGenericMethods.MethodInvoker').Type)
{
	Add-Type -ErrorAction SilentlyContinue -TypeDefinition @'
    namespace PSGenericMethods
    {
        using System;
        using System.Reflection;
        using System.Management.Automation;

        public static class MethodInvoker
        {
            public static object InvokeMethod(MethodInfo method, object target, object[] arguments)
            {
                if (method == null) { throw new ArgumentNullException("method"); }

                object[] args = null;

                if (arguments != null)
                {
                    args = (object[])arguments.Clone();
                    for (int i = 0; i < args.Length; i++)
                    {
                        PSObject pso = args[i] as PSObject;
                        if (pso != null)
                        {
                            args[i] = pso.BaseObject;
                        }

                        PSReference psref = args[i] as PSReference;

                        if (psref != null)
                        {
                            args[i] = psref.Value;
                        }
                    }
                }

                object result = method.Invoke(target, args);

                for (int i = 0; i < arguments.Length; i++)
                {
                    PSReference psref = arguments[i] as PSReference;

                    if (psref != null)
                    {
                        psref.Value = args[i];
                    }
                }

                return result;
            }
        }
    }
'@
}
