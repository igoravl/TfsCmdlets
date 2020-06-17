using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Gets the contents of one or more test plans.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTestPlan")]
    [OutputType(typeof(TestPlan))]
    public class GetTestPlan : BaseCmdlet
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         # Specifies the test plan name. Wildcards are supported
        //         [Parameter(Position=0)]
        //         [SupportsWildcards()]
        //         [Alias("Id")]
        //         [Alias("Name")]
        //         public object TestPlan { get; set; } = "*";

        //         # Specifices the plan"s owner name
        //         [Parameter()]
        //         public string Owner { get; set; }

        //         # Get only basic properties of the test plan
        //         [Parameter()]
        //         public SwitchParameter NoPlanDetails { get; set; }

        //         # Get just the active plans
        //         [Parameter()]
        //         public SwitchParameter FilterActivePlans { get; set; }

        //         # Specifies the team project
        //         [Parameter(ValueFromPipeline=true)]
        //         public object Project { get; set; }

        //         # Specifies the collection / organization
        //         [Parameter()]
        //         [Alias("Organization")]
        //         public object Collection { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
        //     }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         if (TestPlan is Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlan)
        //         {
        //             WriteObject(TestPlan); return;
        //         }

        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
        //         var client = GetClient<Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestPlanHttpClient>();

        //         WriteObject(client.GetTestPlansAsync(); return;
        //             tp.Name, Owner, null, 
        //             (! NoPlanDetails.IsPresent), 
        //             FilterActivePlans.IsPresent).Result | Where-Object Name -like TestPlan
        //     }
        // }
    }
}
