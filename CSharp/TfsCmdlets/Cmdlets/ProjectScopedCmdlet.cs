using System.Management.Automation;

namespace TfsCmdlets.Cmdlets
{
    public abstract class ProjectScopedCmdlet : CollectionScopedCmdlet
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
    }
}
