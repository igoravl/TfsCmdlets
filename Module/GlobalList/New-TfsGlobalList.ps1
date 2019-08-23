<#

.SYNOPSIS
    Creates a new Global List.

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
    System.String / System.String[]
#>
Function New-TfsGlobalList
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType('PSCustomObject')]
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipelineByPropertyName='Name')]
        [Alias('Name')]
        [string] 
        $GlobalList,
    
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

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
    }
    
    Process
    {
        [xml] $xml = Export-TfsGlobalList -Collection $Collection

        # Checks whether the global list already exists
        $list = $xml.SelectSingleNode("//GLOBALLIST[@name='$GlobalList']")

        if ($null -ne $list)
        {
            if ($Force.IsPresent)
            {
                if ($PSCmdlet.ShouldProcess($GlobalList, 'Overwrite existing global list'))
                {
                    [void] $list.ParentNode.RemoveChild($list)
                }
            }
            else
            {
                Throw "Global List $GlobalList already exists. To overwrite an existing list, use the -Force switch."
            }
        }

        if($PSCmdlet.ShouldProcess($GlobalList, 'Create global list'))
        {
            # Creates the new list XML element
            $list = $xml.CreateElement("GLOBALLIST")
            $list.SetAttribute("name", $GlobalList)

            # Adds the item elements to the list
            foreach($item in $Items)
            {
                $itemElement = $xml.CreateElement("LISTITEM")
                [void] $itemElement.SetAttribute("value", $item)
                [void]$list.AppendChild($itemElement)
            }

            # Appends the new list to the XML obj
            [void] $xml.DocumentElement.AppendChild($list)

            Import-TfsGlobalList -Xml $xml -Collection $Collection
            $list =  Get-TfsGlobalList -Name $GlobalList -Collection $Collection

            if ($Passthru)
            {
                return $list
            }
        }        
    }
}
