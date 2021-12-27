using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Exports the team project avatar (image).
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true)]
    partial class ImportTeamProjectAvatar
    {
        /// <summary>
        /// Specifies the path of the file image to import. 
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Path { get; set; }
    }
}