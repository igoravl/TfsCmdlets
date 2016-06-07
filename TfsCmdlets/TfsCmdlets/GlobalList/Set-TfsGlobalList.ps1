<#

.SYNOPSIS
    Changes the name or the contents of a Global List.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.String
#>
Function Set-TfsGlobalList
{
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName='Name')]
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
