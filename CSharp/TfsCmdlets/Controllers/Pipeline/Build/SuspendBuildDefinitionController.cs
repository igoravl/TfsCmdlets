using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Controllers.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [CmdletController(typeof(BuildDefinitionReference))]
    partial class SuspendBuildDefinitionController
    {
        public override IEnumerable<BuildDefinitionReference> Invoke()
        {
            var def = Data.GetItem<BuildDefinition>();
            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

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

            yield return client.UpdateDefinitionAsync(patch)
                .GetResult($"Error updating build definition {def.GetFullPath()}");
        }
    }
}