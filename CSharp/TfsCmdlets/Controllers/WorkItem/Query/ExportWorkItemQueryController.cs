using System.Xml.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.Cmdlets.WorkItem.Query;

namespace TfsCmdlets.Controllers.WorkItem.Query
{
    [CmdletController()]
    partial class ExportWorkItemQueryController
    {
        protected override IEnumerable Run()
        {
            var queries = Data.GetItems<QueryHierarchyItem>(new { ItemType = "Query" });
            var encoding = Parameters.Get<string>(nameof(ExportWorkItemQuery.Encoding));
            var asXml = Parameters.Get<bool>(nameof(ExportWorkItemQuery.AsXml));
            var flattenFolders = Parameters.Get<bool>(nameof(ExportWorkItemQuery.FlattenFolders));
            var force = Parameters.Get<bool>(nameof(ExportWorkItemQuery.Force));
            var destination = Parameters.Get<string>(nameof(ExportWorkItemQuery.Destination));

            var tpc = Data.GetCollection();
            var tp = Data.GetProject();

            var result = new List<string>();

            foreach (var query in queries)
            {
                if (!PowerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Export work item query '{query.Path}'")) continue;

                var doc = XDocument.Parse($@"<?xml version=""1.0"" encoding=""{encoding}""?>
<WorkItemQuery Version=""1"">
    <!-- Original Query Path: [{query.Path}] -->
    <TeamFoundationServer>{tpc.Uri}</TeamFoundationServer>
    <TeamProject>{tp.Name}</TeamProject>
    <Wiql><![CDATA[{query.Wiql}]]></Wiql>
</WorkItemQuery>

");

                if (asXml)
                {
                    result.Add(doc.ToString());
                    continue;
                }

                var relativePath = $"{(flattenFolders ? query.Path.Replace('/', '_') : query.Path)}.wiq";
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
                    doc.Save(outputPath);
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