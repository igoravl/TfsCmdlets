using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Deletes one or more team projects. 
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamProject", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class RemoveTeamProject : RemoveCmdletBase<WebApiTeamProject>
    {
        /// <summary>
        /// Specifies the name of a Team Project to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        public override object Project { get; set; }

        /// <summary>
        /// Deletes the team project permanently. When omitted, the team project is moved to a 
        /// "recycle bin" and can be recovered either via UI or by using Undo-TfsTeamProjectRemoval.
        /// </summary>
        [Parameter()]
        public SwitchParameter Hard { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class TeamProjectDataService
    {
        protected override void DoRemoveItem()
        {
            var tpc = GetCollection();
            var tps = GetItems<WebApiTeamProject>();
            var hard = GetParameter<bool>(nameof(RemoveTeamProject.Hard));
            var force = GetParameter<bool>(nameof(RemoveTeamProject.Force));

            foreach (var tp in tps)
            {
                var client = GetClient<ProjectHttpClient>();

                if (!ShouldProcess(tpc, $"Delete team project '{tp.Name}'")) continue;

                if (!force && !ShouldContinue($"Are you sure you want to delete team project '{tp.Name}'?")) continue;

                if (hard && !force && !ShouldContinue("You are using the -Hard switch. The team project deletion is IRREVERSIBLE " +
                    $"and may cause DATA LOSS. Are you sure you want to proceed with deleting team project '{tp.Name}'")) continue;

                var method = hard ? "Hard" : "Soft";

                this.Log($"{method}-deleting team project {tp.Name}");

                var token = client.QueueDeleteProject(tp.Id, hard).GetResult($"Error queueing team project deletion");

                // Wait for the operation to complete

                var opsClient = GetClient<OperationsHttpClient>();
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
        }
    }
}