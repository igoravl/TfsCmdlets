/*
.SYNOPSIS
Gets information about one or more team projects. 

.DESCRIPTION
The Get-TfsTeamProject cmdlets gets one or more Team Project objects (an instance of Microsoft.TeamFoundation.WorkItemTracking.Client.Project) from the supplied Team Project Collection.

.PARAMETER Project
Specifies the name of a Team Project. Wildcards are supported.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.NOTES
As with most cmdlets in the TfsCmdlets module, this cmdlet requires a TfsTeamProjectCollection object to be provided via the -Collection argument. If absent, it will default to the connection opened by Connect-TfsTeamProjectCollection.

*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet(VerbsCommon.Get, "TeamProject", DefaultParameterSetName = "Get by project")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    public class GetTeamProject: PSCmdlet
    {
/*
        [Parameter(Position = 0, ParameterSetName = "Get by project")]
        [object] 
        Project = "*",

        [Parameter(ValueFromPipeline = true, Position = 1, ParameterSetName = "Get by project")]
        public object Collection { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

    protected override void BeginProcessing()
    {
        // _Requires "Microsoft.TeamFoundation.Core.WebApi"
    }

    protected override void ProcessRecord()
    {
        if (Current)
        {
            WriteObject([TfsCmdlets.CurrentConnections]::Project); return;
        }

        if (Project is Microsoft.TeamFoundation.Core.WebApi.TeamProject) { _Log "Input item is of type Microsoft.TeamFoundation.Core.WebApi.TeamProject; returning input item immediately, without further processing."; WriteObject(Project }); return;

        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        client = Get-TfsRestClient "Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient" -Collection tpc

        if ((_TestGuid Project) || (! (_IsWildcard Project)))
        {
            task = client.GetProject([string] Project, true); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error getting team project "{Project}"" task.Exception.InnerExceptions })

            WriteObject(result); return;
        }

        task = client.GetProjects(); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error getting team project(s) matching "{Project}"" task.Exception.InnerExceptions })

        result | Where-Object Name -like Project | ForEach-Object {
            task = client.GetProject(_.Id, true); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error getting team project "{{_}.Id}"" task.Exception.InnerExceptions })
            Write-Output result
        }
    }
}
*/
}
}
