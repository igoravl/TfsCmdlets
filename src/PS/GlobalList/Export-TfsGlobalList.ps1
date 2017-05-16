<#

.SYNOPSIS
    Exports the contents of one or more Global Lists to XML.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri
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
    
        [Parameter(ValueFromPipeline=$true)]
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

        $nodesToRemove = $xml.SelectNodes("//GLOBALLIST") #| Where-Object ([System.Xml.XmlElement]$_).GetAttribute("name") -NotLike $Name

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
