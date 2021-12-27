using TfsCmdlets.Cmdlets.WorkItem.WorkItemType;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Controllers.WorkItem.WorkItemType
{
    [CmdletController]
    partial class ExportWorkItemTypeController
    {

        [Import]
        private IWorkItemStore Store { get; set; }

        public override object InvokeCommand()
        {
            var types = Data.GetItems<WebApiWorkItemType>();
            var encoding = Parameters.Get<string>(nameof(ExportWorkItemType.Encoding));
            var asXml = Parameters.Get<bool>(nameof(ExportWorkItemType.AsXml));
            var force = Parameters.Get<bool>(nameof(ExportWorkItemType.Force));
            var includeGlobalLists = Parameters.Get<bool>(nameof(ExportWorkItemType.IncludeGlobalLists));
            var destination = Parameters.Get<string>(nameof(ExportWorkItemType.Destination));

            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            var result = new List<string>();

            foreach (var type in types)
            {
                if (!PowerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Export work item type '{type.Name}'")) continue;

                var xml = Store.ExportWorkItemType(tp.Name, type.Name, includeGlobalLists);

                if (xml == null) continue;

                if (asXml)
                {
                    result.Add(xml);
                    continue;
                }

                var relativePath = $"{type.Name}.xml";
                var outputPath = PowerShell.ResolvePath(destination, relativePath);
                var destDir = Path.GetDirectoryName(outputPath);

                if (!Directory.Exists(destDir))
                {
                    Logger.Log($"Destination path '{destination}' not found.");

                    if (!PowerShell.ShouldProcess(destination, "Create output directory")) continue;

                    Directory.CreateDirectory(destDir);
                }

                if (File.Exists(outputPath) && !(force || PowerShell.ShouldContinue($"Are you sure you want to overwrite existing file '{outputPath}'", "Confirm")))
                {
                    Logger.LogWarn($"Cannot overwrite existing file '{outputPath}'");
                    continue;
                }

                try
                {
                    File.WriteAllText(outputPath, xml, Encoding.GetEncoding(encoding));
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return result;
        }
    }
}