using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Imports and sets a new team project avatar (image).
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, HostedOnly = true)]
    partial class ImportTeamProjectAvatar
    {
        /// <summary>
        /// Specifies the path of the image file to import. 
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Path { get; set; }
    }
}