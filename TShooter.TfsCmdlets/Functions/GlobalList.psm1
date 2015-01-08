#=========================
# Global List cmdlets
#=========================

Function New-TfsGlobalList
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
        [xml] $xml = Export-TfsGlobalLists

        # Checks whether the global list already exists
        $list = $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
        $overwritten = $false

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
        Import-TfsGlobalLists -Xml $xml

        return [PSCustomObject] @{
            Name = $Name;
            Items = $Items;
            IsOverwritten = $overwritten
        }
    }
}

Function Add-TfsGlobalListItem
{
    param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $Name,
    
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [string[]] 
        $Items,

        [switch]
        $Force
    )

    Process
    {
        [xml] $xml = Export-TfsGlobalLists

        # Retrieves the list
        $list = $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
        $newList = $false

        if ($list -eq $null)
        {
            if (-not $Force.IsPresent)
            { 
                throw "Global list name $Name is invalid or nonexistent"
            }
            
            # Creates the new list XML element
            $list = $xml.CreateElement("GLOBALLIST")
            [void] $list.SetAttribute("name", $Name)
            [void] $xml.DocumentElement.AppendChild($list)
            $newList = $true
        }

        # Adds the item elements to the list
        foreach($item in $Items)
        {
            if (-not $newList)
            {
                # Checks if the element exists (prevents duplicates)
                $existingItem = $list.SelectSingleNode("//GLOBALLIST[@name='$Name']/LISTITEM[@value='$item']")
                if ($existingItem -ne $null) { continue }
            }

            $itemElement = $xml.CreateElement("LISTITEM")
            [void] $itemElement.SetAttribute("value", $item)
            [void]$list.AppendChild($itemElement)
        }
        
        # Saves the list back to TFS
        Import-TfsGlobalLists -Xml $xml

        $return = [PSCustomObject] @{
            Name = $Name;
            Items = @()
            IsNewList = $newList
        }

        $list.SelectNodes("//GLOBALLIST[@name='$Name']/LISTITEM/@value") | foreach { $return.Items += $_.Value };

        return $return
    }
}

Function Get-TfsGlobalList
{
    param
    (
        [Parameter(Mandatory=$true)]
        [string] 
        $Name
    )

    Process
    {
        [xml] $xml = Export-TfsGlobalLists

        return $xml.SelectSingleNode("//GLOBALLIST[@name='$Name']")
    }
}

Function Import-TfsGlobalLists
{
    param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [xml] 
        $Xml
    )

    Begin
    {
        $tpc = Get-TfsTeamProjectCollection -Current
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
    }

    Process
    {
        $store.ImportGlobalLists($Xml.OuterXml)
    }
}

Function Export-TfsGlobalLists
{
    param
    (
    )

    Begin
    {
        $tpc = Get-TfsTeamProjectCollection -Current
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

