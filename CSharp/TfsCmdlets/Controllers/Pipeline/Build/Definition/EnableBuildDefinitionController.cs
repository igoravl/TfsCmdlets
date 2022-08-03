using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Controllers.Pipeline.Build.Definition
{
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class EnableBuildDefinitionController
    {
        protected override IEnumerable Run()
        {
            var def = Data.GetItem<BuildDefinition>();
            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

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

            yield return client.UpdateDefinitionAsync(patch)
                .GetResult($"Error updating build definition {def.GetFullPath()}");
        }
    }
}