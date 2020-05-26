using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    [Exports(typeof(TeamProject))]
    internal class ProjectService : BaseService<TeamProject>
    {
        protected override string ItemName => "Team Project";

        protected override IEnumerable<TeamProject> GetItems(object filter)
        {
            var parms = Cmdlet.GetParameters();
            var project = parms["Project"];
            var current = parms.Get<bool>("Current");

            var tpc = Cmdlet.GetCollection();
            var client = tpc.GetClient<ProjectHttpClient>();

            while (true)
                switch (project)
                {
                    case null:
                    case object _ when current:
                    {
                        Logger.Log("Get currently connected team project");
                        yield return CurrentConnections.Project;
                        yield break;
                    }
                    case TeamProject tp:
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