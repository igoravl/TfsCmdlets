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

using System.Linq;
using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet(VerbsCommon.Get, "TeamProject", DefaultParameterSetName = "Get by project")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    public class GetTeamProject : BaseCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "Get by project")]
        public object Project { get; set; } = "*";

        [Parameter(ValueFromPipeline = true, Position = 1, ParameterSetName = "Get by project")]
        public object Collection { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(this.GetMany<Microsoft.TeamFoundation.Core.WebApi.TeamProject>().ToList(), true);
        }
    }
}