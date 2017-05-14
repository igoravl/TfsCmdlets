<#

.SYNOPSIS
    Creates a new Global List.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String / System.String[]
#>
Function New-TfsGlobalList
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType([PSCustomObject])]
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName='Name')]
        [string] 
        $Name,
    
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName='Items')] 
        [string[]] 
        $Items,

        [Parameter()]
        [switch]
        $Force,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
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

        $list =  Get-TfsGlobalList -Name $Name -Collection $Collection

        if ($Passthru)
        {
            return $list
        }        
    }
}
