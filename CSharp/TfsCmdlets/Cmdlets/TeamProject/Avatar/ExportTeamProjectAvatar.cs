using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Exports the current avatar (image) of the specified team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, HostedOnly = true)]
    partial class ExportTeamProjectAvatar
    {
        /// <summary>
        /// Specifies the path of the file where the avatar image will be saved. 
        /// When omitted, the image will be saved to the current directory as &lt;team-project-name&gt;.png.
        /// </summary>
        [Parameter(Position = 0)]
        public string Path { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_OVERWRITE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }

    }
}