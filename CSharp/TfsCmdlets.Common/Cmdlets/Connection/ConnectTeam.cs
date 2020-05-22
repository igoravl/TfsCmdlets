/*
.SYNOPSIS
Connects to a team.

.DESCRIPTION

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
Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
System.String
*/

using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "Team", DefaultParameterSetName="Explicit credentials")]
	[OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.WebApiTeam))]
    public class ConnectTeam: BaseCmdlet
    {
/*
		[Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
		[ValidateNotNull()]
		[object] 
		Team,
	
		[Parameter()]
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
		if (Interactive.IsPresent)
		{
			Credential = (Get-TfsCredential -Interactive)
		}

        t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)

		[TfsCmdlets.CurrentConnections]::Reset()
		[TfsCmdlets.CurrentConnections]::Server = srv
		[TfsCmdlets.CurrentConnections]::Collection = tpc
		[TfsCmdlets.CurrentConnections]::Project = tp
		[TfsCmdlets.CurrentConnections]::Team = t

		this.Log($"Adding "{{t}.Name}" to the MRU list");

		_SetMru "Server" -Value (srv.Uri)
		_SetMru "Collection" -Value (tpc.Uri)
		_SetMru "Project" -Value (tp.Name)
		_SetMru "Team" -Value (t.Name)

		this.Log($"Connected to "{{t}.Name}"");

		if (Passthru)
		{
			WriteObject(t); return;
		}
	}
}

Set-Alias -Name ctfst -Value Connect-TfsTeam
*/
}
}
