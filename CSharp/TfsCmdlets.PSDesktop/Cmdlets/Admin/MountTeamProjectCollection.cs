using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.Client;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    partial class MountTeamProjectCollection
    {
    /// <summary>
    /// Performs execution of the command
    /// </summary>
    protected override void ProcessRecord()
        {
            var configServer = (TfsConfigurationServer) GetServer().InnerConnection;
            var tpcService = configServer.GetService<Microsoft.TeamFoundation.Framework.Client.ITeamProjectCollectionService>();
            var servicingTokens = new System.Collections.Generic.Dictionary<string,string>();

            if (ParameterSetName == "Use database server")
            {
                servicingTokens["CollectionDatabaseName"] = DatabaseName;
                ConnectionString = $"Data source={DatabaseServer}; Integrated Security=true; Initial Catalog={DatabaseName}";
            }

            try
            {
                //Write-Progress -Id 1 -Activity $"Attach team project collection" -Status "Attaching team project collection {Collection}" -PercentComplete 0

                var tpcJob = tpcService.QueueAttachCollection(ConnectionString, servicingTokens, Clone.ToBool(), Collection, Description,
                    $"~/{Collection}/");

                var collection = tpcService.WaitForCollectionServicingToComplete(tpcJob, Timeout);

                WriteObject(GetItem<Models.Connection>(new {Collection = this.Collection}));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error mounting collection '{Collection}': {ex}", ex);
            }
            finally
            {
                // Write-Progress -Id 1 -Activity "Attach team project collection" -Completed
            }
        }
    }
}