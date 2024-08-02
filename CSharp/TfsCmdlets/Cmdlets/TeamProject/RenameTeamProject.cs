using System.Management.Automation;
using System.Threading;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Renames a team project. 
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class RenameTeamProject
    {
        /// <summary>
        /// Specifies the name of a Team Project to rename.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// Forces the renaming of the team project. When omitted, the command prompts for 
        /// confirmation prior to renaming the team project.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WebApiTeamProject), Client=typeof(IOperationsHttpClient))]
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

            var opsToken = Client.GetOperation(token.Id)
                .GetResult("Error getting operation status");

            while (
                (opsToken.Status != OperationStatus.Succeeded) &&
                (opsToken.Status != OperationStatus.Failed) &&
                (opsToken.Status != OperationStatus.Cancelled))
            {
                Thread.Sleep(2);
                opsToken = Client.GetOperation(token.Id)
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