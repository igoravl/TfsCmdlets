using System.Collections.Generic;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Shell
{
    /// <summary>
    /// Deactivates the Azure DevOps Shell
    /// </summary>
    [Cmdlet(VerbsCommon.Exit, "TfsShell")]
    public class ExitShell : CmdletBase
    {
    }
}