<#

.SYNOPSIS
    Gets the contents of one or more Global Lists.

.PARAMETER Collection
    HELP_PARAM_COLLECTION

.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
#>
Function Get-TfsGlobalList
{
    [CmdletBinding()]
    [OutputType('PSCustomObject')]
    Param
    (
        [Parameter()]
        [Alias('Name')]
        [SupportsWildcards()]
        [string] 
        $GlobalList = "*",
    
        [Parameter(ValueFromPipeline=$true)]
        [object]
        $Collection
    )

    Begin
    {
        REQUIRES(Microsoft.TeamFoundation.WorkItemTracking.Client)
    }
    
    Process
    {
        $xml = [xml](Export-TfsGlobalList @PSBoundParameters)

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
