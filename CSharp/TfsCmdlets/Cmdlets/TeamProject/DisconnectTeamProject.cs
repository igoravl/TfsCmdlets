using System.Management.Automation;
using TfsCmdlets.Services.Impl;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Disconnects from the currently connected team project.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsTeamProject cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsTeamProject. Therefore, cmdlets relying on a "default team project" 
    /// as provided by "Get-TfsTeamProject -Current" will no longer work after a call to 
    /// this cmdlet, unless their -Project argument is provided or a new call to 
    /// Connect-TfsTeamProject is made.
    /// </remarks>
    [Cmdlet(VerbsCommunications.Disconnect, "TfsTeamProject")]
    public class DisconnectTeamProject : CmdletBase
    {
    }
}