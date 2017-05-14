<#

.SYNOPSIS
    Gets the contents of one or more Global Lists.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
#>
Function Get-TfsGlobalList
{
    [CmdletBinding()]
    [OutputType([PSCustomObject])]
    Param
    (
        [Parameter()]
        [SupportsWildcards()]
        [string] 
        $Name = "*",
    
        [Parameter(ValueFromPipeline=$true)]
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
