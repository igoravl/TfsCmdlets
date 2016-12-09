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
        if ($WorkItem -is [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem])
        {
            $tpc = $WorkItem.Store.TeamProjectCollection
            $id = $WorkItem.Id
        }
        else
        {
            $tpc = Get-TfsTeamProjectCollection -Collection $Collection
            $id = (Get-TfsWorkItem -WorkItem $WorkItem -Collection $Collection).Id
        }

        if ($BypassRules)
        {
            $store = New-Object 'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore' -ArgumentList $tpc, [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStoreFlags]::BypassRules
        }
        else
        {
            $store = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
        }

        $wi = $store.GetWorkItem($id)

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