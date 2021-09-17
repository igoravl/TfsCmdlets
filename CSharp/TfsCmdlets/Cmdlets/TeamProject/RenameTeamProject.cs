using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Renames a team project. 
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsTeamProject", SupportsShouldProcess = true)]
    public class RenameTeamProject : CollectionLevelCmdlet
    {
        /// <summary>
        /// Specifies the name of a Team Project to rename.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Project { get; set; }

        /// <summary>
        /// Forces the renaming of the team project. When omitted, the command prompts for 
        /// confirmation prior to renaming the team project.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        // TODO

    }
}