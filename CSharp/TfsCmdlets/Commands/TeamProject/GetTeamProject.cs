using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Commands.TeamProject
{
    [Command]
    internal class GetTeamProject : CommandBase<WebApiTeamProject>
    {
        public ICurrentConnections CurrentConnections { get; }

        public override IEnumerable<WebApiTeamProject> Invoke(ParameterDictionary parameters)
        {
            var project = parameters.Get<object>(nameof(Cmdlets.TeamProject.GetTeamProject.Project));
            var current = parameters.Get<bool>(nameof(Cmdlets.TeamProject.GetTeamProject.Current));
            var deleted = parameters.Get<bool>(nameof(Cmdlets.TeamProject.GetTeamProject.Deleted));

            if (project == null || current)
            {
                Logger.Log("Get currently connected team project");

                yield return CurrentConnections.Project;
                yield break;
            }

            var client = GetClient<ProjectHttpClient>();

            while (true) switch (project)
                {
                    case Microsoft.TeamFoundation.Core.WebApi.TeamProject tp:
                        {
                            yield return tp;
                            yield break;
                        }
                    case Guid g:
                        {
                            project = g.ToString();
                            continue;
                        }
                    case string s when !s.IsWildcard() && !deleted:
                        {
                            yield return FetchProject(s, client);
                            yield break;
                        }
                    case string s:
                        {
                            var stateFilter = deleted ? ProjectState.Deleted : ProjectState.All;
                            var tpRefs = FetchProjects(stateFilter, client);

                            foreach (var tpRef in tpRefs.Where(r => StringExtensions.IsLike(r.Name, s)))
                            {
                                var proj = deleted ?
                                    new Microsoft.TeamFoundation.Core.WebApi.TeamProject(tpRef) :
                                    FetchProject(tpRef.Id.ToString(), client);

                                if (proj == null) continue;

                                yield return proj;
                            }

                            yield break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent team project {project}"));
                            yield break;
                        }
                }
        }

        private WebApiTeamProject FetchProject(string project, ProjectHttpClient client)
        {
            try
            {
                return client.GetProject(project, true).GetResult($"Error getting team project '{project}'");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return null;
        }

        private IEnumerable<TeamProjectReference> FetchProjects(ProjectState stateFilter, ProjectHttpClient client)
        {
            try
            {
                return client.GetProjects(stateFilter).GetResult($"Error getting team project(s)");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }

            return null;
        }

        [ImportingConstructor]
        public GetTeamProject(ICurrentConnections currentConnections, IPowerShellService powerShell, IConnectionManager connections, IDataManager data, ILogger logger)
          : base(powerShell, connections, data, logger)
        {
            CurrentConnections = currentConnections;
        }

    }
}