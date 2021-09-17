using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace TfsCmdlets.Cmdlets
{
    public class GetCmdlet: ProjectLevelCmdlet
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public override object Project { get; set; }
    }
}
