using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.TestManagement
{
    /// <summary>
    /// Renames a test plans.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsTestPlan", SupportsShouldProcess=true, ConfirmImpact=ConfirmImpact.Medium)]
    [OutputType(typeof(TestPlan))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class RenameTestPlan
    {
        /// <summary>
        /// Specifies the test plan name.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline=true)]
        [Alias("Id", "Name")]
        public object TestPlan { get; set; }
    }
}