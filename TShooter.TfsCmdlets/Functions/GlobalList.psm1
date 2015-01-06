#=========================
# Global List cmdlets
#=========================

Function New-GlobalList
{
    param
    (
        [Parameter(Mandatory=$true)]
		[string] 
        $Name,
    
        [Parameter(Mandatory=$true)] 
		[string[]] 
        $Items,

		[switch]
		$Force
    )

    Process
    {
        [xml] $xml = Export-GlobalLists

		# Checks whether the global list already exists
        $list = $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")

		if ($list -eq $null)
		{
			# Creates the new list XML element
			$list = $xml.CreateElement("GLOBALLIST")
			$list.SetAttribute("name", $Name)
		}
		else
		{
			if ($Force.IsPresent)
			{
			}
			else
			{
			}
		}


        # Adds the item elements to the list
        foreach($item in $Items)
        {
            $itemElement = $xml.CreateElement("LISTITEM")
            $itemElement.SetAttribute("value", $item)
            [void]$list.AppendChild($itemElement)
        }

        # Appends the new list to the XML obj
        [void] $xml.DocumentElement.AppendChild($list)

        # Saves the list back to TFS
        Import-GlobalLists -Xml $xml

        return $list
    }
}

Function Add-GlobalListItem
{
    param
    (
        [Parameter(Mandatory=$true)]
		[string] 
        $Name,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
		[string] 
        $Item
    )

    Process
    {
        [xml] $xml = Export-GlobalLists

        # Creates the new list item XML element
        $itemXml = $xml.CreateElement("LISTITEM")
        $itemXml.SetAttribute("value", $Item)

        # Appends the new item to the list
        $list = $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
        [void]$list.AppendChild($itemXml)

        # Saves the list back to TFS
        Import-GlobalLists -Xml $xml
    }
}

Function Get-GlobalList
{
    param
    (
        [Parameter(Mandatory=$true)]
		[string] 
        $Name
    )

    Process
    {
        [xml] $xml = Export-GlobalLists

        return $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
    }
}

Function Import-GlobalLists
{
    param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
		[xml] 
        $Xml
    )

	Begin
	{
        $tpc = _GetConnection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
	}

    Process
    {
        $store.ImportGlobalLists($Xml.OuterXml)
    }
}

Function Export-GlobalLists
{
    param
	(
    )

	Begin
	{
        $tpc = _GetConnection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
	}

    Process
    {
        [xml]$xml = $store.ExportGlobalLists()

        $procInstr = $xml.CreateProcessingInstruction("xml", 'version="1.0"')

        [void] $xml.InsertBefore($procInstr, $xml.DocumentElement)

        return $xml
    }
}

