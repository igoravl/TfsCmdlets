using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    /// <summary>
    /// Suspends (pauses) a build/pipeline definition.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(BuildDefinitionReference))]
    partial class SuspendBuildDefinition
    {
        /// <summary>
        /// Specifies the pipeline name/path.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public object Definition { get; set; }
    }

    [CmdletController(typeof(BuildDefinitionReference), Client=typeof(IBuildHttpClient))]
    partial class SuspendBuildDefinitionController
    {
        protected override IEnumerable Run()
        {
            var def = Data.GetItem<BuildDefinition>();

            if (def.QueueStatus == DefinitionQueueStatus.Paused)
            {
                Logger.Log($"Build definition '{def.Name}' is already paused.");
                yield return def;
            }

            if (!PowerShell.ShouldProcess(def.Project.Name, $"Pause Build Definition '{def.GetFullPath()}'")) yield break;

            if (def.QueueStatus == DefinitionQueueStatus.Disabled)
            {
                Logger.LogError(new InvalidOperationException($"Build definition '{def.Name}' is disabled. Disabled builds cannot be paused."));
                yield return def;
            }

            var patch = new BuildDefinition()
            {
                Id = def.Id,
                Project = def.Project,
                QueueStatus = DefinitionQueueStatus.Paused,
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