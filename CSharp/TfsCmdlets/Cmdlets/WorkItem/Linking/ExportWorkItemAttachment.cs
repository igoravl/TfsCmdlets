using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Downloads one or more attachments from work items
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class ExportWorkItemAttachment : CmdletBase
    {
        /// <summary>
        /// Specifies the attachment to download. Wildcards are supported. 
        /// When omitted, all attachments in the specified work item are downloaded.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        [ValidateNotNull()]
        public object Attachment { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies the directory to save the attachment to. When omitted, defaults to the current directory.
        /// </summary>
        [Parameter]
        [ValidateNotNull()]
        public string Destination { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing file.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WorkItemRelation), Client = typeof(IWorkItemTrackingHttpClient))]
    partial class ExportWorkItemAttachmentController
    {
        protected override IEnumerable Run()
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

                    var stream = Client.GetAttachmentContentAsync(id, name).SyncResult<Stream>();

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