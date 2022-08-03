using System.Threading;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class RenameTeamProjectController
    {

        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var newName = Parameters.Get<string>(nameof(RenameTeamProject.NewName));
            var force = Parameters.Get<bool>(nameof(RenameTeamProject.Force));

            if (!PowerShell.ShouldProcess(tpc, $"Rename team project '{tp.Name}' to ")) return null;

            if (!force && !PowerShell.ShouldContinue(
                $"Renaming this project is a disruptive action that can " +
                   "significantly impact all members. The new name will update across all version control " +
                   "paths, work items, queries, URLs and any other project content. Project members may " +
                   "need to react and all currently running builds may fail as a result of this change. " +
                   $"Are you sure you want to rename team project '{tp.Name}'?")) return null;

            Logger.Log($"Renaming team project '{tp.Name}' to '{newName}'");

            var token = RestApiService.QueueOperationAsync(
                tpc,
                $"/_apis/projects/{tp.Id}",
                "PATCH",
                $"{{\"name\":\"{newName}\"}}")
                .GetResult($"Error renaming team project '{tp.Name}'");

            // Wait for the operation to complete

            var opsClient = Data.GetClient<OperationsHttpClient>();
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

            return Data.GetItems<WebApiTeamProject>(new { Project = tp.Id });
        }

        [Import]
        private IRestApiService RestApiService { get; }
    }
}