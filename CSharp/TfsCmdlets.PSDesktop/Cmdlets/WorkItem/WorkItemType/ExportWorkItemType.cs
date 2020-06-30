using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;
using System.Management.Automation;
using System.Xml;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    partial class ExportWorkItemType
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var (tpc, tp) = GetCollectionAndProject();
            var types = GetItems<WebApiWorkItemType>();
            var store = tpc.GetService<WorkItemStore>();
            var project = store.Projects[tp.Name];

            foreach (var t in types)
            {
                var type = project.WorkItemTypes[t.Name];
                var xml = type.Export(IncludeGlobalLists).ToXDocument().ToString();

                if(AsXml)
                {
                    WriteObject(xml);
                    return;
                }
            }
        }
    }
}