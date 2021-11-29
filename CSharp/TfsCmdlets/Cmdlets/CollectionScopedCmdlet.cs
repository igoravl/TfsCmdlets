using System.Management.Automation;

namespace TfsCmdlets.Cmdlets
{
    public abstract class CollectionScopedCmdlet: ServerScopedCmdlet
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }
    }
}
