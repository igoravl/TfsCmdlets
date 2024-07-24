using System.Management.Automation;
using System.Threading;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Cmdlets.TestManagement;

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
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(TestPlan))]
    partial class CopyTestPlan
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
        [Parameter]
        public object Destination { get; set; }

        /// <summary>
        /// Specifies the area path where the test plan will be cloned into. 
        /// When omitted, the test plan is cloned into the same area path of the original 
        /// test plan.
        /// </summary>
        [Parameter]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the iteration path where the test plan will be cloned into. 
        /// When omitted, the test plan is cloned into the same iteration path of 
        /// the original test plan.
        /// </summary>
        [Parameter]
        public string IterationPath { get; set; }

        /// <summary>
        /// Clones all the referenced test cases. When omitted, only the test plan is 
        /// cloned; the original test cases are only referenced in the new plan, not duplicated.
        /// </summary>
        [Parameter]
        public SwitchParameter DeepClone { get; set; }

        /// <summary>
        /// Clone all test suites recursively.
        /// </summary>
        [Parameter]
        public SwitchParameter Recurse { get; set; }

        /// <summary>
        /// Copies ancestor hierarchy.
        /// </summary>
        [Parameter]
        public SwitchParameter CopyAncestorHierarchy { get; set; }

        /// <summary>
        /// Clones requirements referenced by the test plan.
        /// </summary>
        [Parameter]
        public SwitchParameter CloneRequirements { get; set; }

        /// <summary>
        /// Specifies the name of the workitem type of the clone.
        /// </summary>
        [Parameter]
        public string DestinationWorkItemType { get; set; } = "Test Case";

        /// <summary>
        /// Clones only the specified suites.
        /// </summary>
        [Parameter]
        public int[] SuiteIds { get; set; }

        /// <summary>
        /// Specifies the comment of the Related link that is created ato point 
        /// to the original test plan.
        /// </summary>
        [Parameter]
        public string RelatedLinkComment { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        [ValidateSet("Original", "Copy", "None")]
        public string Passthru { get; set; } = "None";
    }

    [CmdletController(typeof(TestPlan))]
    partial class CopyTestPlanController
    {
        protected override IEnumerable Run()
        {
            var plan = Data.GetItem<TestPlan>();
            var destinationProject = Parameters.Get<string>(nameof(CopyTestPlan.Destination));
            var destTp = Data.GetItem<WebApiTeamProject>(new { Project = destinationProject });
            var tp = Data.GetItem<WebApiTeamProject>(new { Project = plan.Project.Name });
            var newName = Parameters.Get<string>(nameof(CopyTestPlan.NewName), $"{plan.Name}{(tp.Name.Equals(destTp.Name, StringComparison.OrdinalIgnoreCase) ? $" (cloned {DateTime.Now.ToShortDateString()})" : string.Empty)}");
            var areaPath = Parameters.Get<string>(nameof(CopyTestPlan.AreaPath), destTp.Name);
            var iterationPath = Parameters.Get<string>(nameof(CopyTestPlan.IterationPath), destTp.Name);
            var deepClone = Parameters.Get<bool>(nameof(CopyTestPlan.DeepClone));
            var passthru = Parameters.Get<string>(nameof(CopyTestPlan.Passthru));
            var relatedLinkComment = Parameters.Get<string>(nameof(CopyTestPlan.RelatedLinkComment));
            var copyAllSuites = Parameters.Get<bool>(nameof(CopyTestPlan.Recurse));
            var copyAncestorHierarchy = Parameters.Get<bool>(nameof(CopyTestPlan.CopyAncestorHierarchy));
            var destinationWorkItemType = Parameters.Get<string>(nameof(CopyTestPlan.DestinationWorkItemType));
            var cloneRequirements = Parameters.Get<bool>(nameof(CopyTestPlan.CloneRequirements));

            var client = Data.GetClient<TestPlanHttpClient>();

            var cloneParams = new CloneTestPlanParams()
            {
                sourceTestPlan = new SourceTestPlanInfo()
                {
                    id = plan.Id
                },
                destinationTestPlan = new DestinationTestPlanCloneParams()
                {
                    Project = destTp.Name,
                    Name = newName,
                    AreaPath = areaPath,
                    Iteration = iterationPath
                },
                cloneOptions = new Microsoft.TeamFoundation.TestManagement.WebApi.CloneOptions()
                {
                    RelatedLinkComment = relatedLinkComment,
                    CopyAllSuites = copyAllSuites,
                    CopyAncestorHierarchy = copyAncestorHierarchy,
                    DestinationWorkItemType = destinationWorkItemType,
                    CloneRequirements = cloneRequirements,
                    OverrideParameters = new Dictionary<string, string>()
                    {
                        ["System.AreaPath"] = areaPath,
                        ["System.IterationPath"] = iterationPath
                    }
                }
            };

            var result = client.CloneTestPlanAsync(cloneParams, tp.Name, deepClone)
                .GetResult($"Error cloning test plan '{plan.Name}' to '{destTp.Name}'");

            var opInfo = result;

            do
            {
                Thread.Sleep(5000);
                opInfo = client.GetCloneInformationAsync(tp.Name, opInfo.cloneOperationResponse.opId)
                    .GetResult($"Error getting operation status");
            } while (opInfo.cloneOperationResponse.state.Equals("Queued") ||
                     opInfo.cloneOperationResponse.state.Equals("InProgress"));


            if (opInfo.cloneOperationResponse.state.Equals("Failed"))
            {
                throw new Exception($"Error cloning test plan {plan.Name}: {opInfo.cloneOperationResponse.message}");
            }

            yield return (passthru == "Original") ? plan : Data.GetItem<TestPlan>(new { Plan = opInfo.destinationTestPlan });
        }
    }

    [CmdletController(typeof(TestPlan))]
    partial class GetTestPlanController
    {
        protected override IEnumerable Run()
        {
            var testPlan = Parameters.Get<object>(nameof(GetTestPlan.TestPlan));
            var owner = Parameters.Get<string>(nameof(GetTestPlan.Owner));
            var planDetails = !Parameters.Get<bool>(nameof(GetTestPlan.NoPlanDetails));
            var active = Parameters.Get<bool>(nameof(GetTestPlan.Active));

            while (true) switch (testPlan)
                {
                    case TestPlan plan:
                        {
                            yield return plan;
                            yield break;
                        }
                    case int i:
                        {
                            var tp = Data.GetProject();
                            var client = Data.GetClient<TestPlanHttpClient>();
                            yield return client.GetTestPlanByIdAsync(tp.Id, i)
                                .GetResult($"Error getting test plan '{i}'");
                            yield break;
                        }
                    case string s:
                        {
                            var tp = Data.GetProject();
                            var client = Data.GetClient<TestPlanHttpClient>();
                            foreach (var plan in client.GetTestPlansAsync(tp.Id, owner, null, planDetails, active)
                                .GetResult($"Error getting test plans '{testPlan}'")
                                .Where(plan => plan.Name.IsLike(s)))
                            {
                                yield return plan;
                            }
                            yield break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent test plan '{testPlan}'");
                        }
                }
        }
    }
}