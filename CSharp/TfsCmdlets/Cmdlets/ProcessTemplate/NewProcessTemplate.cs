using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using TfsCmdlets.Extensions;
using WebApiProcess = Microsoft.TeamFoundation.Core.WebApi.Process;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Creates a new inherited process.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsProcessTemplate", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiProcess))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class NewProcessTemplate 
    {
        /// <summary>
        /// Specifies the name of the process to create.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("Name")]
        public string ProcessTemplate { get; set; }

        /// <summary>
        /// Specifies the description of the new process.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the reference name of the new process. When omitted, a random name 
        /// will be automatically generated and assigned by the server.
        /// </summary>
        [Parameter]
        public string ReferenceName { get; set; }

        /// <summary>
        /// Specifies the name of the parent process from which the new process will inherit.
        /// </summary>
        [Parameter]
        public object Parent { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing process.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }
}
