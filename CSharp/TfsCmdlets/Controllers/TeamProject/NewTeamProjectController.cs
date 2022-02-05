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
            var client = GetClient<ProjectHttpClient>();

            foreach (var project in Project)
            {
                if (!PowerShell.ShouldProcess(Collection, $"Create team project '{project}'")) continue;

                var template = ProcessTemplate switch
                {
                    Process p => p,
                    string s => Data.GetItem<Process>(new { Process = s }),
                    null => GetItem<Process>(new { Default = true }),
                    _ => throw new ArgumentException($"Invalid or non-existent process template '{ProcessTemplate}'")
                };

                var tpInfo = new WebApiTeamProject
                {
                    Name = project,
                    Description = Description,
                    Capabilities = new Dictionary<string, Dictionary<string, string>>
                    {
                        ["versioncontrol"] = new Dictionary<string, string>
                        {
                            ["sourceControlType"] = SourceControl
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
                    Logger.LogError(new Exception($"Error creating team project {project}: {opsToken.ResultMessage}"));
                    continue;
                }

                yield return GetItem<WebApiTeamProject>();
            }
        }
    }
}