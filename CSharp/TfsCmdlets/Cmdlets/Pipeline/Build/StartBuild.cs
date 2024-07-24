using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class StartBuild
    {
        /// <summary>
        /// Specifies the pipeline to start.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path", "Pipeline")]
        public object Definition { get; set; }
    }

    [CmdletController(typeof(WebApiBuild))]
    partial class StartBuildController
    {
        protected override IEnumerable Run()
        {
            var definition = GetItem<BuildDefinitionReference>();

            var client = GetClient<BuildHttpClient>();

            yield return client.QueueBuildAsync(project: Project.Name, build: new WebApiBuild
            {
                Definition = definition
            }).GetResult($"Error queuing build '{definition}'");
        }
    }
}