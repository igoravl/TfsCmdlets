using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.WorkItemType;

namespace TfsCmdlets.Controllers.WorkItem.WorkItemType
{
    [CmdletController(typeof(WebApiWorkItemType))]
    partial class ImportWorkItemTypeController
    {
        [Import]
        private IWorkItemStore Store { get; set; }

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

            Store.ImportWorkItemType(tp.Name, xml);

            return null;
        }
    }
}
