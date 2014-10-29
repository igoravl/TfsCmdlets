Add-Type -AssemblyName 'Microsoft.TeamFoundation.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.Common, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Add-Type -AssemblyName 'Microsoft.TeamFoundation.WorkItemTracking.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

#=====================
# Team cmdlets
#=====================

Function New-Team
{
	param
	(
		[Parameter(ParameterSetName="Not connected",Mandatory=$true)]
		[string] 
		$CollectionUrl = $null,
    
		[Parameter(ParameterSetName="Not connected",Mandatory=$true)]
		[Parameter(ParameterSetName="Connected",Mandatory=$true)]
		[string] 
		$ProjectName,
    
		[Parameter(ParameterSetName="Not connected",Mandatory=$true, ValueFromPipeline=$true)]
		[Parameter(ParameterSetName="Connected",Mandatory=$true, ValueFromPipeline=$true)]
		[string] 
		$Name,
    
		[Parameter(ParameterSetName="Not connected")]
		[Parameter(ParameterSetName="Connected")]
		[string] 
		$Description,
    
		[Parameter(ParameterSetName="Not connected")]
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter(ParameterSetName="Not connected")]
		[System.Management.Automation.PSCredential] [System.Management.Automation.Credential()]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = _GetConnection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential -ParameterSetName $pscmdlet.ParameterSetName

		# Get Team Project
		$cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService3")
		$teamProject = $cssService.GetProjectFromName($ProjectName)

		# Create Team
		$teamService = $tpc.GetService([type]"Microsoft.TeamFoundation.Client.TfsTeamService")

		$teamService.CreateTeam($teamProject.Uri, $Name, $Description, $null)
	}
}

#=============================
# Git cmdlets
#=============================

Function New-GitRepository
{
	param
	(
		[Parameter(ParameterSetName="Not connected",Mandatory=$true)]
		[string] 
		$CollectionUrl = $null,
    
		[Parameter(ParameterSetName="Not connected",Mandatory=$true)]
		[Parameter(ParameterSetName="Connected",Mandatory=$true)]
		[string] 
		$ProjectName,
    
		[Parameter(ParameterSetName="Not connected",Mandatory=$true, ValueFromPipeline=$true)]
		[Parameter(ParameterSetName="Connected",Mandatory=$true, ValueFromPipeline=$true)]
		[string] 
		$Name,
    
		[Parameter(ParameterSetName="Not connected")]
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter(ParameterSetName="Not connected")]
		[System.Management.Automation.PSCredential] [System.Management.Automation.Credential()]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$connection = _GetRestConnection  -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential -ParameterSetName $PSCmdlet.ParameterSetName
		$project = Get-TeamProjectInformation -CollectionUrl $connection.Url -Name $ProjectName -UseDefaultCredentials:$connection.UseDefaultCredentials -Credential $connection.Credential

		$id = $project.id
		$apiUrl = _GetUrl $CollectionUrl "_apis/git/repositories?api-version=1.0"
		$body = "{`"name`": `"${Name}`", `"project`": { `"id`": `"${id}`" } }"
	
		if ($connection.UseDefaultCredentials)
		{
			Invoke-RestMethod -Uri $apiUrl -UseDefaultCredentials -Method "POST" -Body $body -ContentType "application/json"
		}
		else
		{
			Invoke-RestMethod -Uri $apiUrl -Credential $Credential -Method "POST" -Body $body -ContentType "application/json"
		}
	}
}

#=================================
# Team Project cmdlets
#=================================

Function Get-TeamProjectInformation
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$Name,
    
		[Parameter()] [switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.PSCredential] [System.Management.Automation.Credential()]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		if ((!$UseDefaultCredentials.IsPresent) -and ($Credential -eq [System.Management.Automation.PSCredential]::Empty)) { $Credential = Get-Credential }

		$apiUrl = _GetUrl $CollectionUrl "_apis/projects/${Name}?api-version=1.0&includeCapabilities=true"
	
		if ($UseDefaultCredentials.IsPresent)
		{
			$json = Invoke-RestMethod -Uri $apiUrl -UseDefaultCredentials -Method "GET"
		}
		else
		{
			$json = Invoke-RestMethod -Uri $apiUrl -Credential $Credential -Method "GET"
		}

		return $json
	}
}

#============================
# Area & Iteration cmdlets
#============================

Function New-Area
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$ProjectName,
    
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
		$Path,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential
		$areaPath = ("\" + $ProjectName + "\Area\" + $Path).Replace("\\", "\")
    
		_NewCssNode -TeamProjectCollection $tpc -Path $areaPath
	}
}

Function New-Iteration
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$ProjectName,
    
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
		$Path,

		[Parameter()] [DateTime]
		$StartDate,
    
		[Parameter()] [DateTime]
		$EndDate,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential
		$iterationPath = ("\" + $ProjectName + "\Iteration\" + $Path).Replace("\\", "\")
    
		$iterationNode = _NewCssNode -TeamProjectCollection $tpc -Path $iterationPath

		if ($StartDate)
		{
			$iterationNode = Set-IterationDates @PSBoundParameters
		}

		return $iterationNode
	}
}

Function Set-IterationDates
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$ProjectName,
    
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
		$Path,

		[Parameter(Mandatory=$true)] [Nullable[DateTime]]
		$StartDate,
    
		[Parameter(Mandatory=$true)] [Nullable[DateTime]]
		$EndDate,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential
		$cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService4")

		$iterationPath = ("\" + $ProjectName + "\Iteration\" + $Path).Replace("\\", "\")
		$iterationNode = _GetCssNode -TeamProjectCollection $tpc -Path $iterationPath

		if (!$iterationNode)
		{
			throw "Invalid iteration path: $Path"
		}

		[void]$cssService.SetIterationDates($iterationNode.Uri, $StartDate, $EndDate)

		return _GetCssNode -TeamProjectCollection $tpc -Path $iterationPath
	}
}

#=========================
# Global List cmdlets
#=========================

Function New-GlobalList
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$Name,
    
		[Parameter(Mandatory=$true)] [string[]] 
		$Items,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		[xml] $xml = Export-GlobalLists -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		# Creates the new list XML element
		$listElement = $xml.CreateElement("GLOBALLIST")
		$listElement.SetAttribute("name", $Name)

		# Adds the item elements to the list
		foreach($item in $Items)
		{
			$itemElement = $xml.CreateElement("LISTITEM")
			$itemElement.SetAttribute("value", $item)
			[void]$listElement.AppendChild($itemElement)
		}

		# Appends the new list to the XML obj
		[void] $xml.DocumentElement.AppendChild($listElement)

		# Saves the list back to TFS
		Import-GlobalLists -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential -Xml $xml

		return $listElement
	}
}

Function Add-GlobalListItem
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$Name,
    
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)] [string] 
		$Item,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		[xml] $xml = Export-GlobalLists -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		# Creates the new list item XML element
		$itemXml = $xml.CreateElement("LISTITEM")
		$itemXml.SetAttribute("value", $Item)

		# Appends the new item to the list
		$list = $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
		[void]$list.AppendChild($itemXml)

		$xml | Format-List * -Force

		# Saves the list back to TFS
		Import-GlobalLists  -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential -Xml $xml
	}
}

Function Get-GlobalList
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$Name,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		[xml] $xml = Export-GlobalLists -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		return $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
	}
}

Function Import-GlobalLists
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)] [xml] 
		$Xml,

		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{

		$tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		$store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

		$store.ImportGlobalLists($Xml.OuterXml)
	}
}

Function Export-GlobalLists
{
	param(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		$store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

		[xml]$xml = $store.ExportGlobalLists()

		$procInstr = $xml.CreateProcessingInstruction("xml", 'version="1.0"')
		[void] $xml.InsertBefore($procInstr, $xml.DocumentElement)

		return $xml
	}
}

#===================================
# Work Item Type cmdlets
#===================================

Function Get-WorkItemTypeDefinition
{
	param
	(
		[Parameter(Mandatory=$true)] [string] 
		$CollectionUrl,
    
		[Parameter(Mandatory=$true)] [string] 
		$ProjectName,
    
		[Parameter(ValueFromPipeline=$true)] [string] 
		$Name,

		[switch] 
		$UseDefaultCredentials,
    
		[switch] 
		$IncludeGlobalLists,
    
		[Parameter()] [ValidateNotNull()] [System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential = [System.Management.Automation.PSCredential]::Empty
	)

	Process
	{
		$tpc = Get-TeamProjectCollection -CollectionUrl $CollectionUrl -UseDefaultCredentials:$UseDefaultCredentials.IsPresent -Credential $Credential

		$store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

		$project = $store.Projects[$ProjectName]

		foreach($witd in $project.WorkItemTypes)
		{ 
			if (($Name -eq "") -or ($witd.Name -eq $Name))
			{
				 return $witd
			}
		}
	}
}

#===========================
# Connection cmdlets
#===========================

Function Get-TeamProjectCollection
{
	param
	(
		[Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
		[Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
		[string]
		$CollectionUrl,
    
		[Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
		[System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		switch($PSCmdlet.ParameterSetName)
		{
			"Windows Integrated Credential"
			{
				$cred = [System.Net.CredentialCache]::DefaultNetworkCredentials
			}
			"Custom Credential"
			{
				$cred = $Credential.GetNetworkCredential()
			}
		}

		return New-Object Microsoft.TeamFoundation.Client.TfsTeamProjectCollection ([Uri] $CollectionUrl), $cred
	}
}

Function Connect-TeamProjectCollection
{
	param
	(
		[Parameter(ParameterSetName="Windows Integrated Credential", Position=0, Mandatory=$true)] 
		[Parameter(ParameterSetName="Custom Credential", Position=0, Mandatory=$true)]  
		[string]
		$CollectionUrl,
    
		[Parameter(ParameterSetName="Windows Integrated Credential", Position=1, Mandatory=$true)] 
		[switch] 
		$UseDefaultCredentials,
    
		[Parameter(ParameterSetName="Custom Credential", Position=1, Mandatory=$true)] 
		[System.Management.Automation.Credential()] [System.Management.Automation.PSCredential]
		$Credential
	)

	Process
	{
		$Global:TfsConnection = Get-TeamProjectCollection @PSBoundParameters
		$Global:TfsConnectionUrl = $CollectionUrl
		$Global:TfsConnectionCredential = $cred
		$Global:TfsConnectionUseDefaultCredentials = $UseDefaultCredentials.IsPresent
	}
}

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

Function _GetCssNode
{
	param
	(
		[Parameter(Mandatory=$true)] [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
		$TeamProjectCollection,

		[Parameter(Mandatory=$true)] [string]
		$Path,

		[Parameter()] [switch]
		$CreateIfMissing
	)

	Process
	{
		$cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")

		try
		{
			$cssService.GetNodeFromPath($Path)
		}
		catch
		{
			if($CreateIfMissing.IsPresent)
			{
				_NewCssNode $TeamProjectCollection $Path
			}
		}
	}
}

Function _NewCssNode
{
	param
	(
		[Parameter(Mandatory=$true)] [Microsoft.TeamFoundation.Client.TfsTeamProjectCollection]
		$TeamProjectCollection,

		[Parameter(Mandatory=$true)] [string]
		$Path
	)

	Process
	{
		$i = $Path.LastIndexOf("\")

		if ($i -eq -1)
		{
			throw "Node Path must be in format '\<Project>\<Area|Iteration>\Node1\Node2\Node-n'"
		}

		$parentPath = $Path.Substring(0, $i)
		$nodeName = $Path.Substring($i+1)
		$cssService = $tpc.GetService([type]"Microsoft.TeamFoundation.Server.ICommonStructureService")

		try
		{
			$parentNode = $cssService.GetNodeFromPath($parentPath)
		}
		catch
		{
			$parentNode = _NewCssNode -TeamProjectCollection $TeamProjectCollection -Path $parentPath
		}

		$nodeUri = $cssService.CreateNode($nodeName, $parentNode.Uri)

		return $cssService.GetNode($nodeUri)
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
