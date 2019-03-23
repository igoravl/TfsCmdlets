<#

.SYNOPSIS
    Imports a work item type definition to a team project from XML.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    System.Xml.XmlDocument
#>
Function Import-TfsWorkItemType
{
    [CmdletBinding(ConfirmImpact='Medium')]
    Param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [xml] 
        $Xml,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $tp = Get-TfsTeamProject $Project $Collection
        $tp.WorkItemTypes.Import($Xml.OuterXml)
    }
}
