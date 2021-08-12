using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    partial class DismountTeamProjectCollection
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var tpc = GetCollection();
            var srv = tpc.ConfigurationServer;

            if (!ShouldProcess($"Server '{srv.Uri}'", $"Detach collection '{tpc.DisplayName}'")) return;

			var tpcService = srv.GetService<Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService>();
			var collectionInfo = tpcService.GetCollection(tpc.InnerConnection.InstanceId);

			var tpcJob = tpcService.QueueDetachCollection(collectionInfo, null, Reason, out var connectionString);
			collectionInfo = tpcService.WaitForCollectionServicingToComplete(tpcJob, Timeout);

			WriteObject(connectionString); 
        }
    }
}