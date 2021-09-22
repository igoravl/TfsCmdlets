using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.Build.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Pipeline.Build
{
    /// <summary>
    /// Gets one or more build/pipeline definitions in a team project.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Suspend, "TfsBuildDefinition", SupportsShouldProcess = true)]
    [OutputType(typeof(BuildDefinitionReference))]
    public class SuspendBuildDefinition : CmdletBase
    {
        /// <summary>
        /// Specifies the pipeline path.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        public object Definition { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        /// <value></value>
        [Parameter()]
        public object Project { get; set; }
    }

    // TODO

    //partial class BuildDefinitionDataService
    //{
    //    protected override BuildDefinition DoSuspendItem()
    //    {
    //        var def = GetItem<BuildDefinition>();
    //        var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>(parameters);

    //        if(def.QueueStatus == DefinitionQueueStatus.Paused)
    //        {
    //            Logger.Log($"Build definition '{def.Name}' is already paused.");
    //            return def;
    //        }

    //        if (!PowerShell.ShouldProcess(def.Project.Name, $"Pause Build Definition '{def.GetFullPath()}'")) return def;

    //        if (def.QueueStatus == DefinitionQueueStatus.Disabled)
    //        {
    //            Logger.LogError(new InvalidOperationException($"Build definition '{def.Name}' is disabled. Disabled builds cannot be paused."));
    //            return def;
    //        }

    //        var patch = new BuildDefinition()
    //        {
    //            Id = def.Id,
    //            Project = def.Project,
    //            QueueStatus = DefinitionQueueStatus.Paused,
    //            Revision = def.Revision,
    //            Repository = def.Repository,
    //            Process = def.Process,
    //            Name = def.Name,
    //        };

    //        return client.UpdateDefinitionAsync(patch)
    //            .GetResult($"Error updating build definition {def.GetFullPath()}");
    //    }
    //}
}