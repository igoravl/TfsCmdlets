using System;
using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiTeamProjectRef = Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    internal partial class UndoTeamProjectRemovalController
    {
        [Import]
        private IRestApiService RestApiService { get; }

        public override IEnumerable<WebApiTeamProject> Invoke()
        {
            var project = Parameters.Get<object>("Project");
            var references = new List<WebApiTeamProjectRef>();

            switch (project)
            {
                case WebApiTeamProjectRef tpRef:
                {
                    references.Add(tpRef);
                    break;
                }
                case string s:
                {
                    references.AddRange(GetItems(new { Project = s, Deleted = true }));
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid team project '{project}'");
                }
            }

            foreach (var tp in references)
            {
                RestApiService.InvokeAsync(
                        Data.GetCollection(),
                        $"/_apis/projects/{tp.Id}",
                        "PATCH",
                        $"{{\"state\":1,\"name\":\"{tp.Name}\"}}")
                    .GetResult($"Error restoring team project '{tp.Name}'");

                yield return GetItem(tp);
            }
        }
    }
}