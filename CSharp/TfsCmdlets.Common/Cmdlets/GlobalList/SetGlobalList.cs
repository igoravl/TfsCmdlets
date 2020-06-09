using System.Collections.Generic;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Changes the contents of a Global List.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsGlobalList", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [DesktopOnly]
    public partial class SetGlobalList : BaseCmdlet
    {
        /// <summary>
        /// Specifies the name of the global list to be changed.
        /// </summary>
        /// <value></value>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }

        /// <summary>
        /// Specifies a list of items to be added to the global list.
        /// </summary>
        [Parameter(ParameterSetName = "Edit list items")]
        public IEnumerable<string> Add { get; set; }

        /// <summary>
        /// Specifies a list of items to be removed from the global list.
        /// </summary>
        [Parameter(ParameterSetName = "Edit list items")]
        public IEnumerable<string> Remove { get; set; }

        /// <summary>
        /// Creates a new list if the specified one does not exist.
        /// </summary>
        [Parameter(ParameterSetName = "Edit list items")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        public object Collection { get; set; }
    }
}