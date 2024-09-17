using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Run
{
    /// <summary>
    /// Cancels (stops) a running pipeline.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(BuildDefinitionReference))]
    partial class StopPipelineRun
    {
        /// <summary>
        /// Specifies the pipeline to start.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ParameterSetName = "By Pipeline", Mandatory = true)]
        [Alias("Id")]
        public object Pipeline { get; set; }
    }

    [CmdletController(typeof(WebApiBuild), Client = typeof(IBuildHttpClient))]
    partial class StopPipelineRunController
    {
        protected override IEnumerable Run()
        {
            foreach(var build in Items)
            {
                if(!PowerShell.ShouldProcess(Project, $"Stop build {build.Id} ({build.BuildNumber})")) continue;
                {
                    build.Status = BuildStatus.Cancelling;

                    yield return Client.UpdateBuildAsync(build)
                    .GetResult($"Error stopping build '{build.Id}'");
                }
                build.Status = BuildStatus.Cancelling;

                yield return Client.UpdateBuildAsync(build)
                .GetResult($"Error stopping build '{build.Id}'");
            }
        }
    }
}