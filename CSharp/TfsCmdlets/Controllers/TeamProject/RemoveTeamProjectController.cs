using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class RemoveTeamProjectController
    {
        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var tps = Data.GetItems<WebApiTeamProject>();
            var hard = Parameters.Get<bool>(nameof(RemoveTeamProject.Hard));
            var force = Parameters.Get<bool>(nameof(RemoveTeamProject.Force));

            var client = Data.GetClient<ProjectHttpClient>();

            foreach (var tp in tps)
            {
                if (!PowerShell.ShouldProcess($"[Organization: {tpc.DisplayName}]/[Project: {tp.Name}]", "Delete team project")) continue;

                if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete team project '{tp.Name}'?")) continue;

                if (hard && !PowerShell.ShouldContinue(
                    "You are using the -Hard switch. The team project deletion is IRREVERSIBLE " +
                    $"and may cause DATA LOSS. Are you sure you want to proceed with deleting team project '{tp.Name}'")) continue;

                var method = hard ? "Hard" : "Soft";

                Logger.Log($"{method}-deleting team project {tp.Name}");

                var token = client.QueueDeleteProject(tp.Id, hard).GetResult($"Error queueing team project deletion");

                // Wait for the operation to complete

                var opsClient = Data.GetClient<OperationsHttpClient>();
                var opsToken = opsClient.GetOperation(token.Id).GetResult("Error getting operation status");

                while (
                    (opsToken.Status != OperationStatus.Succeeded) &&
                    (opsToken.Status != OperationStatus.Failed) &&
                    (opsToken.Status != OperationStatus.Cancelled))
                {
                    Thread.Sleep(2);
                    opsToken = opsClient.GetOperation(token.Id).GetResult("Error getting operation status");
                }

                if (opsToken.Status != OperationStatus.Succeeded)
                {
                    throw new Exception($"Error deleting team project {tp.Name}: {opsToken.ResultMessage}");
                }
            }

            return null;
        }
    }
}