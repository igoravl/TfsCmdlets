using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Gets information about one or more team projects.
    /// </summary>
    /// <remarks>
    /// The Get-TfsTeamProject cmdlets gets one or more Team Project objects 
    /// (an instance of Microsoft.TeamFoundation.Core.WebApi.TeamProject) from the supplied 
    /// Team Project Collection.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, DefaultParameterSetName = "Get by project", OutputType = typeof(WebApiTeamProject))]
    partial class GetTeamProject
    {
        /// <summary>
        /// Specifies the name of a Team Project. Wildcards are supported. 
        /// When omitted, all team projects in the supplied collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by project")]
        [Parameter(Position = 0, ParameterSetName = "Cached credentials")]
        [Parameter(Position = 0, ParameterSetName = "User name and password")]
        [Parameter(Position = 0, ParameterSetName = "Credential object")]
        [Parameter(Position = 0, ParameterSetName = "Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName = "Prompt for credential")]
        public object Project { get; set; } = "*";

        /// <summary>
        /// Lists deleted team projects present in the "recycle bin"
        /// </summary>
        [Parameter(ParameterSetName = "Get by project")]
        [Parameter(ParameterSetName = "Cached credentials")]
        [Parameter(ParameterSetName = "User name and password")]
        [Parameter(ParameterSetName = "Credential object")]
        [Parameter(ParameterSetName = "Personal Access Token")]
        [Parameter(ParameterSetName = "Prompt for credential")]
        public SwitchParameter Deleted { get; set; }

        /// <summary>
        /// Returns only those team projects matching the specified process template(s).
        /// Wildcards are supported. When omitted returns all team projects, regardless of process template.
        /// </summary>
        [Parameter]
        [SupportsWildcards]
        public string[] Process { get; set; }

        /// <summary>
        /// Includes details about the team projects, such as the process template name and other properties.
        /// Specifying this argument signficantly increases the time it takes to complete the operation.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeDetails { get; set; }

        /// <summary>
        /// Returns the team project specified in the last call to Connect-TfsTeamProject 
        /// (i.e. the "current" team project)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }
    }

    [CmdletController(typeof(WebApiTeamProject), Client = typeof(IProjectHttpClient))]
    partial class GetTeamProjectController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        [Import]
        private IPaginator Paginator { get; }

        protected override IEnumerable Run()
        {
            if (Parameters.Get<object>("Project") == null || Current)
            {
                Logger.Log("Get currently connected team project");

                yield return IncludeDetails ?
                    FetchProject(CurrentConnections.Project.Name, Client, true) : 
                    CurrentConnections.Project;

                yield break;
            }

            var includeDetails = IncludeDetails || Has_Process;

            foreach (var project in Project)
            {
                switch (project)
                {
                    case Microsoft.TeamFoundation.Core.WebApi.TeamProject tp:
                        {
                            yield return tp;
                            break;
                        }
                    case Guid g:
                        {
                            var p = FetchProject(g.ToString(), Client, includeDetails);

                            if(Has_Process && !Process.Any(process => p.Capabilities["processTemplate"]["templateName"].IsLike(process))) continue;
                            
                            yield return p;

                            break;
                        }
                    case string s when !s.IsWildcard() && !Deleted:
                        {
                            var p = FetchProject(s, Client, includeDetails);
                            
                            if(Has_Process && !Process.Any(process => p.Capabilities["processTemplate"]["templateName"].IsLike(process))) continue;
                            
                            yield return p;

                            break;
                        }
                    case string s:
                        {
                            var stateFilter = Deleted ? ProjectState.Deleted : ProjectState.All;
                            var tpRefs = FetchProjects(stateFilter, Client);

                            foreach (var tpRef in tpRefs.Where(r => r.Name.IsLike(s)))
                            {
                                var p = Deleted || !includeDetails?
                                    new WebApiTeamProject(tpRef) :
                                    FetchProject(tpRef.Id.ToString(), Client, true);

                                if (p == null) continue;

                                if(Has_Process && !Process.Any(process => p.Capabilities["processTemplate"]["templateName"].IsLike(process))) continue;

                                yield return p;
                            }
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent team project {project}"));
                            break;
                        }
                }
            }
        }

        private WebApiTeamProject FetchProject(string project, IProjectHttpClient client, bool includeDetails = true)
            => client.GetProject(project, includeDetails).GetResult($"Error getting team project '{project}'");

        private IEnumerable<TeamProjectReference> FetchProjects(ProjectState stateFilter, IProjectHttpClient client)
            => Paginator.Paginate((top, skip) => client.GetProjects(stateFilter, top: top, skip: skip).GetResult($"Error getting team project(s)"));
    }
}