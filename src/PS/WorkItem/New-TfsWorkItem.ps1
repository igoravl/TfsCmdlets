<#

.SYNOPSIS
    Creates a new work item in a team project.

.PARAMETER Type
    Represents the name of the work item type to create.

.PARAMETER Title
    Specifies a Title field of new work item type that will be created.

.PARAMETER Fields
    Specifies the fields that are changed and the new values to give to them.
    FieldN The name of a field to update.
    ValueN The value to set on the fieldN.
    [field1=value1[;field2=value2;...]

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.EXAMPLE
    New-TfsWorkItem -Type Task -Title "Task 1" -Project "MyTeamProject"
    This example creates a new Work Item on Team Project "MyTeamProject".

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType
    System.String    
#>
Function New-TfsWorkItem
{
    [CmdletBinding(ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])]
    Param
    (
        [Parameter(ValueFromPipeline=$true, Mandatory=$true, Position=0)]
        [object] 
        $Type,

        [Parameter(Position=1)]
        [string]
        $Title,

        [Parameter()]
        [hashtable]
        $Fields,

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
        if($PSCmdlet.ShouldProcess($Type, 'Create work item of specified type'))
        {
            $wit = Get-TfsWorkItemType -Type $Type -Project $Project -Collection $Collection

            $wi = $wit.NewWorkItem()

            if ($Title)
            {
                $wi.Title = $Title
            }

            foreach($field in $Fields)
            {
                $wi.Fields[$field.Key] = $field.Value
            }

            $wi.Save()

            if ($Passthru)
            {
                return $wi
            }
        }
    }
}
