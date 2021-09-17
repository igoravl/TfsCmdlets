using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace TfsCmdlets.Cmdlets
{
    public class SetCmdlet: ProjectLevelCmdlet
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
    }
}
