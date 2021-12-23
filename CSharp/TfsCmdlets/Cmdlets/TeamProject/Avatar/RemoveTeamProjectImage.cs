using System.Management.Automation;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    /// <summary>
    /// Removes the team project avatar, resetting it to the default avatar.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true)]
    partial class RemoveTeamProjectAvatar
    {
    }
}