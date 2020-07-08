using System.Collections.Generic;
using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;
using TfsCmdlets.Util;
using System;
using TfsCmdlets.Extensions;
using Microsoft.VisualStudio.Services.Operations;
using System.Threading;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Creates a new team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTeamProject", DefaultParameterSetName = "Get by project", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeamProject))]
    [RequiresVersion(2015)]
    public class NewTeamProject : NewCmdletBase<WebApiTeamProject>
    {
        /// <summary>
        ///  Specifies the name of the new team project.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public override object Project { get; set; }

        /// <summary>
        /// Specifies a description for the new team project.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the source control type to be provisioned initially with the team project. 
        /// Supported types are "Git" and "Tfvc".
        /// </summary>
        [Parameter()]
        [ValidateSet("Git", "Tfvc")]
        public string SourceControl { get; set; }

        /// <summary>
        /// Specifies the process template on which the new team project is based. 
        /// Supported values are the process name or an instance of the
        /// Microsoft.TeamFoundation.Core.WebApi.Process class.
        /// </summary>
        [Parameter()]
        public object ProcessTemplate { get; set; }
    }

    partial class TeamProjectDataService
    {
        protected override WebApiTeamProject DoNewItem()
        {
            var tpc = GetCollection();
            var project = GetParameter<string>(nameof(NewTeamProject.Project));

            if (!ShouldProcess(tpc, $"Create team project '{project}'"))
            {
                return null;
            }

            var processTemplate = GetParameter<object>(nameof(NewTeamProject.ProcessTemplate));
            var description = GetParameter<string>(nameof(NewTeamProject.Description));
            var sourceControl = GetParameter<string>(nameof(NewTeamProject.SourceControl));
            var done = false;

            WebApiProcess template = null;

            while(!done) switch(processTemplate)
            {
                case WebApiProcess p:
                {
                    template = p;
                    done = true;
                    break;
                }
                case string s:
                {
                    template = GetItem<WebApiProcess>();
                    ErrorUtil.ThrowIfNotFound(template, nameof(NewTeamProject.ProcessTemplate), processTemplate);
                    done = true;
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Invalid or non-existent process template '{processTemplate}'");
                }
            }

            var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient>();

            var tpInfo = new WebApiTeamProject() {
                Name = project,
                Description = description,
                Capabilities = new Dictionary<string,Dictionary<string,string>>() {
                    ["versioncontrol"] = new Dictionary<string,string>() {
                        ["sourceControlType"] = sourceControl },
                    ["processTemplate"] = new Dictionary<string, string>() {
                        ["templateTypeId"] = template.Id.ToString()
                    }
                }
            };
            
            // Trigger the project creation

            var token = client.QueueCreateProject(tpInfo)
                .GetResult("Error queueing project creation");

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
                throw new Exception($"Error creating team project {project}: {opsToken.ResultMessage}");
            }

            return GetItem<WebApiTeamProject>();
        }
    }
}