using System.Management.Automation;
using TfsCmdlets.Cmdlets.Team.TeamMember;

namespace TfsCmdlets.Controllers.Team.TeamMember
{
    [CmdletController(typeof(Models.TeamMember))]
    partial class AddTeamMemberController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam();
            var member = Parameters.Get<object>(nameof(GetTeamMember.Member));

            var identities = Enumerable.Where<Models.Identity>(Data.GetItems<Models.Identity>(new { Identity = member }), i => !i.IsContainer)
                .ToList();

            if (identities.Count == 0)
            {
                Logger.LogWarn($"No identities found matching '{member}'");
                yield break;
            }

            var ids = identities.Select(i => i.Id);
            var uniqueNames = identities.Select(i => i.UniqueName);

            if (!PowerShell.ShouldProcess(team, $"Add team member(s) {string.Join(", ", uniqueNames)}")) yield break;

            var added = Enumerable.Cast<Models.Identity>(Data.Invoke(VerbsCommon.Add, "GroupMember", new
            {
                Group = team.Id,
                Member = identities
            }));

            foreach(var i in added)
            {
                yield return new Models.TeamMember(i.InnerObject, team);
            }
        }
    }
}
