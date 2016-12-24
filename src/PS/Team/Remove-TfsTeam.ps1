<#

.SYNOPSIS
    Deletes a team.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.INPUTS
    Microsoft.TeamFoundation.Client.TeamFoundationTeam
    System.String
#>
Function Remove-TfsTeam
{
    [CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
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
        [object]
        $Project,

        [Parameter()]
        [object]
        $Collection
    )

    Process
    {
        $t = Get-TfsTeam -Team $Team -Project $Project -Collection $Collection

        if ($PSCmdlet.ShouldProcess($t.Name, 'Delete team'))
        {
            $tp = Get-TfsTeamProject -Project $Project -Collection $Collection
            $tpc = $tp.Store.TeamProjectCollection
            $identityService = $tpc.GetService([type]'Microsoft.TeamFoundation.Framework.Client.IIdentityManagementService')

            $identityService.DeleteApplicationGroup($t.Identity.Descriptor)
        }
    }
}
