using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Gets one or more Team Project Collection addresses registered in the current computer.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsRegisteredTeamProjectCollection")]
    //[OutputType(typeof(Microsoft.TeamFoundation.Client.RegisteredProjectCollection[]))]
    partial class GetRegisteredTeamProjectCollection
    {
        // TODO

        //         [Parameter(Position=0, ValueFromPipeline=true)]
        //         [Alias("Name")]
        //         [SupportsWildcards()]
        //         public string Collection { get; set; } = "*"

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         registeredCollections = Microsoft.TeamFoundation.Client.RegisteredTfsConnections.GetProjectCollections() 

        //         foreach(tpc in registeredCollections)
        //         {
        //             tpcName = ([Uri]tpc.Uri).Segments[-1]

        //             if(tpcName -like Collection)
        //             {
        //                 Write-Output tpc
        //             }
        //         }
        //     }
        // }
    }
}
