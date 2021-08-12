using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Extensions;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Adds new members to a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "TfsTeamMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(TeamAdmins))]
    public class AddTeamMember : CmdletBase
    {
        /// <summary>
        /// Specifies the member (user or group) to add to the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }

        /// <summary>
        /// Specifies the team to which the member is added.
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

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var member = GetItem<Models.Identity>(new {
                Identity = Member
            });

            var (_, _, t) = GetCollectionProjectAndTeam();

            var group = GetItem<Models.Identity>(new {
                Identity = t.Id
            });

            var client = GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>();

            this.Log($"Adding {member.IdentityType} '{member.DisplayName} ({member.UniqueName})' to team '{group.DisplayName}'");

            if (!ShouldProcess($"Team '{group.DisplayName}'", 
                $"Add member '{member.DisplayName} ({member.UniqueName})'")) return;

            client.AddMemberToGroupAsync(
                (IdentityDescriptor)group.Descriptor, 
                (IdentityDescriptor)member.Descriptor)
                .GetResult($"Error adding member '{member.DisplayName}' to team '{group.DisplayName}'");
        }
    }
}
