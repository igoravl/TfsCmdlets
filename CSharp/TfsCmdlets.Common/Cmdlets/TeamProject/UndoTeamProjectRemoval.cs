using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiTeamProjectRef = Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Undeletes one or more team projects. 
    /// </summary>
    [Cmdlet(VerbsCommon.Undo, "TfsTeamProjectRemoval", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class UndoTeamProjectRemoval : CmdletBase
    {
        /// <summary>
        /// Specifies the name of the Team Project to undelete.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var done = false;
            var projs = new List<TeamProjectReference>();

            while (!done) switch (Project)
                {
                    case TeamProjectReference tpRef:
                        {
                            projs.Add(tpRef);
                            done = true;
                            break;
                        }
                    case string s:
                        {
                            projs.AddRange(GetItems<WebApiTeamProjectRef>(new { Project = s }));
                            done = true;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent team project '{Project}'");
                        }
                }

            var client = GetService<IRestApiService>();

            foreach (var tp in projs)
            {
                client.InvokeAsync(
                    GetCollection(),
                    $"/_apis/projects/{tp.Id}",
                    "PATCH",
                    $"{{\"state\":1,\"name\":\"{tp.Name}\"}}")
                    .GetResult($"Error restoring team project '{tp.Name}'");
            }
        }
    }
}