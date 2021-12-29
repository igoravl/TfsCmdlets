using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Linking;

namespace TfsCmdlets.Controllers.WorkItem.Linking
{
    [CmdletController(typeof(WorkItemRelation))]
    partial class ExportWorkItemAttachmentController
    {
        public override IEnumerable<WorkItemRelation> Invoke()
        {
            var links = Data.GetItems<WorkItemRelation>(new { LinkType = WorkItemLinkType.AttachedFile, IncludeAttachments = true });
            var attachment = Parameters.Get<object>(nameof(ExportWorkItemAttachment.Attachment));
            var destination = Parameters.Get<string>(nameof(ExportWorkItemAttachment.Destination));
            var force = Parameters.Get<bool>(nameof(ExportWorkItemAttachment.Force));

            string attachmentName;

            switch (attachment)
            {
                case WorkItemRelation r:
                    {
                        attachmentName = (string)r.Attributes["name"];
                        break;
                    }
                case string s when !string.IsNullOrEmpty(s):
                    {
                        attachmentName = s;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"Invalid attachment '{attachment}'");
                    }
            }

            // var tp = Data.GetProject();
            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var outputDir = PowerShell.ResolvePath(destination);

            Logger.Log($"Downloading attachments to output directory '{outputDir}'");

            if (!Directory.Exists(outputDir))
            {
                if (!PowerShell.ShouldProcess(outputDir, "Create destination directory")) yield break;

                if (!force && !PowerShell.ShouldContinue($"Do you want to create destination directory '{outputDir}'?"))
                {
                    throw new ArgumentException("Destination path invalid or non-existent");
                }

                Directory.CreateDirectory(outputDir);
            }

            foreach (var link in links.Where(l => ((string)l.Attributes["name"]).IsLike(attachmentName)))
            {
                try
                {
                    var name = (string)link.Attributes["name"];
                    var outputPath = Path.Combine(outputDir, name);
                    var exists = File.Exists(outputPath);

                    if (!PowerShell.ShouldProcess(name, $"Download attachment{(exists ? " and overwrite existing file" : "")}")) continue;

                    var uri = new Uri(link.Url);
                    var id = Guid.Parse(uri.Segments[uri.Segments.Length - 1]);

                    var stream = client.GetAttachmentContentAsync(id, name).SyncResult<Stream>();

                    if (exists && !force && !PowerShell.ShouldContinue($"Are you sure you want to overwrite existing file '{outputPath}'?")) continue;

                    using var writer = new FileStream(outputPath, FileMode.Create);

                    stream.CopyTo(writer);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    continue;
                }

                yield return link;
            }
        }
    }
}
