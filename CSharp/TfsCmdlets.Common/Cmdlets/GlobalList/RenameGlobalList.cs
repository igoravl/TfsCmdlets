using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Changes either the name or the contents of a Global List.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsGlobalList", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [DesktopOnly]
    public partial class RenameGlobalList : BaseGlobalListCmdlet
    {
        /// <summary>
        /// Specifies the name of the global lsit to be renamed.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }

        /// <summary>
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string NewName { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
    }
}