using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace TfsCmdlets.Cmdlets
{
    public abstract class ProjectScopedGetCmdlet : CollectionScopedCmdlet
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Project { get; set; }
    }
}
