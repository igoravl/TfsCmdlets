//using System.Management.Automation;
//using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
//using TfsCmdlets.Cmdlets;

//namespace TfsCmdlets.Controllers.TestManagement
//{
//    /// <summary>
//    /// Renames a test plans.
//    /// </summary>
//    [Cmdlet(VerbsCommon.Rename, "TfsTestPlan", SupportsShouldProcess=true, ConfirmImpact=ConfirmImpact.Medium)]
//    [OutputType(typeof(TestPlan))]
//    public class RenameTestPlan : CmdletBase
//    {
//        /// <summary>
//        /// Specifies the test plan name.
//        /// </summary>
//        [Parameter(Position = 0, ValueFromPipeline=true)]
//        [Alias("Id", "Name")]
//        public object TestPlan { get; set; }
//    }

//    // TODO

//    //partial class TestPlanDataService
//    //{
//    //    protected override TestPlan DoRenameItem()
//    //    {
//    //        var plan = GetItem<TestPlan>();
//    //        var newName = parameters.Get<string>("NewName");

//    //        var(_,tp)=GetCollectionAndProject();

//    //        if(!PowerShell.ShouldProcess(tp, $"Rename test plan '{plan.Name}' to '{newName}'")) return null;

//    //        var client = Data.GetClient<TestPlanHttpClient>(parameters);

//    //        return client.UpdateTestPlanAsync(new TestPlanUpdateParams() {
//    //            Name = newName
//    //        }, tp.Name, plan.Id).GetResult($"Error renaming test plan '{plan.Name}'");
//    //    }
//    //}
//}