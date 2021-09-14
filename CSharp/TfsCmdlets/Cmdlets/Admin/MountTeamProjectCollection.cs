using System;
using System.Management.Automation;

#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Client;
#endif

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Attaches a team project collection database to a Team Foundation Server installation.
    /// </summary>
    [Cmdlet(VerbsData.Mount, "TfsTeamProjectCollection", ConfirmImpact = ConfirmImpact.Medium)]
    public partial class MountTeamProjectCollection : BasicCmdlet
    {
        /// <summary>
        /// Specifies the name of the collection to attach. It can be different from the original 
        /// name - in that case, it is attached under a new name.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Name")]
        public string Collection { get; set; }

        /// <summary>
        /// Specifies a new description for the collection. When omitted, it retains the original description.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the name of the SQL Server instance where the database is stored.
        /// </summary>
        [Parameter(ParameterSetName = "Use database server", Mandatory = true)]
        public string DatabaseServer { get; set; }

        /// <summary>
        /// Specifies the name of the collection database.
        /// </summary>
        /// <value></value>
        [Parameter(ParameterSetName = "Use database server", Mandatory = true)]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Specifies the connection string of the collection database.
        /// </summary>
        [Parameter(ParameterSetName = "Use connection string", Mandatory = true)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Specifies whether the collection will be started ou stopped after being attached. 
        /// When omitted, the collection is automatically started and goes online after being attached.
        /// </summary>
        /// <value></value>
        [Parameter()]
        [ValidateSet("Started", "Stopped")]
        public string InitialState { get; set; } = "Started";

        /// <summary>
        /// Changes the internal collection IDs upon attaching to that a "clone" of the original 
        /// collection can be attached to the same server.
        /// </summary>
        [Parameter()]
        public SwitchParameter Clone { get; set; }

        /// <summary>
        /// Specifies the polling interval (in seconds) to get an updated status from the server. 
        /// When omitted, defaults to 5 seconds.
        /// </summary>
        /// <value></value>
        [Parameter()]
        public int PollingInterval { get; set; } = 5;

        /// <summary>
        /// Specifies the maximum period of time this cmdlet should wait for the attach procedure 
        /// to complete. By default, it waits indefinitely until the collection servicing completes.
        /// </summary>
        [Parameter()]
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Server;

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
// #if NET471_OR_GREATER
//             var configServer = (TfsConfigurationServer) GetServer().InnerConnection;
//             var tpcService = configServer.GetService<Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService>();
//             var servicingTokens = new System.Collections.Generic.Dictionary<string,string>();

//             if (ParameterSetName == "Use database server")
//             {
//                 servicingTokens["CollectionDatabaseName"] = DatabaseName;
//                 ConnectionString = $"Data source={DatabaseServer}; Integrated Security=true; Initial Catalog={DatabaseName}";
//             }

//             try
//             {
//                 //Write-Progress -Id 1 -Activity $"Attach team project collection" -Status "Attaching team project collection {Collection}" -PercentComplete 0

//                 var tpcJob = tpcService.QueueAttachCollection(ConnectionString, servicingTokens, Clone.ToBool(), Collection, Description,
//                     $"~/{Collection}/");

//                 var collection = tpcService.WaitForCollectionServicingToComplete(tpcJob, Timeout);

//                 WriteObject(GetItem<Models.Connection>(new {Collection = this.Collection}));
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception($"Error mounting collection '{Collection}': {ex}", ex);
//             }
//             finally
//             {
//                 // Write-Progress -Id 1 -Activity "Attach team project collection" -Completed
//             }
// #endif
        }
    }
}