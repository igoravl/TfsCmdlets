using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Creates a new team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, RequiresVersion = 2015, DefaultParameterSetName = "Get by project", SupportsShouldProcess = true, 
     OutputType = typeof(WebApiTeamProject))]
    partial class NewTeamProject
    {
        /// <summary>
        ///  Specifies the name of the new team project.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Project { get; set; }

        /// <summary>
        /// Specifies a description for the new team project.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the source control type to be provisioned initially with the team project. 
        /// Supported types are "Git" and "Tfvc".
        /// </summary>
        [Parameter]
        [ValidateSet("Git", "Tfvc")]
        public string SourceControl { get; set; } = "Git";

        /// <summary>
        /// Specifies the process template on which the new team project is based. 
        /// Supported values are the process name or an instance of the
        /// Microsoft.TeamFoundation.Core.WebApi.Process class.
        /// </summary>
        [Parameter]
        public object ProcessTemplate { get; set; }
    }
}