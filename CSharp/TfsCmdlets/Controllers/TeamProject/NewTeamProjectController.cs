using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Cmdlets.TeamProject;

namespace TfsCmdlets.Controllers.TeamProject
{
    [CmdletController(typeof(WebApiTeamProject))]
    partial class NewTeamProjectController
    {
        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection();
            var project = Parameters.Get<string>(nameof(NewTeamProject.Project));

            if (!PowerShell.ShouldProcess(tpc, $"Create team project '{project}'"))
            {
                yield break;
            }

            var processTemplate = Parameters.Get<object>(nameof(NewTeamProject.ProcessTemplate));
            processTemplate ??= Data.GetItem<Process>(new { ProcessTemplate = "*", Default = true });

            var description = Parameters.Get<string>(nameof(Cmdlets.TeamProject.NewTeamProject.Description));
            var sourceControl = Parameters.Get<string>(nameof(Cmdlets.TeamProject.NewTeamProject.SourceControl));

            var template = processTemplate switch
            {
                Process p => p,
                string s => Data.GetItem<Process>(new { Process = s }),
                _ => throw new ArgumentException($"Invalid or non-existent process template '{processTemplate}'")
            };

            var client = Data.GetClient<ProjectHttpClient>();

            var tpInfo = new WebApiTeamProject
            {
                Name = project,
                Description = description,
                Capabilities = new Dictionary<string, Dictionary<string, string>>
                {
                    ["versioncontrol"] = new Dictionary<string, string>
                    {
                        ["sourceControlType"] = sourceControl
                    },
                    ["processTemplate"] = new Dictionary<string, string>
                    {
                        ["templateTypeId"] = template.Id.ToString()
                    }
                }
            };

            // Trigger the project creation

            var token = client.QueueCreateProject(tpInfo)
                .GetResult("Error queueing project creation");

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
                throw new Exception($"Error creating team project {project}: {opsToken.ResultMessage}");
            }

            yield return Data.GetItem<WebApiTeamProject>();;
        }
    }
}