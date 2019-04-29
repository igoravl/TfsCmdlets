<#
.SYNOPSIS
Imports one or more Global Lists from an XML document

.DESCRIPTION
This cmdletsimports an XML containing one or more global lists and their respective items, in the same format used by witadmin. It is functionally equivalent to 'witadmin importgloballist'

.PARAMETER InputObject
XML document object containing one or more global list definitions

.PARAMETER Collection
HELP_PARAM_COLLECTION

.INPUTS
System.Xml.XmlDocument

.EXAMPLE
Get-Content gl.xml | Import-GlobalList
Imports the contents of an XML document called gl.xml to the current project collection

.NOTES
To import global lists, you must be a member of the Project Collection Administrators security group
#>
Function Import-TfsGlobalList
{
    [CmdletBinding(ConfirmImpact='Medium')]
    Param
    (
        [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [Alias("Xml")]
        [object] 
        $InputObject,

        [Parameter()]
        [switch]
        $Force,
    
        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tpc = Get-TfsTeamProjectCollection $Collection
        $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

        if ($InputObject -is [xml])
        {
            $doc = $InputObject.OuterXml
        }
        else
        {
            $doc = $InputObject
        }

        if (-not $Force)
        {
            $existingLists = Get-TfsGlobalList -Collection $tpc
            $listsInXml = ([xml]($InputObject)).SelectNodes('//*/@name')."#text"

            foreach($list in $existingLists)
            {
                if ($list.Name -in $listsInXml)
                {
                    Throw "Global List '$($list.Name)' already exists. To overwrite an existing list, use the -Force switch."
                }
            }
        }

        [void] $store.ImportGlobalLists($doc)
    }
}
