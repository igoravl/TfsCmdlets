using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Exports(typeof(WebApiTeamProject))]
    internal class ProjectService : BaseDataService<WebApiTeamProject>
    {
        protected override string ItemName => "Team Project";

        protected override IEnumerable<WebApiTeamProject> GetItems(object userState)
        {
            var project = ItemFilter = Parameters.Get<object>("Project");
            var current = Parameters.Get<bool>("Current");

            if(project == null || current)
            {
                Logger.Log("Get currently connected team project");

                var c = CurrentConnections.Project;
                if(c != null) yield return c;

                yield break;
            }

            var tpc = Cmdlet.GetCollection();
            var client = tpc.GetClient<ProjectHttpClient>();

            while (true)
                switch (project)
                {
                    case WebApiTeamProject tp:
                    {
                        yield return tp;
                        yield break;
                    }
                    case Guid g:
                    {
                        project = g.ToString();
                        break;
                    }
                    case string s when !s.IsWildcard():
                    {
                        yield return client.GetProject(s, true).GetResult($"Error getting team project '{project}'");
                        yield break;
                    }
                    case string s:
                    {
                        var tpRefs = client.GetProjects().GetResult($"Error getting team project(s) '{project}'");

                        foreach (var tpRef in tpRefs.Where(r => r.Name.IsLike(s)))
                        {
                            yield return client.GetProject(tpRef.Id.ToString(), true).GetResult($"Error getting team project '{tpRef.Id}'");
                        }
                        
                        yield break;
                    }
                }
        }
    }
}