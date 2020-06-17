using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Work
{
    /// <summary>
    /// Gets information about one or more backlogs of the given team.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsTeamBacklog")]
    [OutputType(typeof(BacklogLevelConfiguration))]
    public class GetTeamBacklog : BaseCmdlet
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();

        //         [Parameter(Position=0)]
        //         [Alias("Name")]
        //         [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration])}) 
        //         [SupportsWildcards()]
        //         public object Backlog { get; set; } = "*";

        //         [Parameter(ValueFromPipeline=true)]
        //         public object Team { get; set; }

        //         [Parameter()]
        //         public object Project { get; set; }

        //         [Parameter()]
        //         public object Collection { get; set; }

        //     protected override void BeginProcessing()
        //     {
        //         # #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Work.WebApi"
        //         # #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.WebApi"
        //     }

        //         /// <summary>
        //         /// Performs execution of the command
        //         /// </summary>
        //         protected override void ProcessRecord()
        //     {
        //         if (Backlog is Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration) { this.Log("Input item is of type Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration; returning input item immediately, without further processing."; WriteObject(Backlog }); return;);
        //         t = Get-TfsTeam -Team Team -Project Project -Collection Collection
        //         if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        //         var client = GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();
        //         ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(@(tp.Name, t.Name))

        //         if (! Backlog.ToString().Contains("*"))
        //         {
        //             this.Log($"Get backlog "{Backlog}"");
        //             task = client.GetBacklogAsync(ctx, Backlog)

        //             result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error getting backlog "{Backlog}"" task.Exception.InnerExceptions })
        //         }
        //         else
        //         {
        //             this.Log($"Get all backlogs matching "{Backlog}"");
        //             task = client.GetBacklogsAsync(ctx)
        //             result = task.Result; if(task.IsFaulted) { _throw new Exception("Error enumerating backlogs" task.Exception.InnerExceptions })

        //             result = result | Where-Object Name -like Backlog
        //         }

        //         WriteObject(result); return;
        //     }
        // }
    }
}
