using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class GetTeamProjectController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        [Import]
        private IPaginator Paginator { get; }

        protected override IEnumerable Run()
        {
            var client = GetClient<ProjectHttpClient>();

            if (Parameters.Get<object>("Project") == null || Current)
            {
                Logger.Log("Get currently connected team project");

                yield return IncludeDetails ?
                    FetchProject(CurrentConnections.Project.Name, client, true) : 
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
                            var p = FetchProject(g.ToString(), client, includeDetails);

                            if(Has_Process && !Process.Any(process => p.Capabilities["processTemplate"]["templateName"].IsLike(process))) continue;
                            
                            yield return p;

                            break;
                        }
                    case string s when !s.IsWildcard() && !Deleted:
                        {
                            var p = FetchProject(s, client, includeDetails);
                            
                            if(Has_Process && !Process.Any(process => p.Capabilities["processTemplate"]["templateName"].IsLike(process))) continue;
                            
                            yield return p;

                            break;
                        }
                    case string s:
                        {
                            var stateFilter = Deleted ? ProjectState.Deleted : ProjectState.All;
                            var tpRefs = FetchProjects(stateFilter, client);

                            foreach (var tpRef in tpRefs.Where(r => r.Name.IsLike(s)))
                            {
                                var p = Deleted || !includeDetails?
                                    new WebApiTeamProject(tpRef) :
                                    FetchProject(tpRef.Id.ToString(), client, true);

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

        private WebApiTeamProject FetchProject(string project, ProjectHttpClient client, bool includeDetails = true)
            => client.GetProject(project, includeDetails).GetResult($"Error getting team project '{project}'");

        private IEnumerable<TeamProjectReference> FetchProjects(ProjectState stateFilter, ProjectHttpClient client)
            => Paginator.Paginate((top, skip) => client.GetProjects(stateFilter, top: top, skip: skip).GetResult($"Error getting team project(s)"));
    }
}
