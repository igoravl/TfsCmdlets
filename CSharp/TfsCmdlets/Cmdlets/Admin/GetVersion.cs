using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Gets the version information about Team Foundation / Azure DevOps servers and 
    ///   Azure DevOps Services organizations.
    /// </summary>
    /// <remarks>
    /// The Get-TfsVersion cmdlet retrieves version information from the supplied team project collection or Azure DevOps organization. 
    /// When available/applicable, detailed information about installed updates, deployed sprints and so on are also provided.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(ServerVersion))]
    partial class GetVersion 
    {
    }
}
