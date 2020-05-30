/*
.SYNOPSIS
Creates a new team project. 

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet(VerbsCommon.New, "TeamProject", DefaultParameterSetName = "Get by project", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.Project))]
    public class NewTeamProject : BaseCmdlet
    {
        /*
                [Parameter(Position=0, Mandatory=true)]
        public string Project,

                [Parameter(ValueFromPipeline=true, Position=1)]
                public object Collection { get; set; }

                public string Description { get; set; }

                [string]
                [ValidateSet("Git", "TFVC")]
                SourceControl,

                public object ProcessTemplate { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if(! ShouldProcess(Project, "Create team project"))
                {
                    return
                }

                tpc = Get-TfsTeamProjectCollection Collection
                template = Get-TfsProcessTemplate -Collection tpc -Name ProcessTemplate
                var client = tpc.GetClient<Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient>();

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

                var client = tpc.GetClient<Microsoft.VisualStudio.Services.Operations.OperationsHttpClient>();

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
    }
}
