using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Deletes one or more Global Lists.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGlobalList", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [DesktopOnly]
    public partial class RemoveGlobalList : BaseGlobalListCmdlet
    {
        /// <summary>
        /// Specifies the name of global list to be deleted. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string GlobalList { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }
    }
}