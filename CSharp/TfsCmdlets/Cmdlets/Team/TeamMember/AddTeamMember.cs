using System.Management.Automation;
using TfsCmdlets.HttpClients;

namespace TfsCmdlets.Cmdlets.Team.TeamMember
{
    /// <summary>
    /// Adds new members to a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, DataType = typeof(Models.TeamMember),
        OutputType = typeof(WebApiIdentity))]
    partial class AddTeamMember
    {
        /// <summary>
        /// Specifies the member (user or group) to add to the given team.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }
    }

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