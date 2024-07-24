using System.Management.Automation;
using Newtonsoft.Json.Linq;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProject.Member
{
    /// <summary>
    /// Gets the members of a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(TeamProjectMember))]
    [OutputType(typeof(WebApiIdentity), ParameterSetName = new[] { "As Identity" })]
    partial class GetTeamProjectMember
    {
        /// <summary>
        /// Specifies the name of a team project member. Wildcards are supported. 
        /// When omitted, all team project members are returned.
        /// </summary>
        [Parameter()]
        public object Member { get; set; } = "*";

        /// <summary>
        /// Returns the members as fully resolved <see cref="WebApiIdentity"/> objects.
        /// When omitted, it returns only the name, ID and email of the users.
        /// </summary>
        [Parameter()]
        public SwitchParameter AsIdentity { get; set; }
    }

    [CmdletController(typeof(TeamProjectMember))]
    partial class GetTeamProjectMemberController
    {
        [Import]
        private IRestApiService RestApiService {get;}

        protected override IEnumerable Run()
        {
            var query = new ContributionNodeQuery {
                ContributionIds = new[] { "ms.vss-tfs-web.project-members-data-provider-verticals" },
                DataProviderContext = new DataProviderContext{
                    Properties = new Dictionary<string, object>() {
                        ["forceRefresh"] = true,
                        ["sourcePage"] = new Dictionary<string,object>() {
                            ["url"] = Project.GetLink("web"),
                            ["routeId"] = "ms.vss-tfs-web.project-overview-route",
                            ["routeValues"] = new Dictionary<string,string>() {
                                ["project"] = Project.Name,
                                ["controller"] = "Apps",
                                ["action"] = "ContributedHub"
                            },
                        }
                    }
                }
            };

            var result = RestApiService.QueryContributionNodeAsync(Collection, query)
                .GetResult("Error invoking Contribution API");

            var col = result
                .DataProviders["ms.vss-tfs-web.project-members-data-provider-verticals"]
                .ToObject<TeamProjectMemberCollection>();

            foreach(var member in col.Members)
            {
                if(AsIdentity)
                {
                    yield return GetItem<Models.Identity>(new {Identity=member.Email});
                    continue;
                }

                member.TeamProject = Project.Name;
                yield return member;
            }
        }
    }
}