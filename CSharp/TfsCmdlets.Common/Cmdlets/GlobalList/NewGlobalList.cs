using System.Collections.Generic;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Creates a new Global List.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsGlobalList", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(PSCustomObject))]
    [DesktopOnly]
    public partial class NewGlobalList : BaseGlobalListCmdlet
    {
        /// <summary>
        /// Specifies the name of the new global list.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }

        /// <summary>
        /// Specifies the contents (items) of the new global list.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string[] Items { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing global list.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
    }
}