using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Renames a team project. 
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsTeamProject", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class RenameTeamProject : RenameCmdletBase<WebApiTeamProject>
    {
        /// <summary>
        /// Specifies the name of a Team Project to rename.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public override object Project { get; set; }

        /// <summary>
        /// Forces the renaming of the team project. When omitted, the command prompts for 
        /// confirmation prior to renaming the team project.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class TeamProjectDataService
    {
        protected override WebApiTeamProject DoRenameItem()
        {
            var tpc = Collection;
            var tp = GetItem<WebApiTeamProject>();
            var newName = GetParameter<string>("NewName");
            var force = GetParameter<bool>(nameof(RenameTeamProject.Force));

            if (!ShouldProcess(tpc, $"Rename team project '{tp.Name}' to ")) return null;

            if (!force && !ShouldContinue($"Renaming this project is a disruptive action that can " +
                "significantly impact all members. The new name will update across all version control " +
                "paths, work items, queries, URLs and any other project content. Project members may " +
                "need to react and all currently running builds may fail as a result of this change. " +
                $"Are you sure you want to rename team project '{tp.Name}'?")) return null;

            this.Log($"Renaming team project '{tp.Name}' to '{newName}'");

            var client = GetService<IRestApiService>();

            var token = client.QueueOperationAsync(
                GetCollection(),
                $"/_apis/projects/{tp.Id}",
                "PATCH",
                $"{{\"name\":\"{newName}\"}}")
                .GetResult($"Error renaming team project '{tp.Name}'");

            // Wait for the operation to complete

            var opsClient = GetClient<OperationsHttpClient>();
            var opsToken = opsClient.GetOperation(token.Id)
                .GetResult("Error getting operation status");

            while (
                (opsToken.Status != OperationStatus.Succeeded) &&
                (opsToken.Status != OperationStatus.Failed) && 
                (opsToken.Status != OperationStatus.Cancelled))
            {
                Thread.Sleep(2);
                opsToken = opsClient.GetOperation(token.Id)
                    .GetResult("Error getting operation status");
            }

            if (opsToken.Status != OperationStatus.Succeeded)
            {
                throw new Exception($"Error renaming team project '{tp.Name}': {opsToken.ResultMessage}");
            }

            return GetItem<WebApiTeamProject>(new { Project = tp.Id });
        }
    }
}