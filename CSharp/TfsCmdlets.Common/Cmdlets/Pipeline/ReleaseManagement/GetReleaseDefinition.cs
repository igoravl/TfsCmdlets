/*
.SYNOPSIS
    Gets information from one or more release definitions in a team project.

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
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    [Cmdlet(VerbsCommon.Get, "ReleaseDefinition")]
    [OutputType(typeof(ReleaseDefinition))]
    public class GetReleaseDefinition: BaseCmdlet
    {
/*
        [Parameter(Position=0)]
        [SupportsWildcards()]
        [Alias("Name")]
        [object] 
        Definition = "*",

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
    }

    protected override void ProcessRecord()
    {
        if (Definition is Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition) { this.Log("Input item is of type Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition; returning input item immediately, without further processing."; WriteObject(Definition }); return;);
        
        # if(_TestGuid(Definition))
        # {
        #     tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
            
        #     var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();

        #     task = client.GetRepositoryAsync(guid); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error getting repository with ID {guid}" task.Exception.InnerExceptions })

        #     WriteObject(result); return;
        # }

        tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        var client = tpc.GetClient<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients.ReleaseHttpClient2>();

        task = client.GetReleaseDefinitionsAsync(tp.Name); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error getting release definition "{Definition}"" task.Exception.InnerExceptions })
        
        WriteObject(result | Where-Object Name -Like Definition); return;
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
