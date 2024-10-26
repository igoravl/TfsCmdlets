using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline
{
    /// <summary>
    /// Resumes (unpauses) a previously suspended pipeline.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(BuildDefinitionReference))]
    partial class ResumePipeline
    {
        /// <summary>
        /// Specifies the pipeline name/path.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public object Definition { get; set; }
    }

    [CmdletController(typeof(BuildDefinitionReference), Client=typeof(IBuildHttpClient))]
    partial class ResumePipelineController
    {
        protected override IEnumerable Run()
        {

            var def = Data.GetItem<BuildDefinition>();

            if (def.QueueStatus == DefinitionQueueStatus.Enabled)
            {
                Logger.Log($"Build definition '{def.Name}' is already enabled.");
                yield return def;
            }

            if (!PowerShell.ShouldProcess(def.Project.Name, $"Resume Build Definition '{def.GetFullPath()}'")) yield break;

            if (def.QueueStatus == DefinitionQueueStatus.Disabled)
            {
                Logger.LogError(new InvalidOperationException($"Build definition '{def.Name}' is disabled. Disabled builds cannot be resumed. Use Enable-TfsBuildDefinition instead."));
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