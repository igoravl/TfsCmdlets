using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace TfsCmdlets.Controllers.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets information from one or more release definitions in a team project.
    /// </summary>
    [CmdletController(typeof(ReleaseDefinition))]
    partial class GetReleaseDefinition
    {
        public override IEnumerable<ReleaseDefinition> Invoke()
        {
            throw new NotImplementedException();
        }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void DoProcessRecord()
        //     {
        //         if (Definition is Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition) { Logger.Log("Input item is of type Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition; returning input item immediately, without further processing."; WriteObject(Definition }); return;);

        //         # if(_TestGuid(Definition))
        //         # {
        //         #     tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        //         #     var client = Data.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

        //         #     task = client.GetRepositoryAsync(guid); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error getting repository with ID {guid}" task.Exception.InnerExceptions })

        //         #     WriteObject(result); return;
        //         # }

        //         tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         var client = Data.GetClient<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient2>();

        //         task = client.GetReleaseDefinitionsAsync(tp.Name); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error getting release definition '{Definition}'" task.Exception.InnerExceptions })

        //         WriteObject(result | Where-Object Name -Like Definition); return;
        //     }
        // }
    }
}