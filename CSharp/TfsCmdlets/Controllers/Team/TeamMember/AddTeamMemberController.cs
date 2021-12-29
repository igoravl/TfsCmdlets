using System.Management.Automation;
using TfsCmdlets.Cmdlets.Team.TeamMember;

namespace TfsCmdlets.Controllers.Team.TeamAdmin
{
    [CmdletController(typeof(Models.TeamMember))]
    partial class AddTeamMemberController
    {
        public override IEnumerable<Models.TeamMember> Invoke()
        {
            var team = Data.GetTeam();
            var member = Parameters.Get<object>(nameof(GetTeamMember.Member));

            var identities = Data.GetItems<Models.Identity>(new { Identity = member })
                .Where(i => !i.IsContainer)
                .ToList();

            if (identities.Count == 0)
            {
                Logger.LogWarn($"No identities found matching '{member}'");
                yield break;
            }

            var ids = identities.Select(i => i.Id);
            var uniqueNames = identities.Select(i => i.UniqueName);

            if (!PowerShell.ShouldProcess(team, $"Add team member(s) {string.Join(", ", uniqueNames)}")) yield break;

            var added = Data.Invoke(VerbsCommon.Add, "GroupMember", new
            {
                Group = team.Id,
                Member = identities
            }).Cast<Models.Identity>();

            foreach(var i in added)
            {
                yield return new Models.TeamMember(i.InnerObject, team);
            }
        }
    }
}
