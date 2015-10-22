<#

.SYNOPSIS
    Exports the contents of one or more Global Lists to XML.

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Export-TfsGlobalList
{
    [CmdletBinding()]
    [OutputType([xml])]
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
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        $xml = [xml] $store.ExportGlobalLists()

        $procInstr = $xml.CreateProcessingInstruction("xml", 'version="1.0"')

        [void] $xml.InsertBefore($procInstr, $xml.DocumentElement)

        $nodesToRemove = $xml.SelectNodes("//GLOBALLIST") #| ? ([System.Xml.XmlElement]$_).GetAttribute("name") -NotLike $Name

        foreach($node in $nodesToRemove)
        {
            if (([System.Xml.XmlElement]$node).GetAttribute("name") -notlike $Name)
            {
                [void]$xml.DocumentElement.RemoveChild($node)
            }
        }

        return $xml
    }
}
