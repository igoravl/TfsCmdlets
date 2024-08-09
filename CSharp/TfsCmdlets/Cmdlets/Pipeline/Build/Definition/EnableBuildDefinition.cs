using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    /// <summary>
    /// Enables a previously disabled build/pipeline definition.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(BuildDefinitionReference))]
    partial class EnableBuildDefinition
    {
        /// <summary>
        /// Specifies the pipeline name/path.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public object Definition { get; set; }
    }

    [CmdletController(typeof(BuildDefinitionReference), Client=typeof(IBuildHttpClient))]
    partial class EnableBuildDefinitionController
    {
        protected override IEnumerable Run()
        {
            var def = Data.GetItem<BuildDefinition>();

            if (def.QueueStatus == DefinitionQueueStatus.Enabled)
            {
                Logger.Log($"Build definition '{def.Name}' is already enabled.");
                yield return def;
            }

            if (!PowerShell.ShouldProcess(def.Project.Name, $"Enable Build Definition '{def.GetFullPath()}'")) yield break;

            if (def.QueueStatus == DefinitionQueueStatus.Paused)
            {
                Logger.LogError(new InvalidOperationException($"Build definition '{def.Name}' is paused, not disabled. To re-enable a paused pipeline, use Resume-TfsBuildDefinition instead."));
                yield return def;
            }

            var patch = new BuildDefinition()
            {
                Id = def.Id,
                Project = def.Project,
                QueueStatus = DefinitionQueueStatus.Enabled,
                Revision = def.Revision,
                Repository = def.Repository,
                Process = def.Process,
                Name = def.Name,
            };

            yield return Client.UpdateDefinitionAsync(patch)
                .GetResult($"Error updating build definition {def.GetFullPath()}");
        }
    }
}