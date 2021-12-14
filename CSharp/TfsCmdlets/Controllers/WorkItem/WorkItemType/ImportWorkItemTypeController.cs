using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.WorkItemType;
using TfsCmdlets.Util;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Controllers.WorkItem.WorkItemType
{
    [CmdletController(typeof(WebApiWorkItemType))]
    partial class ImportWorkItemTypeController
    {
        public override IEnumerable<WebApiWorkItemType> Invoke()
        {
            var xml = Parameters.Get<string>(nameof(ImportWorkItemType.Xml));
            var path = Parameters.Get<string>(nameof(ImportWorkItemType.Path));

            if (!string.IsNullOrEmpty(path))
            {
                path = PowerShell.ResolvePath(path);
                xml = System.IO.File.ReadAllText(path);
            }

            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            var process = Data.GetItem<WebApiProcess>(new { ProcessTemplate = tp.ProcessTemplate() });

            if (process.Type == ProcessType.System ||
                process.Type == ProcessType.Inherited)
            {
                throw new InvalidOperationException($"Work item types can only be imported into team projects based on XML Process Templates. Team Project '{tp.Name}' uses the {process.Type} process template '{process.Name}'.");
            }

            DoImport(tpc, tp.Name, xml);

            return null;
        }

        private void DoImport(Models.Connection tpc, string tpName, string xml)
        {
#if NET471_OR_GREATER
            var store = tpc.InnerConnection.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
            var tp = store.Projects[tpName];
            tp.WorkItemTypes.Import(xml);
#endif
        }
    }
}