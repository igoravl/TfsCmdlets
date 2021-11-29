using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Deletes one or more test plans.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTestPlan", SupportsShouldProcess = true)]
    public class RemoveTestPlan : CmdletBase
    {
        /// <summary>
        /// Specifies one or more test plans to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards]
        [Alias("Id", "Name")]
        [ValidateNotNull()]
        public object TestPlan { get; set; }

        /// <summary>
        /// Forces the deletion of test plans with test suites and/or test cases. 
        /// When omitted, only empty test plans can be deleted.
        /// </summary>
        public SwitchParameter Force { get; set; }
    }

    // TODO

    //partial class TestPlanDataService
    //{
    //    protected override void DoRemoveItem(ParameterDictionary parameters)
    //    {
    //        var plans = GetItems<TestPlan>();
    //        var force = parameters.Get<bool>(nameof(RemoveTestPlan.Force));
    //        var tp = Data.GetProject(parameters);
    //        var client = Data.GetClient<TestPlanHttpClient>(parameters);

    //        foreach (var plan in plans)
    //        {
    //            if (!PowerShell.ShouldProcess(tp, $"Delete test plan '{plan.Name}'")) continue;

    //            var hasChildren = true; // TODO: Get children

    //            if (hasChildren && !force & !PowerShell.ShouldContinue($"Are you sure you want to delete test plan '{plan.Name}' and all of its contents?")) continue;

    //            client.DeleteTestPlanAsync(tp.Name, plan.Id)
    //                .Wait($"Error deleting test plan '{plan.Name}'");
    //        }
    //    }
    //}
}