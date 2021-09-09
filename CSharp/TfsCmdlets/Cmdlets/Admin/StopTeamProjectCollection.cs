using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Stops a team project collection and make it offline.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    public class StopTeamProjectCollection : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
        // public object Collection,

        //         [Parameter()]
        //         public string Reason { get; set; }

        //         [Parameter()]
        // public object Server,

        //         [Parameter()]
        //         [System.Management.Automation.Credential()]
        //         [System.Management.Automation.PSCredential]
        //         Credential = System.Management.Automation.PSCredential.Empty

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         if(ShouldProcess(Collection, "Stop team project collection"))
        //         {
        //             throw new Exception("Not implemented")
        //         }
        //     }
        // }
    }
}
