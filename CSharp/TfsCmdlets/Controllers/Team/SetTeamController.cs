using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Cmdlets.Team;

namespace TfsCmdlets.Controllers.Team
{
    [CmdletController(typeof(WebApiTeam))]
    partial class SetTeamController
    {
        [Import]
        private IRestApiService RestApiService { get; set; }

        [Import]
        private INodeUtil NodeUtil { get; set; }

        public override IEnumerable<WebApiTeam> Invoke()
        {
            var tpc = Data.GetCollection();
            var tp = Data.GetProject();
            var t = Data.GetTeam(new { Default = false });

            var description = Parameters.Get<string>(nameof(SetTeam.Description));
            var defaultTeam = Parameters.Get<bool>(nameof(SetTeam.Default));
            var defaultAreaPath = Parameters.Get<string>(nameof(SetTeam.DefaultAreaPath));
            var areaPaths = Parameters.Get<IEnumerable<string>>(nameof(SetTeam.AreaPaths));
            var backlogIteration = Parameters.Get<string>(nameof(SetTeam.BacklogIteration));
            var iterationPaths = Parameters.Get<IEnumerable<string>>(nameof(SetTeam.IterationPaths));
            var backlogVisibilities = Parameters.Get<Hashtable>(nameof(SetTeam.BacklogVisibilities), new Hashtable()).ToDictionary<string, bool>();
            var defaultIterationMacro = Parameters.Get<string>(nameof(SetTeam.DefaultIterationMacro));
            var workingDays = Parameters.Get<IEnumerable<DayOfWeek>>(nameof(SetTeam.WorkingDays), Enumerable.Empty<DayOfWeek>()).ToList();
            var bugsBehavior = Parameters.Get<BugsBehavior>(nameof(SetTeam.BugsBehavior));

            var teamClient = Data.GetClient<TeamHttpClient>();
            var projectClient = Data.GetClient<ProjectHttpClient>();
            var workClient = Data.GetClient<WorkHttpClient>();

            // Set default team

            if (Parameters.HasParameter(nameof(SetTeam.Default)))
            {
                if (!PowerShell.ShouldProcess(tp, $"Set default team to {t.Name}")) yield break;

                var body = $@"{{
                    'contributionIds': [
                        'ms.vss-admin-web.admin-teams-data-provider'
                    ],
                    'dataProviderContext': {{
                        'properties': {{
                            'setDefaultTeam': true,
                            'teamId': '{t.Id}',
                            'sourcePage': {{
                                'url': '{((ReferenceLink)tp.Links.Links["web"]).Href}/_settings/teams',
                                'routeId': 'ms.vss-admin-web.project-admin-hub-route',
                                'routeValues': {{
                                    'project': '{tp.Name}',
                                    'adminPivot': 'teams',
                                    'controller': 'ContributedPage',
                                    'action': 'Execute' }} }} }} }} }}".Replace(" ", "");

                var result = RestApiService.InvokeAsync(tpc, "/_apis/Contribution/HierarchyQuery",
                    method: "POST",
                    body: body,
                    apiVersion: "6.1").SyncResult();

                yield return t;
                yield break;
            }

            // Set description

            if (Parameters.HasParameter("Description") && PowerShell.ShouldProcess(t, $"Set team's description to '{description}'"))
            {
                teamClient.UpdateTeamAsync(new WebApiTeam()
                {
                    Description = description ?? string.Empty
                }, tp.Id.ToString(), t.Id.ToString())
                .GetResult($"Error setting team '{t.Name}''s description to '{description}'");
            }

            // Set Team Field / Area Path settings

            var ctx = new TeamContext(tp.Name, t.Name);
            var teamFieldPatch = new TeamFieldValuesPatch();

            if (Parameters.HasParameter(nameof(SetTeam.DefaultAreaPath)) && PowerShell.ShouldProcess(t, $"Set team's default area path (team field) to '{defaultAreaPath}'"))
            {
                if (tpc.IsHosted)
                {
                    Logger.Log("Conected to Azure DevOps Services. Treating Team Field Value as Area Path");
                    defaultAreaPath = NodeUtil.NormalizeNodePath(defaultAreaPath, tp.Name, "Areas", includeTeamProject: true);
                }

                if (areaPaths == null)
                {
                    Logger.Log("AreaPaths is empty. Adding DefaultAreaPath (TeamFieldValue) to AreaPaths as default value.");

                    areaPaths = new string[] { defaultAreaPath };
                }

                var area = new { Node = defaultAreaPath };

                if (!Data.TestItem<Models.ClassificationNode>(area))
                {
                    Data.NewItem<Models.ClassificationNode>(area);
                }

                Logger.Log($"Setting default area path (team field) to {defaultAreaPath}");

                teamFieldPatch.DefaultValue = defaultAreaPath;
            }

            if (Parameters.HasParameter(nameof(SetTeam.AreaPaths)) && PowerShell.ShouldProcess(t, $"Set {string.Join(", ", areaPaths)} as team's area paths"))
            {
                var values = new List<TeamFieldValue>();

                foreach (var a in areaPaths)
                {
                    values.Add(new TeamFieldValue()
                    {
                        Value = NodeUtil.NormalizeNodePath(a.TrimEnd('\\', '*'), tp.Name, scope: "Areas", includeTeamProject: true),
                        IncludeChildren = a.EndsWith("*")
                    });
                }

                teamFieldPatch.Values = values;

                workClient.UpdateTeamFieldValuesAsync(teamFieldPatch, ctx)
                    .GetResult("Error applying team field value and/or area path settings");
            }

            // Set backlog and iteration path settings

            bool isDirty = false;
            var patch = new TeamSettingsPatch();

            if (Parameters.HasParameter(nameof(SetTeam.BacklogIteration)) && PowerShell.ShouldProcess(t, $"Set the team's backlog iteration to '{backlogIteration}'"))
            {
                var iteration = Data.GetItem<Models.ClassificationNode>(new
                {
                    Node = backlogIteration,
                    StructureGroup = TreeStructureGroup.Iterations
                });

                patch.BacklogIteration = patch.DefaultIteration = iteration.Identifier;

                isDirty = true;
            }

            if (Parameters.HasParameter(nameof(SetTeam.BacklogVisibilities)) && PowerShell.ShouldProcess(t, $"Set the team's backlog visibilities as '{backlogVisibilities.ToJsonString()}'"))
            {
                var backlogLevels = Data.GetItems<Models.BacklogLevelConfiguration>().ToList();
                var translatedVisibilities = new Dictionary<string, bool>();

                foreach(var kv in backlogVisibilities)
                {
                    var newKey = backlogLevels.FirstOrDefault(l => kv.Key.Equals(l.Name, StringComparison.OrdinalIgnoreCase))?.Id ?? kv.Key;
                    translatedVisibilities.Add(newKey, kv.Value);
                }

                patch.BacklogVisibilities = translatedVisibilities;
                isDirty = true;
            }

            if (Parameters.HasParameter(nameof(SetTeam.DefaultIterationMacro)) && PowerShell.ShouldProcess(t, $"Set the team's default iteration macro to {defaultIterationMacro}"))
            {
                patch.DefaultIterationMacro = defaultIterationMacro;
                isDirty = true;
            }

            if (Parameters.HasParameter(nameof(SetTeam.WorkingDays)) && PowerShell.ShouldProcess(t, $"Set the team's working days to {workingDays.ToJsonString()}"))
            {
                patch.WorkingDays = workingDays.ToArray();
                isDirty = true;
            }

            if (Parameters.HasParameter(nameof(SetTeam.BugsBehavior)) && PowerShell.ShouldProcess(t, $"Set the team's bugs behavior to '{bugsBehavior}'"))
            {
                patch.BugsBehavior = bugsBehavior;
                isDirty = true;
            }

            if (isDirty)
            {
                workClient.UpdateTeamSettingsAsync(patch, ctx)
                    .GetResult("Error applying iteration settings");
            }

            // Set iteration paths

            if (Parameters.HasParameter(nameof(SetTeam.IterationPaths)) && PowerShell.ShouldProcess(t, $"Set the team's iteration paths to '{iterationPaths.ToJsonString()}'"))
            {
                var currentIterations = workClient.GetTeamIterationsAsync(ctx)
                    .GetResult("Error getting team's current iterations");

                var newIterations = iterationPaths
                    .Select(i => Data.GetItem<Models.ClassificationNode>(new { Node = i, StructureGroup = TreeStructureGroup.Iterations }))
                    .Select(n => new TeamSettingsIteration() { Id = n.Identifier })
                    .ToList();

                foreach (var iteration in newIterations)
                {
                    workClient.PostTeamIterationAsync(iteration, ctx)
                        .GetResult($"Error setting iteration {iteration.Id} as a sprint iteration");
                }

                foreach (var iteration in currentIterations.Where(i => !newIterations.Any(ni => ni.Id == i.Id)))
                {
                    workClient.DeleteTeamIterationAsync(ctx, iteration.Id)
                        .Wait($"Error removing iteration '{iteration.Id}' from the team's sprint iterations");
                }
            }

            yield return Data.GetItem<WebApiTeam>(new { Default = false });
        }
    }
}