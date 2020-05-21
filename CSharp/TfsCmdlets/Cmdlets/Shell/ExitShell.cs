using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Shell
{
    [Cmdlet(VerbsCommon.Exit, "Shell")]
    public class ExitShell: PSCmdlet
    {
/*

    if(script:PrevShellTitle)
    {
        Host.UI.RawUI.WindowTitle = script:PrevShellTitle
    }

    if(script:PrevPrompt)
    {
        Set-Content function:prompt script:PrevPrompt
    }

    Clear-Host
}
*/
}
}
