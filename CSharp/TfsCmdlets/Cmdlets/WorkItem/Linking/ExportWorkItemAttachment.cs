using System.Management.Automation;

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
}