using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Creates a new team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, RequiresVersion = 2015, DefaultParameterSetName = "Get by project", SupportsShouldProcess = true, 
     OutputType = typeof(WebApiTeamProject))]
    partial class NewTeamProject
    {
        /// <summary>
        ///  Specifies the name of the new team project.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Project { get; set; }

        /// <summary>
        /// Specifies a description for the new team project.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the source control type to be provisioned initially with the team project. 
        /// Supported types are "Git" and "Tfvc".
        /// </summary>
        [Parameter]
        [ValidateSet("Git", "Tfvc")]
        public string SourceControl { get; set; } = "Git";

        /// <summary>
        /// Specifies the process template on which the new team project is based. 
        /// Supported values are the process name or an instance of the
        /// Microsoft.TeamFoundation.Core.WebApi.Process class.
        /// </summary>
        [Parameter]
        public object ProcessTemplate { get; set; }
    }

    [CmdletController(typeof(WebApiTeamProject), Client=typeof(IProjectHttpClient))]
    partial class NewTeamProjectController
    {
        [Import]
        private IAsyncOperationAwaiter AsyncAwaiter { get; }

        protected override IEnumerable Run()
        {
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

                var result = AsyncAwaiter.Wait(Client.QueueCreateProject(tpInfo), "Error queueing project creation");

                if (result.Status != OperationStatus.Succeeded)
                {
                    Logger.LogError(new Exception($"Error creating team project '{project}': {result.ResultMessage}"));
                    continue;
                }

                yield return GetItem<WebApiTeamProject>();
            }
        }
    }
}