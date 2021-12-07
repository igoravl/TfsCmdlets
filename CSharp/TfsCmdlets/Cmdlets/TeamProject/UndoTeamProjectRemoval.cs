using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiTeamProjectRef = Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Undeletes one or more team projects. 
    /// </summary>
    [Cmdlet(VerbsCommon.Undo, "TfsTeamProjectRemoval", SupportsShouldProcess = true)]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class UndoTeamProjectRemoval
    {
        /// <summary>
        /// Specifies the name of the Team Project to undelete.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Project { get; set; }
    }
}