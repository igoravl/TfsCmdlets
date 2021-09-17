using System.Management.Automation;

namespace TfsCmdlets.Cmdlets
{
    public class ProjectLevelCmdlet: CollectionLevelCmdlet
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public virtual object Project { get; set; }
    }
}
