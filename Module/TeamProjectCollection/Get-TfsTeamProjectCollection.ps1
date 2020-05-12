#define ITEM_TYPE Microsoft.VisualStudio.Services.WebApi.VssConnection
<#
.SYNOPSIS
Gets information about one or more team project collections.

.DESCRIPTION
The Get-TfsTeamProjectCollection cmdlets gets one or more Team Project Collection objects (an instance of Microsoft.TeamFoundation.Client.TfsTeamProjectCollection) from a TFS instance. 
Team Project Collection objects can either be obtained by providing a fully-qualified URL to the collection or by collection name (in which case a TFS Configuration Server object is required).

.PARAMETER Collection
HELP_PARAM_COLLECTION

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server configuration server (the "root" of a TFS installation) to connect to, or a previously initialized Microsoft.TeamFoundation.Client.TfsConfigurationServer object.

.PARAMETER Current
Returns the team project collection specified in the last call to Connect-TfsTeamProjectCollection (i.e. the "current" project collection)

.PARAMETER Credential
HELP_PARAM_TFSCREDENTIAL

.EXAMPLE
Get-TfsTeamProjectCollection http://

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri

.NOTES
Cmdlets in the TfsCmdlets module that operate on a collection level require a TfsConfigurationServer object to be provided via the -Server argument. If absent, it will default to the connection opened by Connect-TfsConfigurationServer.
#>
Function Get-TfsTeamProjectCollection
{
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingPlainTextForPassword', '')]
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSUsePSCredentialType', '')]
	[CmdletBinding(DefaultParameterSetName='Get by collection')]
	[OutputType('ITEM_TYPE')]
	Param
	(
		[Parameter(Position=0, ParameterSetName="Get by collection")]
        [SupportsWildcards()]
		[object] 
		$Collection = "*",
	
		[Parameter(ValueFromPipeline=$true, ParameterSetName="Get by collection")]
		[object] 
		$Server,
	
		[Parameter(Position=0, ParameterSetName="Get current")]
        [switch]
        $Current,

		[Parameter(ParameterSetName="Get by collection")]
		[object]
		$Credential
	)

	Process
	{
		if ($Current.IsPresent -or (-not $Collection))
        {
			_Log 'Return current connection'
			return [TfsCmdlets.CurrentConnections]::Collection
		}
		
		CHECK_ITEM($Collection)

		$cred = Get-TfsCredential -Credential $Credential

		if ($Collection -is [Uri] -or ([Uri]::IsWellFormedUriString($Collection, [UriKind]::Absolute)))
		{
			_Log "Return collection referenced by URL '$Collection'"

			$tpc = [Microsoft.VisualStudio.Services.WebApi.VssConnection]::new([uri]$Collection, $cred)
			CALL_ASYNC($tpc.ConnectAsync(), "Error connecting to '$tpc.Uri'")
		}

		return $tpc
	}
}
