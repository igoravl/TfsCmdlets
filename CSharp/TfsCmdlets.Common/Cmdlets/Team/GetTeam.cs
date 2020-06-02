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
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Team
{
    [Cmdlet(VerbsCommon.Get, "Team", DefaultParameterSetName = "Get by team")]
    [OutputType(typeof(WebApiTeam))]
    public class GetTeam : BaseCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "Get by team")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; } = "*";

        [Parameter(ParameterSetName = "Get by team")]
        public SwitchParameter IncludeMembers { get; set; }

        [Parameter(ParameterSetName = "Get by team")]
        public SwitchParameter IncludeSettings { get; set; }

        [Parameter(ValueFromPipeline = true, ParameterSetName = "Get by team")]
        public object Project { get; set; }

        [Parameter(ParameterSetName = "Get by team")]
        public object Collection { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            if (Current)
            {
                try
                {
                    WriteObject(this.GetCollectionOf<WebApiTeam>(), true);
                    return;
                }
                finally { }
            }

            if (!IncludeMembers && !IncludeSettings)
            {
                WriteObject(this.GetCollectionOf<WebApiTeam>(), true);
                return;
            }

            var (tpc, tp) = this.GetCollectionAndProject();
            var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.TeamHttpClient>();
            var workClient = GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();

            foreach (var t in this.GetCollectionOf<WebApiTeam>())
            {
                var pso = new PSObject(t);

                if (IncludeMembers)
                {
                    this.Log($"Retrieving team membership information for team '{t.Name}'");

                    var members = client.GetTeamMembersWithExtendedPropertiesAsync(tp.Name, t.Name)
                        .GetResult($"Error retrieving membership information for team {t.Name}");

                    pso.AddNoteProperty("Members", members);

                }

                if (IncludeSettings.IsPresent)
                {
                    this.Log($"Retrieving team settings for team '{t.Name}'");

                    var ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(tp.Name, t.Name);
                    pso.AddNoteProperty("Settings", workClient.GetTeamSettingsAsync(ctx).GetResult($"Error retrieving settings for team {t.Name}"));
                }

                WriteObject(pso);
            }
        }
    }
}