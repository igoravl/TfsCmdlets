/*
.SYNOPSIS
Connects to a team project.

.DESCRIPTION
The Connect-TfsTeamProject cmdlet "connects" (initializes a Microsoft.TeamFoundation.WorkItemTracking.Client.Project object) to a TFS Team Project. That connection is subsequently kept in a global variable to be later reused until it"s closed by a call to Disconnect-TfsTeamProject.
Cmdlets in the TfsCmdlets module that require a team project object to be provided via their -Project argument in order to access a TFS project will use the connection opened by this cmdlet as their "default project". In other words, TFS cmdlets (e.g. New-TfsArea) that have a -Project argument will use the connection provided by Connect-TfsTeamProject by default.

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.PARAMETER Interactive
Prompts for user credentials. Can be used for any Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build).

.PARAMETER Passthru
Returns the results of the command. By default, this cmdlet does not generate any output. 

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Connect-TfsTeamProject -Project FabrikamFiber
Connects to a project called FabrikamFiber in the current team project collection (as specified in a previous call to Connect-TfsTeamProjectCollection)

.EXAMPLE
Connect-TfsTeamProject -Project FabrikamFiber -Collection http://vsalm:8080/tfs/FabrikamFiberCollection
Connects to a project called FabrikamFiber in the team project collection specified in the given URL
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "TeamProject", DefaultParameterSetName="Explicit credentials")]
	[OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    public class ConnectTeamProject: BaseCmdlet
    {
/*
		[Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
		[ValidateNotNull()]
		[object] 
		Project,
	
		[Parameter()]
		[object] 
		Collection,
	
		[Parameter(ParameterSetName="Explicit credentials")]
		public object Credential { get; set; }

		[Parameter(ParameterSetName="Prompt for credentials", Mandatory=true)]
		public SwitchParameter Interactive { get; set; }

		[Parameter()]
		public SwitchParameter Passthru { get; set; }

	protected override void BeginProcessing()
	{
		// this.LogParams
        // _Requires "Microsoft.VisualStudio.Services.Common", "Microsoft.VisualStudio.Services.Client.Interactive", "Microsoft.TeamFoundation.Core.WebApi"
	}

	protected override void ProcessRecord()
	{
		tpcArgs = PSBoundParameters
		[void] tpcArgs.Remove("Project")

		tpc = Get-TfsTeamProjectCollection @tpcArgs

		tp = (Get-TfsTeamProject -Project Project -Collection tpc | Select-Object -First 1)

		if (! tp)
		{
			throw new Exception($"Error connecting to team project {Project}")
		}

		srv = tpc.ParentConnection

		[TfsCmdlets.CurrentConnections]::Reset()
		[TfsCmdlets.CurrentConnections]::Server = srv
		[TfsCmdlets.CurrentConnections]::Collection = tpc
		[TfsCmdlets.CurrentConnections]::Project = tp

		this.Log($"Adding "{{tp}.Name}" to the MRU list");

		_SetMru "Server" -Value (srv.Uri)
		_SetMru "Collection" -Value (tpc.Uri)
		_SetMru "Project" -Value (tp.Name)

		this.Log($"Connected to {{tp}.Name}");

		if (Passthru)
		{
			WriteObject(tp); return;
		}
	}
}

Set-Alias -Name ctfstp -Value Connect-TfsTeamProject
*/
}
}
