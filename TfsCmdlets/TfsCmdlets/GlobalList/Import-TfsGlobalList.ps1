<#

.SYNOPSIS
    Imports one or more Global Lists from XML.

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.Xml.XmlDocument
#>
Function Import-TfsGlobalList
{
    [CmdletBinding()]
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [xml] 
        $Xml,
    
        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        [void] $store.ImportGlobalLists($Xml.OuterXml)
    }
}
