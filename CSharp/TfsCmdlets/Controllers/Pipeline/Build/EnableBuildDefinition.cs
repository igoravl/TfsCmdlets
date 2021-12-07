using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Pipeline.Build
{
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class EnableBuildDefinition
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