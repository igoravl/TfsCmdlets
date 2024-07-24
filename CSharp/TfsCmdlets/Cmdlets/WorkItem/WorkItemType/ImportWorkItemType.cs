using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Imports a work item type definition into a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DesktopOnly = true)]
    partial class ImportWorkItemType
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, ParameterSetName = "Import from XML")]
        [ValidateNotNull]
        public string Xml { get; set; }

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Import from file")]
        [ValidateNotNull]
        public string Path { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItemType))]
    partial class ImportWorkItemTypeController
    {
        [Import]
        private IWorkItemStore Store { get; set; }

        protected override IEnumerable Run()
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