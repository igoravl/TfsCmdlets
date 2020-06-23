using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Creates a new team project collection.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsTeamProjectCollection", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(Models.Connection))]
    public class NewTeamProjectCollection : CmdletBase
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
        //         [Alias("Name")]
        //         public string Collection { get; set; }

        //         [Parameter()]
        //         public string Description { get; set; }

        //         [Parameter(ParameterSetName="Use database server", Mandatory=true)]
        //         public string DatabaseServer { get; set; }

        //         [Parameter(ParameterSetName="Use database server")]
        //         public string DatabaseName { get; set; }

        //         [Parameter(ParameterSetName="Use connection string", Mandatory=true)]
        //         public string ConnectionString { get; set; }

        //         [Parameter()]
        //         public SwitchParameter Default { get; set; }

        //         [Parameter()]
        //         public SwitchParameter UseExistingDatabase { get; set; }

        //         [Parameter()]
        //         [ValidateSet("Started", "Stopped")]
        //         public string InitialState { get; set; } = "Started",

        //         [Parameter()]
        //         public int PollingInterval { get; set; } = 5,

        //         [Parameter()]
        //         public timespan Timeout { get; set; } = timespan.MaxValue,

        //         [Parameter()]
        // public object Server,

        //         [Parameter()]
        //         [System.Management.Automation.Credential()]
        //         [System.Management.Automation.PSCredential]
        //         Credential = System.Management.Automation.PSCredential.Empty,

        //         [Parameter()]
        //         public SwitchParameter Passthru { get; set; }

        // /// <summary>
        // /// Performs execution of the command
        // /// </summary>
        // protected override void ProcessRecord()
        //     {
        //         if(ShouldProcess(Collection, "Create team project collection"))
        //         {
        //             configServer = Get-TfsConfigurationServer Server -Credential Credential
        //             tpcService = configServer.GetService([type] "Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService")
        //             servicingTokens = new System.Collections.Generic.Dictionary[string,string]()
        //             servicingTokens["SharePointAction"] = "None"
        //             servicingTokens["ReportingAction"] = "None"

        //             if (DatabaseName)
        //             {
        //                 servicingTokens["CollectionDatabaseName"] = DatabaseName
        //             }

        //             if (UseExistingDatabase)
        //             {
        //                 servicingTokens["UseExistingDatabase"] = UseExistingDatabase.ToBool()
        //             }

        //             if (ParameterSetName == "Use database server")
        //             {
        //                 ConnectionString = $"Data source={DatabaseServer}; Integrated Security=true"
        //             }

        //             try
        //             {
        //                 Write-Progress -Id 1 -Activity $"Create team project collection" -Status "Creating team project collection {Collection}" -PercentComplete 0

        //                 start = Get-Date

        //                 tpcJob = tpcService.QueueCreateCollection(
        //                     Collection,
        //                     Description, 
        //                     Default.ToBool(),
        //                     $"~/{Collection}/",
        //                     [Microsoft.TeamFoundation.Framework.Common.TeamFoundationServiceHostStatus] InitialState,
        //                     servicingTokens,
        //                     ConnectionString,
        //                     null,  # Default connection string
        //                     null)  # Default category connection strings

        //                 while((Get-Date).Subtract(start) -le Timeout)
        //                 {
        //                     Start-Sleep -Seconds PollingInterval

        //                     collectionInfo = tpcService.GetCollection(tpcJob.HostId, Microsoft.TeamFoundation.Framework.Client.ServiceHostFilterFlags.IncludeAllServicingDetails)
        //                     jobDetail = collectionInfo.ServicingDetails | Where-Object JobId == tpcJob.JobId

        //                     if ((null = = jobDetail) || 
        //                         ((jobDetail.JobStatus != Microsoft.TeamFoundation.Framework.Client.ServicingJobStatus.Queued) && 
        //                         (jobDetail.JobStatus != Microsoft.TeamFoundation.Framework.Client.ServicingJobStatus.Running)))
        //                     {
        //                         if (jobDetail.Result == Microsoft.TeamFoundation.Framework.Client.ServicingJobResult.Failed || 
        //                             jobDetail.JobStatus == Microsoft.TeamFoundation.Framework.Client.ServicingJobStatus.Failed)
        //                         {
        //                             throw new Exception($"Error creating team project collection {Collection} : ")
        //                         }

        //                         tpc = Get-TfsTeamProjectCollection -Server Server -Credential Credential -Collection Collection

        //                         if (Passthru)
        //                         {
        //                             WriteObject(tpc); return;
        //                         }
        //                     }
        //                 }
        //             }
        //             finally
        //             {
        //                     Write-Progress -Id 1 -Activity "Create team project collection" -Completed
        //             }

        //             throw new Exception((new System.TimeoutException($"Operation timed out during creation of team project collection {Collection}")))
        //         }
        //     }
        // }
    }
}
