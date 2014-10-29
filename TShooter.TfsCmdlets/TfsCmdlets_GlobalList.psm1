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

