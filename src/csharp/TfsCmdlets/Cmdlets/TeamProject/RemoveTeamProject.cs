using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Deletes one or more team projects. 
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class RemoveTeamProject
    {
        /// <summary>
        /// Specifies the name of a Team Project to delete. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [SupportsWildcards()]
        public object Project { get; set; }

        /// <summary>
        /// Deletes the team project permanently. When omitted, the team project is moved to a 
        /// "recycle bin" and can be recovered either via UI or by using Undo-TfsTeamProjectRemoval.
        /// </summary>
        [Parameter]
        public SwitchParameter Hard { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }
}