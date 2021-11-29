using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity.Client;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Removes a member from a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsTeamMember", SupportsShouldProcess = true)]
    public class RemoveTeamMember : CmdletBase
    {
        /// <summary>
        /// Specifies the member (user or group) to remove from the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }

        /// <summary>
        /// Specifies the team from which the member is removed.
        /// </summary>
        [Parameter(Position = 1)]
        public object Team { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        // TODO

        ///// <summary>
        ///// Performs execution of the command
        ///// </summary>
        //protected override void DoProcessRecord()
        //{
        //    var member = GetItem<Models.Identity>(new
        //    {
        //        Identity = Member
        //    });

        //    ErrorUtil.ThrowIfNotFound(member, nameof(member), Member);

        //    var (_, _, t) = GetCollectionProjectAndTeam();

        //    var group = GetItem<Models.Identity>(new
        //    {
        //        Identity = t.Id
        //    });

        //    ErrorUtil.ThrowIfNotFound(group, nameof(group), Team);

        //    var client = Data.GetClient<IdentityHttpClient>(parameters);

        //    if (!PowerShell.ShouldProcess($"Team '{t.Name}'",
        //        $"Remove member '{member.DisplayName} ({member.UniqueName})'"))
        //    {
        //        return;
        //    }

        //    Logger.Log($"Removing '{member.DisplayName} ({member.UniqueName}))' from team '{t.Name}'");

        //    client.RemoveMemberFromGroupAsync(group.Descriptor, member.Descriptor)
        //        .GetResult($"Error removing '{member.DisplayName} ({member.UniqueName}))' from group '{group.DisplayName}'");
        //}
    }
}