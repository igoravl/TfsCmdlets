// ReSharper disable RedundantUsingDirective
using System;
using System.Linq;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Framework.Common;
using TfsCmdlets.Models;

#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Framework.Client;
#endif

namespace TfsCmdlets.Controllers.TeamProjectCollection
{
    [CmdletController(typeof(Connection))]
    partial class NewTeamProjectCollectionController
    {
        public override IEnumerable<Connection> Invoke()
        {
#if NET471_OR_GREATER
            var tpc = Data.GetCollection();

            if (!PowerShell.ShouldProcess(tpc, "Create team project collection")) return null;

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

                if (jobDetail == null) return GetItems();

                switch (jobDetail.JobStatus)
                {
                    case ServicingJobStatus.Unknown:
                    case ServicingJobStatus.Queued:
                    case ServicingJobStatus.Running:
                        continue;
                    case ServicingJobStatus.Failed:
                        throw new Exception($"Error creating team project collection {collectionName}");
                    case ServicingJobStatus.Complete:
                        GetItem();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new TimeoutException($"Operation timed out during creation of team project collection {collectionName}");
#else
            return null;
#endif
        }
    }
}