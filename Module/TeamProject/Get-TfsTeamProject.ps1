<#
.SYNOPSIS
Gets information about one or more team projects. 

.DESCRIPTION
The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of Microsoft.TeamFoundation.WorkItemTracking.Client.Project) from the supplied Team Project Collection.

.PARAMETER Project
Specifies the name of a Team Project. Wildcards are supported.

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Server
Specifies either a URL or the name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.
For more details, see the -Server argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

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
	[OutputType('Microsoft.TeamFoundation.WorkItemTracking.Client.Project')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidGlobalVars', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
    Param
    (
        [Parameter(Position=0, ParameterSetName='Get by project')]
        [object] 
        $Project = '*',

        [Parameter(ValueFromPipeline=$true, Position=1, ParameterSetName='Get by project')]
        [object]
        $Collection,

		[Parameter(Position=0, Mandatory=$true, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter(ParameterSetName='Get by project')]
		[object]
		$Credential
    )

    Begin
    {
        _ImportRequiredAssembly 'Microsoft.TeamFoundation.WorkItemTracking.Client'
    }

    Process
    {
        if ($Current)
        {
            return $script:TfsProjectConnection
        }

		if ($Project -is [Microsoft.TeamFoundation.WorkItemTracking.Client.Project])
		{
			return $Project
		}

        $tpc = Get-TfsTeamProjectCollection $Collection -Credential $Credential

        if (($Project -is [uri]) -or ([System.Uri]::IsWellFormedUriString($Project, [System.UriKind]::Absolute)))
        {
            $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')
            $projInfo = $css.GetProject([string] $Project)
            $Project = $projInfo.Name
        }

		if ($Project -is [string])
		{
            $wiStore = $tpc.GetService([type]'Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore')
            
            if($Project.IndexOf('*') -ge 0)
            {
                return _GetAllProjects $tpc | Where-Object Name -Like $Project | Foreach-Object { $wiStore.Projects[$_.Name] }
            }

            return $wiStore.Projects[$Project]
		}

		if ($null -eq $Project)
		{
			if ($script:TfsProjectConnection)
			{
				return $script:TfsProjectConnection
			}
		}

		throw "No TFS team project information available. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet."
    }
}

Function _GetAllProjects
{
    param ($tpc)

    $css = $tpc.GetService([type]'Microsoft.TeamFoundation.Server.ICommonStructureService')

    return $css.ListAllProjects() | Where-Object Status -eq WellFormed
}