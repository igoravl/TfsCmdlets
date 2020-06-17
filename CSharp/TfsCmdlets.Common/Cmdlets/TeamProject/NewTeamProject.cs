/*
.SYNOPSIS
Creates a new team project. 

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

*/

using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Creates a new team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTeamProject", DefaultParameterSetName = "Get by project", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeamProject))]
    public class NewTeamProject : NewCmdletBase<WebApiTeamProject>
    {
        /// <summary>
        ///  Specifies the name of the new team project.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public new string Project { get; set; }

        /// <summary>
        /// Specifies a description for the new team project.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        [Parameter()]
        [ValidateSet("Git", "Tfvc")]
        public string SourceControl { get; set; }

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

            /*                
                            template = Get-TfsProcessTemplate -Collection tpc -Name ProcessTemplate
                            var client = GetClient<Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient>();

                            tpInfo = new Microsoft.TeamFoundation.Core.WebApi.TeamProject()
                            tpInfo.Name = Project
                            tpInfo.Description = Description
                            tpInfo.Capabilities = new System.Collections.Generic.Dictionary[[string],System.Collections.Generic.Dictionary[[string],[string]]]()

                            tpInfo.Capabilities.Add("versioncontrol", (new System.Collections.Generic.Dictionary[[string],[string]]()))
                            tpInfo.Capabilities["versioncontrol"].Add("sourceControlType", SourceControl)

                            tpInfo.Capabilities.Add("processTemplate", (new System.Collections.Generic.Dictionary[[string],[string]]()))
                            tpInfo.Capabilities["processTemplate"].Add("templateTypeId", ([xml]template.Metadata).metadata.version.type)

                            # Trigger the project creation

                            token = client.QueueCreateProject(tpInfo).Result

                            if (! token)
                            {
                                throw new Exception($"Error queueing team project creation: {{client}.LastResponseContext.Exception.Message}")
                            }

                            # Wait for the operation to complete

                            var client = GetClient<Microsoft.VisualStudio.Services.Operations.OperationsHttpClient>();

                            opsToken = operationsClient.GetOperation(token.Id).Result

                            while (
                                (opsToken.Status != Microsoft.VisualStudio.Services.Operations.OperationStatus.Succeeded) -and
                                (opsToken.Status != Microsoft.VisualStudio.Services.Operations.OperationStatus.Failed) && 
                                (opsToken.Status != Microsoft.VisualStudio.Services.Operations.OperationStatus.Cancelled))
                            {
                                Start-Sleep -Seconds 2
                                opsToken = operationsClient.GetOperation(token.Id).Result
                            }

                            if (opsToken.Status != Microsoft.VisualStudio.Services.Operations.OperationStatus.Succeeded)
                            {
                                throw new Exception($"Error creating team project {Project}")
                            }

                            # Force a metadata cache refresh prior to retrieving the newly created project

                            wiStore = tpc.GetService([type]"Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")
                            wiStore.RefreshCache()

                            tp = this.GetProject();

                            if (Passthru)
                            {
                                WriteObject(tp); return;
                            }
                        }
                    }
                    */
            return null;
        }
    }
}