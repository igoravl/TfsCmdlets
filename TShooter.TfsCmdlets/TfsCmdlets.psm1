Add-Type -AssemblyName 'Microsoft.TeamFoundation.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Common, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.WorkItemTracking.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

# =================
# Helper Functions
# =================

Function _GetUrl
{
    param
    (
        [string] $baseUri,
        [string] $relativeUri
    )

    Process
    {
        $fixedBase = ?: {$baseUri.EndsWith('/')} {$baseUri} {"$baseUri/"}
        New-Object Uri ([Uri] $fixedBase), "$relativeUri"
    }
}


Function _InvokeTernary
{ 
    param
    (
        [Parameter(Mandatory, Position=0)]
        [scriptblock]
        $Condition,

        [Parameter(Mandatory, Position=1)]
        [scriptblock]
        $TrueBlock,

        [Parameter(Mandatory, Position=2)]
        [scriptblock]
        $FalseBlock,

        [Parameter(ValueFromPipeline, ParameterSetName='InputObject')]
        [psobject]
        $InputObject
    ) 
        
    Process { 
        if ($pscmdlet.ParameterSetName -eq 'InputObject') {
            Foreach-Object $Condition -input $InputObject | ForEach-Object { 
                if ($_) { 
                    Foreach-Object $TrueBlock -InputObject $InputObject 
                }
                else {
                    Foreach-Object $FalseBlock -InputObject $InputObject 
                }
            }
        }
        elseif (&$Condition) {
            &$TrueBlock
        } 
        else {
            &$FalseBlock
        }
    }
}

Function _InvokeGenericMethod
{
    param
    (
        [object] 
        $instance = $(throw "Please provide an instance on which to invoke the generic method"),
    
        [string] 
        $methodName = $(throw "Please provide a method name to invoke"),
    
        [string[]] 
        $typeParameters = $(throw "Please specify the type parameters"),
    
        [object[]] 
        $methodParameters = $(throw "Please specify the method parameters")
    ) 

    Process
    {
        ## Determine if the types in $set1 match the types in $set2, replacing generic
        ## parameters in $set1 with the types in $genericTypes
        Function _ParameterTypesMatch([type[]] $set1, [type[]] $set2, [type[]] $genericTypes)
        {
            $typeReplacementIndex = 0
            $currentTypeIndex = 0

            ## Exit if the set lengths are different
            if($set1.Count -ne $set2.Count)
            {
                return $false
            }

            ## Go through each of the types in the first set
            foreach($type in $set1)
            {
                ## If it is a generic parameter, then replace it with a type from
                ## the $genericTypes list
                if($type.IsGenericParameter)
                {
                    $type = $genericTypes[$typeReplacementIndex]
                    $typeReplacementIndex++
                }

                ## Check that the current type (i.e.: the original type, or replacement
                ## generic type) matches the type from $set2
                if($type -ne $set2[$currentTypeIndex])
                {
                    return $false
                }
                $currentTypeIndex++
            }

            return $true
        }

        ## Convert the type parameters into actual types
        [type[]] $typedParameters = $typeParameters

        ## Determine the type that we will call the generic method on. Initially, assume
        ## that it is actually a type itself.
        $type = $instance

        ## If it is not, then it is a real object, and we can call its GetType() method
        if($instance -isnot "Type")
        {
            $type = $instance.GetType()
        }

        ## Search for the method that:
        ##    - has the same name
        ##    - is public
        ##    - is a generic method
        ##    - has the same parameter types
        foreach($method in $type.GetMethods())
        {
            # Write-Host $method.Name
            if(($method.Name -eq $methodName) -and
            ($method.IsPublic) -and
            ($method.IsGenericMethod))
            {
                $parameterTypes = @($method.GetParameters() | % { $_.ParameterType })
                $methodParameterTypes = @($methodParameters | % { $_.GetType() })
                if(_ParameterTypesMatch $parameterTypes $methodParameterTypes $typedParameters)
                {
                    ## Create a closed representation of it
                    $newMethod = $method.MakeGenericMethod($typedParameters)

                    ## Invoke the method
                    $newMethod.Invoke($instance, $methodParameters)

                    return
                }
            }
        }

        ## Return an error if we couldn't find that method
        throw "Could not find method $methodName"
    }
}

Function _GetConnection
{
    param
    (
        [Parameter()]  
        [string]
        $CollectionUrl,
    
        [Parameter()] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter()] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter(Mandatory=$true)]  
        [string]
        $ParameterSetName
    )

    Process
    {
        if (($ParameterSetName -eq "Connected") -and (!$Global:TfsConnection))
        {
            throw "You are trying to run a TFS cmdlet without connection information. Either use Connect-TfsTeamProjectCollection or the parameters -CollectionUrl, -Credential and/or -UseDefaultCredentials"
        }

        if ($Global:TfsConnection)
        {
            return $Global:TfsConnection
        }
        else
        {
            if ($UseDefaultCredentials.IsPresent)
            {
                return Get-TeamProjectCollection $CollectionUrl -UseDefaultCredentials 
            }
            else
            {
                return Get-TeamProjectCollection $CollectionUrl -Credential $Credential
            }
        }
    }
}

Function _GetRestConnection
{
    param
    (
        [Parameter()]  
        [string]
        $CollectionUrl,
    
        [Parameter()] 
        [switch] 
        $UseDefaultCredentials,
    
        [Parameter()] 
        [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
        $Credential,

        [Parameter(Mandatory=$true)]  
        [string]
        $ParameterSetName
    )

    Process
    {
        if (($ParameterSetName -eq "Connected") -and (!$Global:TfsConnection))
        {
            throw "You are trying to run a TFS cmdlet without connection information. Either use Connect-TfsTeamProjectCollection or the parameters -CollectionUrl, -Credential and/or -UseDefaultCredentials"
        }

        if ($Global:TfsConnectionUrl)
        {
            $connection = New-Object -TypeName PSObject
            $connection | Add-Member -Name "Url" -Value $Global:TfsConnectionUrl -MemberType NoteProperty
            $connection | Add-Member -Name "Credential" -Value $Global:TfsConnectionCredential -MemberType NoteProperty
            $connection | Add-Member -Name "UseDefaultCredentials" -Value $Global:TfsConnectionUseDefaultCredentials -MemberType NoteProperty
        }
        else
        {
            $connection = New-Object -TypeName PSObject
            $connection | Add-Member -Name "Url" -Value $CollectionUrl -MemberType NoteProperty
            $connection | Add-Member -Name "Credential" -Value $Credential -MemberType NoteProperty
            $connection | Add-Member -Name "UseDefaultCredentials" -Value $UseDefaultCredentials -MemberType NoteProperty
        }

        return $connection
    }
}

Set-Alias ?: _InvokeTernary
