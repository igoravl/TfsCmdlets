using System.Management.Automation;
using System.Xml.Linq;
using TfsCmdlets.Cmdlets.WorkItem.WorkItemType;
using WebApiWorkItemType = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType;

namespace TfsCmdlets.Controllers.WorkItem.WorkItemType
{
    [CmdletController]
    partial class ExportWorkItemTypeController
    {
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

                var xml = GetXml(tpc, tp.Name, type, includeGlobalLists);

                if(xml == null) continue;

                var doc = xml.ToString();

                if (asXml)
                {
                    result.Add(doc.ToString());
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
                    File.WriteAllText(outputPath, doc, Encoding.GetEncoding(encoding));
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return result;
        }

        private object GetXml(Models.Connection tpc, string tpName, WebApiWorkItemType type, bool includeGlobalLists)
        {
#if NET471_OR_GREATER
            var store = tpc.InnerConnection.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
            var tp = store.Projects[tpName];
            var xmlText = tp.WorkItemTypes[type.Name].Export(includeGlobalLists).OuterXml;
            var doc = XDocument.Parse(xmlText);

            return doc.ToString();
#else
            return null;
#endif
        }
    }
}