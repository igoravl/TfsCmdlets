using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

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
    [Cmdlet(VerbsCommon.Get, "TfsTeamProject", DefaultParameterSetName = "Get by project")]
    [OutputType(typeof(WebApiTeamProject))]
    public class GetTeamProject : CmdletBase
    {
        /// <summary>
        /// Specifies the name of a Team Project. Wildcards are supported. 
        /// When omitted, all team projects in the supplied collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by project")]
        public object Project { get; set; } = "*";

        /// <summary>
        /// Lists deleted team projects present in the "recycle bin"
        /// </summary>
        [Parameter(ParameterSetName = "Get by project")]
        public SwitchParameter Deleted { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true, Position = 1, ParameterSetName = "Get by project")]
        public object Collection { get; set; }

        /// <summary>
        /// Returns the team project specified in the last call to Connect-TfsTeamProject 
        /// (i.e. the "current" team project)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                WriteItems<WebApiTeamProject>();
            }
            catch
            {
                if (!Current) throw;
            }
        }
    }

    [Exports(typeof(WebApiTeamProject))]
    internal partial class TeamProjectDataService : BaseDataService<WebApiTeamProject>
    {
        protected override IEnumerable<WebApiTeamProject> DoGetItems()
        {
            var project = GetParameter<object>(nameof(GetTeamProject.Project));
            var current = GetParameter<bool>(nameof(GetTeamProject.Current));
            var deleted = GetParameter<bool>(nameof(GetTeamProject.Deleted));

            if (project == null || current)
            {
                Logger.Log("Get currently connected team project");

                var c = CurrentConnections.Project;
                if (c != null) yield return c;

                yield break;
            }

            var tpc = GetCollection();
            var client = GetClient<ProjectHttpClient>();

            while (true) switch (project)
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
                    case string s when !s.IsWildcard() && !deleted:
                        {
                            yield return client.GetProject(s, true).GetResult($"Error getting team project '{project}'");
                            yield break;
                        }
                    case string s:
                        {
                            var stateFilter = deleted ? ProjectState.Deleted : ProjectState.All;
                            var tpRefs = client.GetProjects(stateFilter).GetResult($"Error getting team project(s) '{project}'");

                            foreach (var tpRef in tpRefs.Where(r => r.Name.IsLike(s)))
                            {
                                if (deleted)
                                {
                                    yield return new WebApiTeamProject(tpRef);
                                    continue;
                                }
                                yield return client.GetProject(tpRef.Id.ToString(), true).GetResult($"Error getting team project '{tpRef.Id}'");
                            }

                            yield break;
                        }
                    default:
                    {
                        throw new ArgumentException($"Invalid or non-existent team project {project}");
                    }
                }
        }
    }
}