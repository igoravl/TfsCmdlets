using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using TfsCmdlets.Extensions;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using TfsCmdlets.Util;
using System.IO;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    /// <summary>
    /// Downloads one or more attachments from work items
    /// </summary>
    [Cmdlet(VerbsData.Export, "TfsWorkItemAttachment", SupportsShouldProcess = true)]
    public class ExportWorkItemAttachment: CmdletBase
    {
        /// <summary>
        /// Specifies the attachment to download. Wildcards are supported. 
        /// When omitted, all attachments in the specified work item are downloaded.
        /// </summary>
        [Parameter(Position = 0)]
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
        [Parameter()]
        [ValidateNotNull()]
        public string Destination { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing file.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var wi = GetItem<WebApiWorkItem>(new
        //    {
        //        this.WorkItem,
        //        IncludeLinks = true
        //    });

        //    string attachment;

        //    switch (Attachment)
        //    {
        //        case WorkItemRelation r:
        //            {
        //                attachment = (string) r.Attributes["name"];
        //                break;
        //            }
        //        case string s when !string.IsNullOrEmpty(s):
        //            {
        //                attachment = s;
        //                break;
        //            }
        //        default:
        //            {
        //                throw new ArgumentException($"Invalid attachment '{Attachment}'");
        //            }
        //    }

        //    var tp = Data.GetProject(parameters);
        //    var client = Data.GetClient<WorkItemTrackingHttpClient>(parameters);
        //    var outputDir = ResolvePath(Destination);

        //    Log($"Downloading attachments to output directory '{outputDir}'");

        //    if(!Directory.Exists(outputDir))
        //    {
        //        if(!PowerShell.ShouldProcess(outputDir, "Create destination directory")) return;

        //        if(!Force && !PowerShell.ShouldContinue($"Do you want to create destination directory '{outputDir}'?"))
        //        {
        //            throw new ArgumentException("Destination path invalid or non-existent");
        //        }

        //        Directory.CreateDirectory(outputDir);
        //    }

        //    foreach (var att in wi.Relations.Where(l => l.Rel == "AttachedFile" && ((string)l.Attributes["name"]).IsLike(attachment)))
        //    {
        //        var name = ((string)att.Attributes["name"]);
        //        var outputPath = Path.Combine(outputDir, name);
        //        var exists = File.Exists(outputPath);

        //        if (!PowerShell.ShouldProcess(name, $"Download attachment{(exists? " and overwrite existing file": "")}")) continue;

        //        var uri = new Uri(att.Url);
        //        var id = Guid.Parse(uri.Segments[uri.Segments.Length-1]);

        //        var stream = client.GetAttachmentContentAsync(id, name).SyncResult<Stream>();

        //        if (exists && !Force && !PowerShell.ShouldContinue($"Are you sure you want to overwrite existing file '{outputPath}'?")) continue;

        //        using var writer = new FileStream(outputPath, FileMode.Create);

        //        stream.CopyTo(writer);
        //    }
        //}
    }
}
