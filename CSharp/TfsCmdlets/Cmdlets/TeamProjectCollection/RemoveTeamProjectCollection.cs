using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Deletes a team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamProjectCollection", SupportsShouldProcess = true)]
    public class RemoveTeamProjectCollection : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
        // public object Collection,

        //         [Parameter()]
        // public object Server,

        //         [Parameter()]
        //         public timespan Timeout { get; set; } = timespan.MaxValue,

        //         [Parameter()]
        //         [System.Management.Automation.Credential()]
        //         [System.Management.Automation.PSCredential]
        //         Credential = System.Management.Automation.PSCredential.Empty

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void DoProcessRecord()
        //     {
        //         tpc = Get-TfsTeamProjectCollection -Collection Collection -Server Server -Credential Credential

        //         if (ShouldProcess(tpc.Name, "Delete Team Project Collection"))
        //         {
        //             Write-Progress -Id 1 -Activity $"Delete team project collection" -Status "Deleting {{tpc}.Name}" -PercentComplete 0

        //             try
        //             {
        //                 configServer = tpc.ConfigurationServer
        //                 tpcService = configServer.GetService([type] "Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService")
        //                 collectionInfo = tpcService.GetCollection(tpc.InstanceId)

        //                 collectionInfo.Delete()
        //             }
        //             finally
        //             {
        //                 Write-Progress -Id 1 -Activity "Delete team project collection" -Completed
        //             }
        //         }
        //     }
        // }
    }
}
