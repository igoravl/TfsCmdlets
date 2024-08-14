using System.Management.Automation;
using TfsCmdlets.HttpClients;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    /// <summary>
    /// Adds a new administrator to a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, SupportsShouldProcess = true, DataType = typeof(Models.TeamAdmin),
        OutputType = typeof(WebApiIdentity))]
    partial class AddTeamAdmin
    {
        /// <summary>
        /// Specifies the administrator to add to the given team.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public object Admin { get; set; }
    }

    [CmdletController(typeof(Models.TeamAdmin), Client=typeof(ITeamAdminHttpClient))]
    partial class AddTeamAdminController
    {
        protected override IEnumerable Run()
        {
            var team = Data.GetTeam();
            var member = Parameters.Get<object>(nameof(AddTeamAdmin.Admin));

            var identities = Data.GetItems<Models.Identity>(new { Identity = member })
                .ToDictionary(i => i.Id.ToString());

            if (identities.Count == 0)
            {
                Logger.LogWarn($"No identities found matching '{member}'");
                yield break;
            }

            var ids = identities.Values.Select(i => i.Id);
            var uniqueNames = identities.Values.Select(i => i.UniqueName);

            if (!PowerShell.ShouldProcess(team, $"Add team administrator(s) {string.Join(", ", uniqueNames)}")) yield break;

            var result = Client.AddTeamAdmin(team.ProjectId, team.Id, ids);

            foreach (var addedAdmin in result)
            {
                if (!identities.ContainsKey(addedAdmin.TeamFoundationId)) continue;

                yield return new Models.TeamAdmin(identities[addedAdmin.TeamFoundationId].InnerObject, team);
            }
        }
    }
}
