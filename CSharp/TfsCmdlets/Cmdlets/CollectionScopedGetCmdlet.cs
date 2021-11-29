using System.Management.Automation;

namespace TfsCmdlets.Cmdlets
{
    public abstract class CollectionScopedGetCmdlet: ServerScopedCmdlet
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }
    }
}
