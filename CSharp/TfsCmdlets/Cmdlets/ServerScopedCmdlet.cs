using System.Management.Automation;

namespace TfsCmdlets.Cmdlets
{
    public abstract class ServerScopedCmdlet : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}
