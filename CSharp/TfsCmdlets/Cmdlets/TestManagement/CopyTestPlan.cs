using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Clone a test plan and, optionally, its test suites and test cases.
    /// </summary>
    /// <remarks>
    ///   <para>The Copy-TfsTestPlan copies ("clones") a test plan to help duplicate test suites and/or test cases. Cloning is useful if you want to branch your application into two versions. After copying, the tests for the two versions can be changed without affecting each other.</para>
    ///   <para>When you clone a test suite, the following objects are copied from the source test plan to the destination test plan:</para>
    ///   <para>  * Test cases (note: Each new test case retains its shared steps. A link is made between the source and new test cases. The new test cases do not have test runs, bugs, test results, and build information);</para>
    ///   <para>  * Shared steps referenced by cloned test cases;</para>
    ///   <para>  * Test suites (note: The following data is retained - Names and hierarchical structure of the test suites; Order of the test cases; Assigned testers; Configurations);</para>
    ///   <para>  * Action Recordings linked from a cloned test case;</para>
    ///   <para>  * Links and Attachments;</para>
    ///   <para>  * Test configuration.</para>
    ///   <para>The items below are only copied when using -CloneRequirements:</para>
    ///   <para>  * Requirements-based suites;</para>
    ///   <para>  * Requirements work items (product backlog items or user stories);</para>
    ///   <para>  * Bug work items, when in a project that uses the Scrum process template or any other project in which the Bug work item type is in the Requirements work item category. In other projects, bugs are not cloned.</para>
    /// </remarks>
	/// <example>
	/// <code>Copy-TfsTestPlan -TestPlan "My test plan" -Project "SourceProject" -Destination "TargetProject" -NewName "My new test plan"</code>
	/// </example>
    [Cmdlet(VerbsCommon.Copy, "TfsTestPlan")]
    [OutputType(typeof(TestPlan))]
    public class CopyTestPlan : CmdletBase
    {
        /// <summary>
        /// Specifies the name of the test plan to clone.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("id")]
        [ValidateNotNull()]
        public object TestPlan { get; set; }

        /// <summary>
        /// Specifies the name of the new test plan.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string NewName { get; set; }

        /// <summary>
        /// Specifies the name of the team project where the test plan will be cloned into. 
        /// When omitted, the test plan is cloned into the same team project of the original 
        /// test plan.
        /// </summary>
        [Parameter()]
        public object Destination { get; set; }

        /// <summary>
        /// Specifies the area path where the test plan will be cloned into. 
        /// When omitted, the test plan is cloned into the same area path of the original 
        /// test plan.
        /// </summary>
        [Parameter()]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the iteration path where the test plan will be cloned into. 
        /// When omitted, the test plan is cloned into the same iteration path of 
        /// the original test plan.
        /// </summary>
        [Parameter()]
        public string IterationPath { get; set; }

        /// <summary>
        /// Clones all the referenced test cases. When omitted, only the test plan is 
        /// cloned; the original test cases are only referenced in the new plan, not duplicated.
        /// </summary>
        [Parameter()]
        public SwitchParameter DeepClone { get; set; }

        /// <summary>
        /// Clone all test suites recursively.
        /// </summary>
        [Parameter()]
        public SwitchParameter Recurse { get; set; }

        /// <summary>
        /// Copies ancestor hierarchy.
        /// </summary>
        [Parameter()]
        public SwitchParameter CopyAncestorHierarchy { get; set; }

        /// <summary>
        /// Clones requirements referenced by the test plan.
        /// </summary>
        [Parameter()]
        public SwitchParameter CloneRequirements { get; set; }

        /// <summary>
        /// Specifies the name of the workitem type of the clone.
        /// </summary>
        [Parameter()]
        public string DestinationWorkItemType { get; set; } = "Test Case";

        /// <summary>
        /// Clones only the specified suites.
        /// </summary>
        [Parameter()]
        public int[] SuiteIds { get; set; }

        /// <summary>
        /// Specifies the comment of the Related link that is created ato point 
        /// to the original test plan.
        /// </summary>
        [Parameter()]
        public string RelatedLinkComment { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        [ValidateSet("Original", "Copy", "None")]
        public string Passthru { get; set; } = "None";

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //     protected override void BeginProcessing()
        //     {
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
        //         #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.TestManagement.WebApi"
        //         ns = "Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi"
        //     }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         plan = Get-TfsTestPlan -TestPlan TestPlan -Project Project -Collect Collection

        //         if(! plan)
        //         {
        //             throw new Exception($"Invalid or non-existent test plan {TestPlan}")
        //         }

        //         destTp = Get-TfsTeamProject -Project DestinationProject -Collection Collection

        //         if(! destTp)
        //         {
        //             throw new Exception($"Invalid or non-existent team project {DestinationProject}")
        //         }

        //         if (! Project)
        //         {
        //             Project = plan.Project.Name
        //         }

        //         tp = this.GetProject();

        //         if(! tp)
        //         {
        //             throw new Exception($"Invalid or non-existent team project {Project}")
        //         }

        //         if (! NewName)
        //         {
        //             if(tp.Name != destTp.Name)
        //             {
        //                 NewName = plan.Name
        //             }
        //             else
        //             {
        //                 NewName = $"{{plan}.Name} (cloned $(DateTime.Now.ToShortDateString()))"
        //             }
        //         }

        //         if (! AreaPath)
        //         {
        //             AreaPath = destTp.Name
        //         }

        //         if (! IterationPath)
        //         {
        //             IterationPath = destTp.Name
        //         }

        //         tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
        //         client = Get-TfsRestClient $"{ns}.TestPlanHttpClient" -Collection tpc

        //         cloneParams = New-Object $"{ns}.CloneTestPlanParams" -Property @{
        //             sourceTestPlan = New-Object $"{ns}.SourceTestPlanInfo" -Property @{
        //                 Id = plan.Id
        //             };
        //             destinationTestPlan = New-Object $"{ns}.DestinationTestPlanCloneParams" -Property @{
        //                 Project = destTp.Name;
        //                 Name = NewName;
        //                 AreaPath = AreaPath;
        //                 Iteration = IterationPath
        //             };
        //             cloneOptions = new Microsoft.TeamFoundation.TestManagement.WebApi.CloneOptions() -Property @{
        //                 RelatedLinkComment = RelatedLinkComment;
        //                 CopyAllSuites = CopyAllSuites.IsPresent;
        //                 CopyAncestorHierarchy = CopyAncestorHierarchy;
        //                 DestinationWorkItemType = DestinationWorkItemType;
        //                 CloneRequirements = CloneRequirements;
        //                 OverrideParameters = _NewDictionary @([string],[string]) @{
        //                     "System.AreaPath" = AreaPath;
        //                     "System.IterationPath" = IterationPath
        //                 }
        //             }
        //         }

        //         task = client.CloneTestPlanAsync(cloneParams, tp.Name, DeepClone.IsPresent)

        //         result = task.Result; if(task.IsFaulted) { _throw new Exception("Error cloning test plan" task.Exception.InnerExceptions })

        //         opInfo = result

        //         do
        //         {
        //             Start-Sleep -Seconds 1
        //             opInfo = client.GetCloneInformationAsync(tp.Name, opInfo.CloneOperationResponse.opId)
        //         }
        //         while (opInfo.CloneOperationResponse.CloneOperationState -match "Queued|InProgress")

        //         if (opInfo.CloneOperationResponse.CloneOperationState == "Failed")
        //         {
        //             throw new Exception($"Error cloning test plan "{{plan}.Name}": $(opInfo.CloneOperationResponse.Message)")
        //         }
        //         else
        //         {
        //             copy = opInfo.DestinationTestPlan	
        //         }

        //         if (Passthru = = "Original")
        //         {
        //             WriteObject(plan); return;
        //         }

        //         if(Passthru = = "Copy")
        //         {
        //             WriteObject(copy); return;
        //         }
        //     }
        // }
    }
}