using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Run
{
    /// <summary>
    /// Gets one or more pipeline (build) runs in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiBuild), DefaultParameterSetName = "By Search")]
    partial class GetPipelineRun
    {
        /// <summary>
        /// Specifies the pipeline to start. This can be a pipeline object or an ID.
        /// When omitted, all queued and running pipelines are returned in descending order of queue time.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ParameterSetName = "By Run", Mandatory = true)]
        [Alias("Id")]
        public object PipelineRun { get; set; }

        /// <summary>
        /// Returns only runs with the given reason.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public BuildReason Reason { get; set; }

        /// <summary>
        /// Returns only runs in the given state.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public BuildStatus Status { get; set; }

        /// <summary>
        /// Returns only runs with the given result.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public BuildResult Result { get; set; }

        /// <summary>
        ///Returns only runs that were requested for the given user.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public object RequestedFor { get; set; }

        /// <summary>
        /// Returns only runs that were finished/started/queued after the given date.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public DateTime? MinTime { get; set; }

        /// <summary>
        /// Returns only runs that were finished/started/queued before the given date.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public DateTime? MaxTime { get; set; }

        /// <summary>
        /// Returns only runs with the given tag(s).
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public string[] Tag { get; set; }

        /// <summary>
        /// Returns only runs for the given branch.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public object Branch { get; set; }

        /// <summary>
        /// Returns only runs for the given repository.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        [AutoParameter(typeof(GitRepository))]
        public object Repository { get; set; }

        /// <summary>
        /// Returns only runs for the given build definition.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        [AutoParameter(typeof(BuildDefinitionReference))]
        public object Definition { get; set; }

        /// <summary>
        /// Returns only runs with the given build number. Wildcards are supported.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        [SupportsWildcards]
        public string BuildNumber { get; set; }

        /// <summary>
        /// Specifies the query order. When omitted, runs are returned in descending order of queue time.
        /// </summary>
        [Parameter(ParameterSetName = "By Search")]
        public BuildQueryOrder QueryOrder { get; set; } = BuildQueryOrder.QueueTimeDescending;

    }

    [CmdletController(typeof(WebApiBuild), Client = typeof(IBuildHttpClient))]
    partial class GetPipelineRunController
    {
        protected override IEnumerable Run()
        {
            foreach (var input in PipelineRun)
            {
                switch (ParameterSetName)
                {
                    case "By Run":
                        {
                            switch (input)
                            {
                                case WebApiBuild build:
                                    {
                                        yield return build;
                                        break;
                                    }
                                case int id:
                                    {
                                        yield return Client.GetBuildAsync(Project.Name, id).GetResult($"Error getting build '{id}'");
                                        break;
                                    }
                                default:
                                    {
                                        throw new ArgumentException($"Invalid pipeline '{input}'");
                                    }
                            }
                            break;
                        }
                    case "By Search":
                        {
                            yield return SearchRuns();
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid parameter set '{ParameterSetName}'");
                        }
                }
            }
        }

        private object SearchRuns()
        {
            bool hasCriteria = false;

            IList<int> definitions = null;
            string buildNumber = null;
            DateTime? minFinishTime = null;
            DateTime? maxFinishTime = null;
            string requestedFor = null;
            BuildReason? reasonFilter = null;
            BuildStatus? statusFilter = null;
            BuildResult? resultFilter = null;
            IEnumerable<string> tagFilters = null;
            string branchName = null;
            BuildQueryOrder? queryOrder = QueryOrder;

            if (Has_BuildNumber)
            {
                buildNumber = BuildNumber;
                hasCriteria = true;
            }

            if (Has_Definition)
            {
                definitions = Data.GetItems<BuildDefinitionReference>()?.Select(d => d.Id).ToList();
                if(definitions == null || definitions.Count == 0)
                {
                    throw new ArgumentException($"Invalid build definition {Definition}");
                }
                hasCriteria = true;
            }

            if (Has_MaxTime)
            {
                maxFinishTime = MaxTime;
                hasCriteria = true;
            }

            if (Has_MinTime)
            {
                minFinishTime = MinTime;
                hasCriteria = true;
            }

            if (Has_RequestedFor)
            {
                var identity = Data.GetItem<WebApiIdentity>();
                requestedFor = identity.Properties["Account"]?.ToString();
                hasCriteria = true;
            }

            if (Has_Reason)
            {
                reasonFilter = Reason;
                hasCriteria = true;
            }

            if (Has_Result)
            {
                resultFilter = Result;
                hasCriteria = true;
            }

            if (Has_Status)
            {
                statusFilter = Status;
                hasCriteria = true;
            }

            if (Has_Tag)
            {
                tagFilters = Tag;
                hasCriteria = true;
            }

            if (Has_Branch)
            {
                branchName = Branch switch
                {
                    string s => s,
                    GitBranchStats gbs => gbs.Name,
                    _ => null
                };
                hasCriteria = true;
            }

            if(!hasCriteria)
            {
                statusFilter = BuildStatus.InProgress | BuildStatus.NotStarted;
            }

            return Client.GetBuildsAsync(project: Project.Id, definitions: definitions, buildNumber: buildNumber,
                                         minFinishTime: minFinishTime, maxFinishTime: maxFinishTime, requestedFor: requestedFor, 
                                         reasonFilter: reasonFilter, statusFilter: statusFilter, resultFilter: resultFilter, 
                                         tagFilters: tagFilters, branchName: branchName, queryOrder: queryOrder)
                                        .GetResult($"Error getting builds");
        }
    }
}