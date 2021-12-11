using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Connects to a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Prompt for credential", OutputType = typeof(WebApiTeam))]
    partial class ConnectTeam
    {
        /// <summary>
        ///   Specifies the name of the Team, its ID (a GUID), or a 
        ///   Microsoft.TeamFoundation.Core.WebApi.WebApiTeam object to connect to. For more details, 
        ///   see the Get-TfsTeam cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object Team { get; set; }
    }
}