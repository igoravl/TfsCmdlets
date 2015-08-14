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
