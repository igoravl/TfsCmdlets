using System.Management.Automation;
using System;
using System.Threading;
using Microsoft.TeamFoundation.Framework.Common;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{

#if NET461_OR_GREATER
    using Microsoft.TeamFoundation.Framework.Client;
#endif

    /// <summary>
    /// Creates a new team project collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Server, DesktopOnly = true, SupportsShouldProcess = true, OutputType = typeof(Models.Connection))]
    partial class NewTeamProjectCollection
    {
        /// <summary>
        /// Specifies the name of the team project collection to create.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Name")]
        public object Collection { get; set; }

        /// <summary>
        /// Specifies the description of the team project collection.
        /// </summary>
        [Parameter]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the name of the SQL Server for hosting the team project collection database.
        /// </summary>
        [Parameter(ParameterSetName = "Use database server", Mandatory = true)]
        public string DatabaseServer { get; set; }

        /// <summary>
        /// Specifies the name of the team project collection database.
        /// </summary>
        [Parameter(ParameterSetName = "Use database server")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Specifies the connection string for the team project collection.
        /// </summary>
        [Parameter(ParameterSetName = "Use connection string", Mandatory = true)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Specifies whether new the team project collection should become the default collection.
        /// </summary>
        [Parameter]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// Specifies whether to use an existing database for the team project collection.
        /// </summary>
        [Parameter]
        public SwitchParameter UseExistingDatabase { get; set; }

        /// <summary>
        /// Specifies the initial state of the team project collection. This allows you to create a collection in a stopped state when needed.
        /// </summary>
        [Parameter]
        [ValidateSet("Started", "Stopped")]
        public string InitialState { get; set; } = "Started";

        /// <summary>
        /// Specifies the frequency (in seconds) at which the command will check whether the creation is completed.
        /// </summary>
        [Parameter]
        [ValidateRange(5, 60)]
        public int PollingInterval { get; set; } = 5;

        /// <summary>
        /// Specifies the timeout to wait for the operation to complete. When omitted, will wait indefinitely until the operation completes.
        /// </summary>
        [Parameter]
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;
    }

    [CmdletController(typeof(Connection))]
    partial class NewTeamProjectCollectionController
    {
        protected override IEnumerable Run()
        {
#if NET461_OR_GREATER
            var tpc = Data.GetCollection();

            if (!PowerShell.ShouldProcess(tpc, "Create team project collection")) yield break;

            var configServer = Data.GetServer();
            var collectionName = Parameters.Get<string>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.Collection));
            var databaseServer = Parameters.Get<string>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.DatabaseServer));
            var databaseName = Parameters.Get<string>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.DatabaseName));
            var description = Parameters.Get<string>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.Description));
            var useExistingDatabase = Parameters.Get<bool>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.UseExistingDatabase));
            var isDefault = Parameters.Get<bool>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.Default));
            var connectionString = Parameters.Get<string>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.ConnectionString));
            var pollingInterval = Parameters.Get<int>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.PollingInterval));
            var timeout = Parameters.Get<TimeSpan>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.Timeout));

            Enum.TryParse<TeamFoundationServiceHostStatus>(Parameters.Get<string>(nameof(Cmdlets.TeamProjectCollection.NewTeamProjectCollection.InitialState)),
                out var initialState);

            var tpcService = Data.GetService<ITeamProjectCollectionService>();

            var servicingTokens = new Dictionary<string, string>()
            {
                ["SharePointAction"] = "None",
                ["ReportingAction"] = "None"
            };

            if (!string.IsNullOrEmpty(databaseName)) servicingTokens["CollectionDatabaseName"] = databaseName;
            if (useExistingDatabase) servicingTokens["UseExistingDatabase"] = useExistingDatabase.ToString();
            if (!string.IsNullOrEmpty(connectionString)) servicingTokens["CollectionDatabaseName"] = databaseName;
            if (!string.IsNullOrEmpty(databaseServer)) connectionString = $"Data source={databaseServer}; Integrated Security=true";

            var tpcJob = tpcService.QueueCreateCollection(
                collectionName,
                description,
                isDefault,
                $"~/{collectionName}/",
                initialState,
                servicingTokens,
                connectionString,
                null,
                null);

            var start = DateTime.Now;

            while (DateTime.Now.Subtract(start) < timeout)
            {
                Thread.Sleep(pollingInterval);

                var collectionInfo = tpcService.GetCollection(tpcJob.HostId,
                    ServiceHostFilterFlags.IncludeAllServicingDetails);

                var jobDetail = collectionInfo.ServicingDetails.Cast<ServicingJobDetail>().FirstOrDefault(job => job.JobId == tpcJob.JobId);

                if (jobDetail == null) yield break;

                switch (jobDetail.JobStatus)
                {
                    case ServicingJobStatus.Unknown:
                    case ServicingJobStatus.Queued:
                    case ServicingJobStatus.Running:
                        continue;
                    case ServicingJobStatus.Failed:
                        throw new Exception($"Error creating team project collection {collectionName}");
                    case ServicingJobStatus.Complete:
                        yield return Data.GetItem<WebApiTeamProject>();
                        yield break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new TimeoutException($"Operation timed out during creation of team project collection {collectionName}");
#else
            yield break;
#endif
        }
    }
}
