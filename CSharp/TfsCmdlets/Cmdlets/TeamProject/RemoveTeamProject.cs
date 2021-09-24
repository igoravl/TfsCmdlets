using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Deletes one or more team projects. 
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamProject", SupportsShouldProcess = true)]
    public class RemoveTeamProject : CollectionLevelCmdlet
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
        [Parameter()]
        public SwitchParameter Hard { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }
}