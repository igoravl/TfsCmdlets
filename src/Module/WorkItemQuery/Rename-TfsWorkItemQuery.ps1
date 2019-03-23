<#

.SYNOPSIS
    Changes the value of a property of an Area.

.PARAMETER Area
    ${HelpParam_Area}

.PARAMETER NewName
    Specifies the new name of the area. Enter only a name, not a path and name. If you enter a path that is different from the path that is specified in the area parameter, Rename-Tfsarea generates an error. To rename and move an item, use the Move-Tfsarea cmdlet.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
#>
Function Rename-TfsWorkItemQuery
{
    [CmdletBinding(ConfirmImpact='Medium')]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.QueryDefinition])]
    Param
    (
        [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$true)]
        [ValidateNotNull()] 
        [Alias("Path")]
        [object]
        $Query,

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [string]
        $Definition,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection,

        [Parameter()]
        [switch]
        $Passthru
    )

    Process
    {
        $result = Set-TfsWorkItemQuery -Query $Query -NewName $NewName -Project $Project -Collection $Collection -Passthru:$Passthru.IsPresent

        if ($Passthru)
        {
            return $result
        }
    }
}
