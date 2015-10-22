<#

.SYNOPSIS
    Gets the contents of one or more Global Lists.

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Get-TfsGlobalList
{
    [CmdletBinding()]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
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
