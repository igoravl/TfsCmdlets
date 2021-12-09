using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Connects to a Team Project.
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "TfsTeamProject", DefaultParameterSetName = "Prompt for credential")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class ConnectTeamProject
    {
        /// <summary>
        /// Specifies the name of the Team Project, its ID (a GUID), or a 
        /// Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public object Project { get; set; }
    }
}