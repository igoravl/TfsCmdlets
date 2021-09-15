using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using WebApiTeamProjectRef = Microsoft.TeamFoundation.Core.WebApi.TeamProjectReference;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Undeletes one or more team projects. 
    /// </summary>
    [Cmdlet(VerbsCommon.Undo, "TfsTeamProjectRemoval", SupportsShouldProcess = true)]
    public class UndoTeamProjectRemoval : CmdletBase
    {
        /// <summary>
        /// Specifies the name of the Team Project to undelete.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Project { get; set; }

        // TODO

    }
}