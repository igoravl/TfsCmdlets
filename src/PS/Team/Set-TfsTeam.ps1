<#
.SYNOPSIS
Changes the details of a team.

.PARAMETER Project
${HelpParam_Project}

.PARAMETER Collection
${HelpParam_Collection}

.INPUTS
Microsoft.TeamFoundation.Client.TeamFoundationTeam
System.String
#>
Function Set-TfsTeam
{
    [CmdletBinding(DefaultParameterSetName="Get by name", ConfirmImpact='Medium', SupportsShouldProcess=$true)]
    [OutputType([Microsoft.TeamFoundation.Client.TeamFoundationTeam])]
    param
    (
        [Parameter(Position=0, ValueFromPipeline=$true)]
        [Alias("Name")]
        [ValidateScript({($_ -is [string]) -or ($_ -is [Microsoft.TeamFoundation.Client.TeamFoundationTeam])})] 
        [SupportsWildcards()]
        [object]
        $Team = '*',

        [Parameter()]
        [switch]
        $Default,

        [Parameter()]
        [string]
        $NewName,

        [Parameter()]
        [string]
        $Description,

        [Parameter()]
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection
        
        if ($Project)
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
        }
        else
        {
            $tpc = Get-TfsTeamProjectCollection -Collection $Collection
        }

        $teamService = $tpc.GetService([type]'Microsoft.TeamFoundation.Client.TfsTeamService')

        if ($NewName -and $PSCmdlet.ShouldProcess($Team, "Rename team to '$NewName'"))
        {
            $isDirty = $true
            $t.Name = $NewName
        }

        if ($PSBoundParameters.ContainsKey('Description') -and $PSCmdlet.ShouldProcess($Team, "Set team's description to '$Description'"))
        {
            $isDirty = $true
            $t.Description = $Description
        }

        if ($Default -and $PSCmdlet.ShouldProcess($Team, "Set team to project's default team"))
        {
            $teamService.SetDefaultTeam($t)
        }

        if($isDirty)
        {
            $teamService.UpdateTeam($t)
        }
        
        return $t
    }
}
