/*
.SYNOPSIS
Gets information about one or more backlogs of the given team.

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
Microsoft.TeamFoundation.Core.WebApi.WebApiTeam
System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.Work.WebApi;

namespace TfsCmdlets.Cmdlets.Work
{
    [Cmdlet(VerbsCommon.Get, "TeamBacklog")]
    [OutputType(typeof(BacklogLevelConfiguration))]
    public class GetTeamBacklog: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [Alias("Name")]
        [ValidateScript({(_ is string]) || (_ is [Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration])}) 
        [SupportsWildcards()]
        public object Backlog { get; set; } = "*";

        [Parameter(ValueFromPipeline=true)]
        public object Team { get; set; }

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        # #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Work.WebApi"
        # #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.WebApi"
    }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        if (Backlog is Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration) { this.Log("Input item is of type Microsoft.TeamFoundation.Work.WebApi.BacklogLevelConfiguration; returning input item immediately, without further processing."; WriteObject(Backlog }); return;);
        t = Get-TfsTeam -Team Team -Project Project -Collection Collection
        if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        var client = GetClient<Microsoft.TeamFoundation.Work.WebApi.WorkHttpClient>();
        ctx = new Microsoft.TeamFoundation.Core.WebApi.Types.TeamContext(@(tp.Name, t.Name))

        if (! Backlog.ToString().Contains("*"))
        {
            this.Log($"Get backlog "{Backlog}"");
            task = client.GetBacklogAsync(ctx, Backlog)

            result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error getting backlog "{Backlog}"" task.Exception.InnerExceptions })
        }
        else
        {
            this.Log($"Get all backlogs matching "{Backlog}"");
            task = client.GetBacklogsAsync(ctx)
            result = task.Result; if(task.IsFaulted) { _throw new Exception("Error enumerating backlogs" task.Exception.InnerExceptions })
            
            result = result | Where-Object Name -like Backlog
        }

        WriteObject(result); return;
    }
}
*/
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
    }
}
