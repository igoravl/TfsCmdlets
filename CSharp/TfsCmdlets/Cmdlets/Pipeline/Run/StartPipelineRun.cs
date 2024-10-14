using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Run
{
    /// <summary>
    /// Queues (starts) a new pipeline run.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class StartPipelineRun
    {
        /// <summary>
        /// Specifies the pipeline to start.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path", "Pipeline")]
        public object Definition { get; set; }
    }

    [CmdletController(typeof(WebApiBuild), Client = typeof(IBuildHttpClient))]
    partial class StartPipelineRunController
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