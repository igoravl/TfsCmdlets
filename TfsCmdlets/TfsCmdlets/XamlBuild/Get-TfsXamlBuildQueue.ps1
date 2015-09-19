<#
.SYNOPSIS
	Gets all queued builds

.PARAMETER BuildDefinition
	Uses this parameter to filter for an specific Build Defintion.
	If suppress, cmdlet will show all queue builds.

.PARAMETER Project
    ${HelpParam_Project}

.PARAMETER Collection
    ${HelpParam_Collection}

.EXAMPLE
	Get-TfsBuildQueue -BuildDefinition "My Build Definition" -Project "My Team Project"
	Get all queued builds given a definition name and a team project name

.EXAMPLE
	Get-TfsBuildQueue
	Get all queued builds, regardless of definition name or team project name

#>
Function Get-TfsXamlBuildQueue
{
	[CmdletBinding()]
	[OutputType([Microsoft.TeamFoundation.Build.Client.IQueuedBuild])]
	Param
	(
		[Parameter(Position=0, ValueFromPipeline=$true)]
		[ValidateScript({$_ -is [string] -or $_ -is [Microsoft.TeamFoundation.Build.Client.IBuildDefinition]})]
		[ValidateNotNullOrEmpty()]
		[object] 
		$BuildDefinition = "*",

		[Parameter()]
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
			$buildDefName = $BuildDefinition.Name
		}
		else
		{
			$buildDefName = $BuildDefinition
		}

		if ($Project)
		{
			$tp = Get-TfsTeamProject $Project $Collection
			$tpName = $tp.Name
			$tpc = $tp.Store.TeamProjectCollection
		}
		else
		{
			$tpName = "*"
			$tpc = Get-TfsTeamProjectCollection $Collection
		}

		$buildServer = $tpc.GetService([type]'Microsoft.TeamFoundation.Build.Client.IBuildServer')
		$query = $buildServer.CreateBuildQueueSpec($tpName, $buildDefName)
		
		$buildServer.QueryQueuedBuilds($query).QueuedBuilds
 	}
}
