using System.Management.Automation;

namespace TfsCmdlets.Cmdlets
{
    public class CollectionLevelCmdlet: CmdletBase
    {

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public virtual object Collection { get; set; }
    }
}
