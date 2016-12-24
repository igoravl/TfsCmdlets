<#
.SYNOPSIS
    Gets information about one or more team projects. 

.DESCRIPTION
	The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of Microsoft.TeamFoundation.WorkItemTracking.Client.Project) from the supplied Team Project Collection.

.PARAMETER Project
	Specifies the name of a Team Project. Wildcards are supported.

.PARAMETER Collection
    ${HelpParam_Collection}

.PARAMETER Server
	Specifies either a URL or the name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.
	For more details, see the -Server argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Credential
    ${HelpParam_TfsCredential}

.INPUTS
	Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
    System.String
    System.Uri

.NOTES
	As with most cmdlets in the TfsCmdlets module, this cmdlet requires a TfsTeamProjectCollection object to be provided via the -Collection argument. If absent, it will default to the connection opened by Connect-TfsTeamProjectCollection.

#>
Function Get-TfsTeamProject
{
    [CmdletBinding(DefaultParameterSetName='Get by project')]
	[OutputType([Microsoft.TeamFoundation.WorkItemTracking.Client.Project])]
    Param
    (
        [Parameter(Position=0, ParameterSetName='Get by project')]
        [object] 
        $Project = '*',

        [Parameter(ValueFromPipeline=$true, Position=1, ParameterSetName='Get by project')]
        [object]
        $Collection,

		[Parameter(Position=0, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter()]
		[object]
		$Credential
    )

    Process
    {
        if ($Current)
        {
            return $global:TfsProjectConnection
        }

		if ($Project -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])
		{
			return $Project
		}

        if (($Project -is [uri]) -or ([System.Uri]::IsWellFormedUriString($Project, [System.UriKind]::Absolute)))
        {
			$tpc = Get-TfsTeamProjectCollection $Collection -Credential $Credential
            $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

            $projInfo = $css.GetProject([string] $Project)
            $Project = $projInfo.Name
        }

		if ($Project -is [string])
		{
			$tpc = Get-TfsTeamProjectCollection $Collection -Credential $Credential
			$wiStore = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')

			return _GetAllProjects $tpc | ? Name -Like $Project | % { $wiStore.Projects[$_.Name] }
		}

		if ($Project -eq $null)
		{
			if ($global:TfsProjectConnection)
			{
				return $global:TfsProjectConnection
			}
		}

		throw "No TFS team project information available. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet."
    }
}

Function _GetAllProjects
{
    param ($tpc)

    $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

    return $css.ListAllProjects() | ? Status -eq WellFormed
}