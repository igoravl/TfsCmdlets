namespace TfsCmdlets.Cmdlets.Organization
{
    /// <summary>
    /// Disconnects from the currently connected Azure DevOps organization.
    /// </summary>
    /// <remarks>
    /// The Disconnect-TfsOrganization cmdlet removes the connection previously set by its 
    /// counterpart Connect-TfsOrganization. Therefore, cmdlets relying on a "default organization/collection" 
    /// as provided by "Get-TfsOrganization -Current" will no longer work after a call to 
    /// this cmdlet, unless their -Collection argument is provided or a new call to 
    /// Connect-TfsTeam is made.
    /// </remarks>
    [TfsCmdlet(CmdletScope.None, CustomControllerName = "DisconnectTeamProjectCollection")]
    partial class DisconnectOrganization
    {
    }
}