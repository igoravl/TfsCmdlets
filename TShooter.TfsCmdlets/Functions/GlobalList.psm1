Function New-TfsGlobalList
{
    Param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $Name,
    
        [Parameter(Mandatory=$true)] 
        [string[]] 
        $Items,

        [Parameter()]
        [switch]
        $Force,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        [xml] $xml = Export-TfsGlobalList -Collection $Collection

        # Checks whether the global list already exists
        $list = $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")

        if ($list -ne $null)
        {
            if ($Force.IsPresent)
            {
                [void] $list.ParentNode.RemoveChild($list)
                $overwritten = $true
            }
            else
            {
                Throw "Global List $Name already exists. To overwrite an existing list, use the -Force switch."
            }
        }

        # Creates the new list XML element
        $list = $xml.CreateElement("GLOBALLIST")
        $list.SetAttribute("name", $Name)

        # Adds the item elements to the list
        foreach($item in $Items)
        {
            $itemElement = $xml.CreateElement("LISTITEM")
            [void] $itemElement.SetAttribute("value", $item)
            [void]$list.AppendChild($itemElement)
        }

        # Appends the new list to the XML obj
        [void] $xml.DocumentElement.AppendChild($list)

        # Saves the list back to TFS
        Import-TfsGlobalList -Xml $xml -Collection $Collection

        return Get-TfsGlobalList -Name $Name -Collection $Collection
    }
}

Function Set-TfsGlobalList
{
    Param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $Name,
    
		[Parameter(ParameterSetName="Edit list items")]
        [string[]] 
        $Add,

		[Parameter(ParameterSetName="Edit list items")]
        [string[]] 
        $Remove,

		[Parameter(ParameterSetName="Rename list", Mandatory=$true)]
        [string] 
        $NewName,

		[Parameter(ParameterSetName="Edit list items")]
        [switch] 
        $Force,

        [object]
        $Collection
    )

    Process
    {
        [xml] $xml = Export-TfsGlobalList -Name $Name -Collection $Collection

        # Retrieves the list
        $list = $xml.SelectSingleNode("//GLOBALLIST")
        $newList = $false

        if ($list -eq $null)
        {
            if (-not $Force.IsPresent)
            { 
                throw "Global list name $Name is invalid or non-existent. Either check the name or use -Force to create a new list."
            }
            
            # Creates the new list XML element
            $list = $xml.CreateElement("GLOBALLIST")
            [void] $list.SetAttribute("name", $Name)
            [void] $xml.DocumentElement.AppendChild($list)
            $newList = $true
        }

		if ($PSCmdlet.ParameterSetName -eq "Rename list")
		{
			$list.SetAttribute("name", $NewName)
			Import-TfsGlobalList -Xml $xml -Collection $Collection

			Remove-TfsGlobalList -Name $Name -Collection $Collection -Confirm:$false

			return Get-TfsGlobalList -Name $NewName -Collection $Collection
		}


        foreach($item in $Add)
        {
            if (-not $newList)
            {
                # Checks if the element exists (prevents duplicates)
                $existingItem = $list.SelectSingleNode("LISTITEM[@value='$item']")
                if ($existingItem -ne $null) { continue }
            }

            $itemElement = $xml.CreateElement("LISTITEM")
            [void] $itemElement.SetAttribute("value", $item)
            [void]$list.AppendChild($itemElement)
        }
        
        if (-not $newList)
        {
			foreach($item in $Remove)
			{
				$existingItem = $list.SelectSingleNode("LISTITEM[@value='$item']")
				
				if ($existingItem)
				{
					[void]$list.RemoveChild($existingItem)
				}
			}
		}
		        
        # Saves the list back to TFS
        Import-TfsGlobalList -Xml $xml -Collection $Collection

        return Get-TfsGlobalList -Name $Name -Collection $Collection
    }
}

Function Get-TfsGlobalList
{
	[CmdletBinding()]
    Param
    (
        [Parameter()]
        [string] 
        $Name = "*",
    
        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $xml = Export-TfsGlobalList @PSBoundParameters

        foreach($listNode in $xml.SelectNodes("//GLOBALLIST"))
		{
			$list = [PSCustomObject] [ordered] @{
				Name = $listNode.GetAttribute("name")
				Items = @()
			}

			foreach($itemNode in $listNode.SelectNodes("LISTITEM"))
			{
				$list.Items += $itemNode.GetAttribute("value")
			}

			$list
		}
    }
}

Function Remove-TfsGlobalList
{
	[CmdletBinding(ConfirmImpact="High", SupportsShouldProcess=$true)]
    Param
    (
        [Parameter(ValueFromPipelineByPropertyName="Name")]
        [string] 
        $Name = "*",
    
        [Parameter()]
        [object]
        $Collection
    )

	Process
	{
		$tpc = Get-TfsTeamProjectCollection $Collection
		$store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

		$lists = Get-TfsGlobalList -Name $Name -Collection $Collection
		$listsToRemove = @()

		foreach($list in $lists)
		{
			if ($PSCmdlet.ShouldProcess($list.Name, "Remove global list"))
			{
				$listsToRemove += $list
			}
		}

		if ($listsToRemove.Length -eq 0)
		{
			return
		}

		$xml = [xml] "<Package />"

		foreach($list in $listsToRemove)
		{
			$elem = $xml.CreateElement("DestroyGlobalList");
			$elem.SetAttribute("ListName", "*" + $list.Name);
			$elem.SetAttribute("ForceDelete", "true");
			[void]$xml.DocumentElement.AppendChild($elem);
		}

		$returnElem = $null

		$store.SendUpdatePackage($xml.DocumentElement, [ref] $returnElem, $false)
	}
}

Function Import-TfsGlobalList
{
	[CmdletBinding()]
    Param
    (
        [Parameter(Mandatory=$true)]
        [xml] 
        $Xml,
    
        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        [void] $store.ImportGlobalLists($Xml.OuterXml)
    }
}

Function Export-TfsGlobalList
{
	[CmdletBinding()]
	[OutputType([xml])]
    Param
    (
        [Parameter()]
        [string] 
        $Name = "*",
    
        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        $xml = [xml] $store.ExportGlobalLists()

        $procInstr = $xml.CreateProcessingInstruction("xml", 'version="1.0"')

        [void] $xml.InsertBefore($procInstr, $xml.DocumentElement)

        $nodesToRemove = $xml.SelectNodes("//GLOBALLIST") #| ? ([System.Xml.XmlElement]$_).GetAttribute("name") -NotLike $Name

        foreach($node in $nodesToRemove)
        {
			if (([System.Xml.XmlElement]$node).GetAttribute("name") -notlike $Name)
			{
				[void]$xml.DocumentElement.RemoveChild($node)
			}
        }

        return $xml
    }
}
