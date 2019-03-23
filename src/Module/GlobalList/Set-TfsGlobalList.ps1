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
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
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
        $xml = [xml] (Export-TfsGlobalList -Name $Name -Collection $Collection)

        # Retrieves the list
        $list = $xml.SelectSingleNode("//GLOBALLIST")
        $newList = $false

        if ($null -eq $list)
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
            if($PSCmdlet.ShouldProcess($Name, "Rename global list to $NewName"))
            {
                $list.SetAttribute("name", $NewName)
                Import-TfsGlobalList -Xml $xml -Collection $Collection
                Remove-TfsGlobalList -Name $Name -Collection $Collection -Confirm:$false
            }
            return Get-TfsGlobalList -Name $NewName -Collection $Collection
        }

        foreach($item in $Add)
        {
            if (-not $newList)
            {
                # Checks if the element exists (prevents duplicates)
                $existingItem = $list.SelectSingleNode("LISTITEM[@value='$item']")
                if ($null -ne $existingItem) { continue }
            }

            if($PSCmdlet.ShouldProcess($Name, "Add item '$item' to global list"))
            {
                $isDirty = $true
                $itemElement = $xml.CreateElement("LISTITEM")
                [void] $itemElement.SetAttribute("value", $item)
                [void]$list.AppendChild($itemElement)
            }
        }
        
        if (-not $newList)
        {
            foreach($item in $Remove)
            {
                $existingItem = $list.SelectSingleNode("LISTITEM[@value='$item']")
                
                if ($existingItem -and $PSCmdlet.ShouldProcess($Name, "Remove item '$item' from global list"))
                {
                    $isDirty = $true
                    [void]$list.RemoveChild($existingItem)
                }
            }
        }
                
        # Saves the list back to TFS
        if($isDirty)
        {
            Import-TfsGlobalList -Xml $xml -Collection $Collection -Force
        }

        return Get-TfsGlobalList -Name $Name -Collection $Collection
    }
}
