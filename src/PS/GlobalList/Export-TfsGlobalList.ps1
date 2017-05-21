<#
.SYNOPSIS
Exports the contents of one or more Global Lists to XML.

.DESCRIPTION
This cmdlets generates an XML containing one or more global lists and their respective items, in the same format used by witadmin. It is functionally equivalent to 'witadmin exportgloballist'

.PARAMETER Name
Specifies the name of the global list to be exported. Wildcards are supported; when used, they result in a single XML containing all the matching global lists.

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.EXAMPLE
Export-TfsGlobalList | Out-File 'gl.xml'
Exports all global lists in the current project collection to a file called gl.xml.

.EXAMPLE
Export-TfsGlobalList -Name 'Builds - *'
Exports all build-related global lists (with names starting with 'Build - ') and return the resulting XML document

.NOTES
To export or list global lists, you must be a member of the Project Collection Valid Users group or have your View collection-level information permission set to Allow.
#>
Function Export-TfsGlobalList
{
    [CmdletBinding()]
    [OutputType([string])]
    Param
    (
        [Parameter(Position=0)]
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

        $nodesToRemove = $xml.SelectNodes("//GLOBALLIST")

        foreach($node in $nodesToRemove)
        {
            if (([System.Xml.XmlElement]$node).GetAttribute("name") -notlike $Name)
            {
                [void]$xml.DocumentElement.RemoveChild($node)
            }
        }

        return $xml.OuterXml
    }
}
