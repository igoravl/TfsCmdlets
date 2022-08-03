using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Changes the details of a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WebApiTeamProject))]
    partial class SetTeamProject
    {
        /// <summary>
        /// Specifies the name of the Team Project. 
        /// </summary>
        [Parameter(Position = 0)]
        public object Project { get; set; }

        /// <summary>
        /// Specifies the description of the team project.
        /// To remove a previously set description, pass a blank string ('') to this argument.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the name of a local image file to be uploaded and used as the team project icon ("avatar"). 
        /// To remove a previously set image, pass either $null or a blank string ('') to this argument.
        /// </summary>
        [Parameter]
        public string AvatarImage { get; set; }
    }
}