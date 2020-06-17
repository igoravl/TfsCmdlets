using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Starts an offline team project collection and make it online.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TfsTeamProjectCollection", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    public class StartTeamProjectCollection : BaseCmdlet
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
        // public object Collection,

        //         [Parameter()]
        // public object Server,

        //         [Parameter()]
        //         [System.Management.Automation.Credential()]
        //         [System.Management.Automation.PSCredential]
        //         Credential = System.Management.Automation.PSCredential.Empty

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         if(ShouldProcess(Collection, "Start team project collection"))
        //         {
        //             throw new Exception("Not implemented")
        //         }
        //     }
        // }
    }
}
