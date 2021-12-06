using System.Collections.Generic;
using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;
using TfsCmdlets.Util;
using System;
using TfsCmdlets.Extensions;
using Microsoft.VisualStudio.Services.Operations;
using System.Threading;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Creates a new team project.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTeamProject", DefaultParameterSetName = "Get by project", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeamProject))]
    [TfsCmdlet(CmdletScope.Collection, RequiresVersion = 2015)]
    partial class NewTeamProject
    {
        /// <summary>
        ///  Specifies the name of the new team project.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public object Project { get; set; }

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