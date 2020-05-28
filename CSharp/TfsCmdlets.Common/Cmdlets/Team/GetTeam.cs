/*

.SYNOPSIS
    Gets information about one or more teams.

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

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet(VerbsCommon.Get, "Team", DefaultParameterSetName="Get by team")]
    [OutputType(typeof(WebApiTeam))]
    public class GetTeam: BaseCmdlet
    {
/*
        [Parameter(Position=0, ParameterSetName="Get by team")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; } = "*",

        [Parameter(ParameterSetName="Get by team")]
        public SwitchParameter IncludeMembers { get; set; }

        [Parameter(ParameterSetName="Get by team")]
        public SwitchParameter IncludeSettings { get; set; }

        [Parameter(ValueFromPipeline=true, ParameterSetName="Get by team")]
        public object Project { get; set; }

        [Parameter(ParameterSetName="Get by team")]
        public object Collection { get; set; }

		[Parameter(Mandatory=true, ParameterSetName="Get current")]
        public SwitchParameter Current { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Work.WebApi"
    }

    protected override void ProcessRecord()
    {
        if(Current.IsPresent || (! Team))
        {
			WriteObject(TfsCmdlets.CurrentConnections.Team); return;
        }

        if (Team is Microsoft.TeamFoundation.Core.WebApi.WebApiTeam) { this.Log("Input item is of type Microsoft.TeamFoundation.Core.WebApi.WebApiTeam; returning input item immediately, without further processing."; WriteObject(Team }); return;);

        tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        var client = tpc.GetClient<Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient>();
        workvar client = tpc.GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();

        if(Team.ToString().Contains("*"))
        {
            this.Log($"Get all teams matching "{Team}"");
            teams = client.GetTeamsAsync(tp.Name).Result | Where-Object Name -like Team
        }
        else
        {
            this.Log($"Get team named "{Team}"");

            if(_TestGuid Team)
            {
                Team = [guid]Team
            }

            teams = client.GetTeamAsync(tp.Name, Team).Result
        }

        foreach(t in teams)
        {
            if (IncludeMembers.IsPresent)
            {
                this.Log($"Retrieving team membership information for team "{{t}.Name}"");

                members = client.GetTeamMembersWithExtendedPropertiesAsync(tp.Name, t.Name).Result
                t | Add-Member -Name "Members" -MemberType NoteProperty -Value members

            }
            else
            {
                t | Add-Member -Name "Members" -MemberType NoteProperty -Value @()
            }

            if (IncludeSettings.IsPresent)
            {
                this.Log($"Retrieving team settings for team "{{t}.Name}"");

                ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(tp.Name, t.Name)
                t | Add-Member -Name "Settings" -MemberType NoteProperty -Value workClient.GetTeamSettingsAsync(ctx).Result
            }
            else
            {
                t | Add-Member -Name "Settings" -MemberType NoteProperty -Value null
            }
        }

        WriteObject(teams); return;
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
