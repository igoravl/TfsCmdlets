using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Definition
{
    /// <summary>
    /// Disables a build/pipeline definition.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(BuildDefinitionReference))]
    partial class DisableBuildDefinition
    {
        /// <summary>
        /// Specifies the pipeline name/path.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public object Definition { get; set; }
    }

    [CmdletController(typeof(BuildDefinitionReference))]
    partial class DisableBuildDefinitionController
    {
        protected override IEnumerable Run()
        {
            var def = Data.GetItem<BuildDefinition>();
            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

            if (def.QueueStatus == DefinitionQueueStatus.Disabled)
            {
                Logger.Log($"Build definition '{def.Name}' is already disabled.");
                yield return def;
            }

            if (!PowerShell.ShouldProcess(def.Project.Name, $"Disable Build Definition '{def.GetFullPath()}'")) yield break;

            var patch = new BuildDefinition()
            {
                Id = def.Id,
                Project = def.Project,
                QueueStatus = DefinitionQueueStatus.Disabled,
                Revision = def.Revision,
                Repository = def.Repository,
                Process = def.Process,
                Name = def.Name,
            };

            yield return client.UpdateDefinitionAsync(patch)
                .GetResult($"Error updating build definition {def.GetFullPath()}");
        }
    }
}