using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    [Cmdlet(VerbsCommon.Remove, "TfsTestPlan", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RemoveTestPlan : BaseCmdlet
    {
        /*
                [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
                [Alias("id")]
                [ValidateNotNull()]
                public object TestPlan { get; set; }

                [Parameter()]
        public object Project,

                [Parameter()]
                public object Collection { get; set; }

            protected override void BeginProcessing()
            {
                #_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
                #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.TestManagement.WebApi"
                ns = "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
            }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                plan = Get-TfsTestPlan -TestPlan TestPlan -Project Project -Collect Collection

                if(! plan)
                {
                    throw new Exception($"Invalid or non-existent test plan {TestPlan}")
                }

                if (! Project)
                {
                    Project = plan.Project.Name
                }

                GET_TEAM_PROECT(tp,tpc)
                client = Get-TfsRestClient $"{ns}.TestPlanHttpClient" -Collection tpc

                if (ShouldProcess($"Plan {{plan}.Id} ("$(plan.Name)")", "Remove test plan"))
                {
                    task = client.DeleteTestPlanAsync(tp.Name, plan.Id)

                    result = task.Result; if(task.IsFaulted) { _throw new Exception("Error deleting test plan" task.Exception.InnerExceptions })
                }
            }
        }
        */
    }
}
