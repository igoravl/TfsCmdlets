Function Import-TfsGlobalList
{
	[CmdletBinding()]
    Param
    (
        [Parameter(Mandatory=$true)]
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
