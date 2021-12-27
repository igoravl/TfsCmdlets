using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Controllers.WorkItem.AreasIterations
{
    [CmdletController]
    partial class TestClassificationNodeController
    {
        public override object InvokeCommand()
        {
            return Data.TestItem<Models.ClassificationNode>();
        }
    }
}