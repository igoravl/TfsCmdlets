<#

.SYNOPSIS
    Sets the contents of one or more work items.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

#>
Function Set-TfsWorkItem
{
    [CmdletBinding()]
    Param
    (
        [Parameter(ValueFromPipeline=$true, Position=0)]
        [Alias("id")]
        [ValidateNotNull()]
        [object]
        $WorkItem,

        [Parameter(Position=1)]
        [hashtable]
        $Fields,

        [Parameter()]
        [switch]
        $BypassRules,

        [Parameter()]
        [switch] 
        $SkipSave,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        if ($BypassRules)
        {
            if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
            {
                Write-Warning "An actual WorkItem object was provided with -BypassRules. A new object will be opened since it is not possible to bypass rules on a previously opened work item."
                $tpc = $WorkItem.Store.TeamProjectCollection
            }
            else
            {
                $tpc = Get-TfsTeamProjectCollection -Collection $Collection
            }

            $store = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore' -ArgumentList @($tpc, [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStoreFlags]::BypassRules)
            $id = (Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection).Id
            $wi = $store.GetWorkItem($id)
        }
        else
        {
            $wi = (Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection)
        }

        $Fields = _FixAreaIterationValues -Fields $Fields -ProjectName $wi.Project.Name

        foreach($fldName in $Fields.Keys)
        {
            $wi.Fields[$fldName].Value = $Fields[$fldName]
        }

        if(-not $SkipSave)
        {
            $wi.Save()
        }

        return $wi
    }
}