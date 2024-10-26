using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Run
{
    /// <summary>
    /// Queues (starts) a new pipeline run.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class StartPipeline
    {
        /// <summary>
        /// Specifies the pipeline to start.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path", "Pipeline")]
        public object Definition { get; set; }

        /// <summary>
        /// Specifies the branch to build. When omitted, the default branch is built.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Specifies the stages to skip during the pipeline execution. When omitted, all stages are executed.
        /// </summary>
        [Parameter]
        public string[] StagesToSkip { get; set; }

        /// <summary>
        /// Overrides the variables defined in the pipeline.
        /// </summary>
        [Parameter]
        public Hashtable Variables { get; set; }

        /// <summary>
        /// Overrides the build resources defined in the pipeline.
        /// </summary>
        [Parameter]
        public Hashtable BuildOverrides { get; set; }

        /// <summary>
        /// Overrides the repository resources defined in the pipeline.
        /// </summary>
        [Parameter]
        public Hashtable RepositoryOverrides { get; set; }
    }

    [CmdletController(typeof(WebApiBuild), Client = typeof(IBuildHttpClient))]
    partial class StartPipelineController
    {
        protected override IEnumerable Run()
        {
            var definition = GetItem<BuildDefinitionReference>();

            yield return Client.QueueBuildAsync(project: Project.Name, definitionId: null, build: new WebApiBuild
            {
                Definition = definition
            }).GetResult($"Error queuing build '{definition}'");
        }
    }
}