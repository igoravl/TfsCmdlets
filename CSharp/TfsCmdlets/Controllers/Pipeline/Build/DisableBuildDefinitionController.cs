using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Controllers.Pipeline.Build
{
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class DisableBuildDefinitionController
    {
        public override IEnumerable<BuildDefinitionReference> Invoke()
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