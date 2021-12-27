using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Controllers.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class ResumeBuildDefinitionController
    {
        public override IEnumerable<BuildDefinitionReference> Invoke()
        {

            var def = Data.GetItem<BuildDefinition>();
            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

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

            yield return client.UpdateDefinitionAsync(patch)
                .GetResult($"Error updating build definition {def.GetFullPath()}");
        }
    }
}