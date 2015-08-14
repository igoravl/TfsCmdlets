<#
.SYNOPSIS
	Gets all queued builds

.PARAMETER BuildDefinition
	Uses this parameter to filter for an specific Build Defintion.
	If suppress, cmdlet will show all queue builds.

.PARAMETER Collection
	Specifies either a URL or the name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object.
	For more details, see the -Collection argument in the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Project
	Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. 
	For more details, see the -Project argument in the Get-TfsTeamProject cmdlet. 

.EXAMPLE
	Get-TfsBuildQueue -BuildDefinition "My Build Definition" -Project "My Team Project"
	Get all queued builds given a definition name and a team project name

.EXAMPLE
	Get-TfsBuildQueue
	Get all queued builds, regardless of definition name or team project name

#>
Function Get-TfsXamlBuildDefinition
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Build.Client.IBuildDefinition])]
	Param
	(
		[Parameter(Position=0)]
		[ValidateScript({$_ -is [string] -or $_ -is [Microsoft.TeamFoundation.Build.Client.IBuildDefinition]})]
		[ValidateNotNullOrEmpty()]
		[Alias("Name")]
		[object] 
		$BuildDefinition = "*",

		[Parameter(ValueFromPipeline=$true)]
		[object]
		$Project,

		[Parameter()]
        [object]
        $Collection
	)

	Process
	{
		if ($BuildDefinition -is [Microsoft.TeamFoundation.Build.Client.IBuildDefinition])
		{
			return $BuildDefinition
		}

		$tp = Get-TfsTeamProject $Project $Collection
		$tpName = $tp.Name
		$tpc = $tp.Store.TeamProjectCollection

		$buildServer = $tpc.GetService([type]'Microsoft.TeamFoundation.Build.Client.IBuildServer')
		$buildDefs = $buildServer.QueryBuildDefinitions($tpName)
		
		return $buildDefs | Where Name -Like $BuildDefinition
 	}
}
