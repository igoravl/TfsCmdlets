using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
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