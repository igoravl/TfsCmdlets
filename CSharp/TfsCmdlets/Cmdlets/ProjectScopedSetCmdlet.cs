using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace TfsCmdlets.Cmdlets
{
    public abstract class ProjectScopedSetCmdlet : ProjectScopedCmdlet
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
    }
}
