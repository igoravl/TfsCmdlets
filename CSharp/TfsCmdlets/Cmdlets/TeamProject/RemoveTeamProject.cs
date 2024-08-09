using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Deletes one or more team projects. 
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class RemoveTeamProject
    {
        /// <summary>
        /// Specifies the name of a Team Project to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        public object Project { get; set; }

        /// <summary>
        /// Deletes the team project permanently. When omitted, the team project is moved to a 
        /// "recycle bin" and can be recovered either via UI or by using Undo-TfsTeamProjectRemoval.
        /// </summary>
        [Parameter]
        public SwitchParameter Hard { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WebApiTeamProject), Client=typeof(IProjectHttpClient))]
    partial class RemoveTeamProjectController
    {
        [Import]
        private IOperationsHttpClient OperationsClient { get; set; }
        
        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var tps = Data.GetItems<WebApiTeamProject>();
            var hard = Parameters.Get<bool>(nameof(RemoveTeamProject.Hard));
            var force = Parameters.Get<bool>(nameof(RemoveTeamProject.Force));

            foreach (var tp in tps)
            {
                if (!PowerShell.ShouldProcess($"[Organization: {tpc.DisplayName}]/[Project: {tp.Name}]", "Delete team project")) continue;

                if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete team project '{tp.Name}'?")) continue;

                if (hard && !PowerShell.ShouldContinue(
                    "You are using the -Hard switch. The team project deletion is IRREVERSIBLE " +
                    $"and may cause DATA LOSS. Are you sure you want to proceed with deleting team project '{tp.Name}'")) continue;

                var method = hard ? "Hard" : "Soft";

                Logger.Log($"{method}-deleting team project {tp.Name}");

                var token = Client.QueueDeleteProject(tp.Id, hard).GetResult($"Error queueing team project deletion");

                // Wait for the operation to complete

                var opsToken = OperationsClient.GetOperation(token.Id).GetResult("Error getting operation status");

                while (
                    (opsToken.Status != OperationStatus.Succeeded) &&
                    (opsToken.Status != OperationStatus.Failed) &&
                    (opsToken.Status != OperationStatus.Cancelled))
                {
                    Thread.Sleep(2);
                    opsToken = OperationsClient.GetOperation(token.Id).GetResult("Error getting operation status");
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