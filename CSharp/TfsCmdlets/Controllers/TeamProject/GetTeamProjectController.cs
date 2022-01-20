using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class GetTeamProjectController
    {
        protected override IEnumerable Run()
        {
            if (!Has_Project || Current)
            {
                Logger.Log("Get currently connected team project");

                yield return CurrentConnections.Project;
                yield break;
            }

            var client = GetClient<ProjectHttpClient>();

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
                            yield return FetchProject(g.ToString(), client, IncludeDetails);
                            continue;
                        }
                    case string s when !s.IsWildcard() && !Deleted:
                        {
                            yield return FetchProject(s, client, IncludeDetails);
                            break;
                        }
                    case string s:
                        {
                            var stateFilter = Deleted ? ProjectState.Deleted : ProjectState.All;
                            var tpRefs = FetchProjects(stateFilter, client);

                            foreach (var tpRef in tpRefs.Where(r => r.Name.IsLike(s)))
                            {
                                var proj = Deleted || !IncludeDetails?
                                    new WebApiTeamProject(tpRef) :
                                    FetchProject(tpRef.Id.ToString(), client, true);

                                if (proj == null) continue;

                                yield return proj;
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

        private WebApiTeamProject FetchProject(string project, ProjectHttpClient client, bool includeDetails = true)
            => client.GetProject(project, includeDetails).GetResult($"Error getting team project '{project}'");

        private IEnumerable<TeamProjectReference> FetchProjects(ProjectState stateFilter, ProjectHttpClient client)
            => client.GetProjects(stateFilter).GetResult($"Error getting team project(s)");

        [Import]
        private ICurrentConnections CurrentConnections { get; }
    }
}
