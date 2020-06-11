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
    public class GetTeamProject : BaseCmdlet<WebApiTeamProject>
    {
        /// <summary>
        /// Specifies the name of a Team Project. Wildcards are supported. 
        /// When omitted, all team projects in the supplied collection are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by project")]
        public object Project { get; set; } = "*";

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
                base.ProcessRecord();
            }
            catch
            {
                if (!Current) throw;
            }
        }
    }

    [Exports(typeof(WebApiTeamProject))]
    internal class TeamProjectService: BaseDataService<WebApiTeamProject>
    {
        protected override IEnumerable<WebApiTeamProject> DoGetItems()
        {
            var project = GetParameter<object>("Project");
            var current = GetParameter<bool>("Current");

            if (project == null || current)
            {
                Logger.Log("Get currently connected team project");

                var c = CurrentConnections.Project;
                if (c != null) yield return c;

                yield break;
            }

            var tpc = GetCollection();
            var client = GetClient<ProjectHttpClient>();

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