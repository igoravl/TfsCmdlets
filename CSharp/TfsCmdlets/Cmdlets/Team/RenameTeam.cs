using Microsoft.TeamFoundation.Core.WebApi;
using System.Management.Automation;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Renames a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeam))]
    public class RenameTeam : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public object Team { get; set; }
    }

    // TODO

    //partial class TeamDataService
    //{
    //    /// <summary>
    //    /// Performs execution of the command
    //    /// </summary>
    //    protected override Models.Team DoRenameItem()
    //    {
    //        var (_, tp, t) = GetCollectionProjectAndTeam();
    //        var newName = parameters.Get<string>("NewName");

    //        if(!PowerShell.ShouldProcess(tp, $"Rename team '{t.Name}' to '{newName}'")) return null;

    //        return new Models.Team(GetClient<TeamHttpClient>().UpdateTeamAsync(new WebApiTeam() {
    //                Name = newName
    //            }, tp.Id.ToString(), t.Id.ToString())
    //            .GetResult($"Error renaming team '{t.Name}' to '{newName}'"));
    //    }
    //}
}