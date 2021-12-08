using System.Management.Automation;
using System.Security;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Services.Impl;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Connects to a team.
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "TfsTeam", DefaultParameterSetName = "Prompt for credential")]
    [OutputType(typeof(WebApiTeam))]
    [TfsCmdlet(CmdletScope.Project)]
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