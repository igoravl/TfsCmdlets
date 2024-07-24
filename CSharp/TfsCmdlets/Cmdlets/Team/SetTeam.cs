using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Changes the details of a team.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(WebApiTeam))]
    partial class SetTeam
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Set team settings")]
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Set default team")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Team { get; set; }

        /// <summary>
        /// Sets the specified team as the default team.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Set default team")]
        public SwitchParameter Default { get; set; }

        /// <summary>
        /// Specifies a new description
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string Description { get; set; }

        /// <summary>
        /// Specifies the team's default area path (or "team field"). The default area path is assigned
        /// automatically to all work items created in a team's backlog and/or board.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        [Alias("TeamFieldValue")]
        public string DefaultAreaPath { get; set; }

        /// <summary>
        /// Specifies the backlog area path(s) that are associated with this team. Wildcards are supported. 
        /// When the path ends with an asterisk, all child area paths will be included recursively. 
        /// Otherwise, only the area itself (without its children) will be included.
        /// To include the children of the default area path, use the wildcard character (*) without a path.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        [SupportsWildcards]
        public string[] AreaPaths { get; set; }

        /// <summary>
        /// Replaces the existing area paths with the specified list of area paths. 
        /// When omitted, the new area paths are added alongside the previously defined ones.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public SwitchParameter OverwriteAreaPaths { get; set; }

        /// <summary>
        /// Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string BacklogIteration { get; set; } = "\\";

        /// <summary>
        /// Specifies the default iteration macro. 
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string DefaultIterationMacro { get; set; }

        /// <summary>
        /// Specifies the backlog iteration path(s) that are associated with this team. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] IterationPaths { get; set; }

        /// <summary>
        /// Replaces the existing iteration paths with the specified list of iteration paths. 
        /// When omitted, the new iteration paths are added alongside the previously defined ones.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public SwitchParameter OverwriteIterationPaths { get; set; }

        /// <summary>
        ///  Specifies the team's Working Days.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public DayOfWeek[] WorkingDays { get; set; }

        /// <summary>
        /// Specifies how bugs should behave when added to a board.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public BugsBehavior BugsBehavior { get; set; }

        /// <summary>
        /// Specifies which backlog levels (e.g. Epics, Features, Stories) should be visible.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public Hashtable BacklogVisibilities { get; set; }

        /// <summary>
        /// Allows the cmdlet to create target area and/or iteration nodes if they're missing.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(Models.Team))]
    partial class SetTeamController
    {
        [Import]
        private IRestApiService RestApiService { get; set; }

        [Import]
        private INodeUtil NodeUtil { get; set; }

        protected override IEnumerable Run()
        {
            var t = Data.GetTeam(includeSettings: true);

            var backlogVisibilities = (BacklogVisibilities ?? new Hashtable()).ToDictionary<string, bool>();
            var workingDays = (WorkingDays ?? Enumerable.Empty<DayOfWeek>()).ToList();

            var teamClient = GetClient<TeamHttpClient>();
            var projectClient = GetClient<ProjectHttpClient>();
            var workClient = GetClient<WorkHttpClient>();

            var ctx = new TeamContext(Project.Name, t.Name);
            var teamFieldPatch = new TeamFieldValuesPatch();

            // Set default team

            if (Has_Default && PowerShell.ShouldProcess(Project, $"Set default team to {t.Name}"))
            {
                var body = $@"{{
                    'contributionIds': [
                        'ms.vss-admin-web.admin-teams-data-provider'
                    ],
                    'dataProviderContext': {{
                        'properties': {{
                            'setDefaultTeam': true,
                            'teamId': '{t.Id}',
                            'sourcePage': {{
                                'url': '{((ReferenceLink)Project.Links.Links["web"]).Href}/_settings/teams',
                                'routeId': 'ms.vss-admin-web.project-admin-hub-route',
                                'routeValues': {{
                                    'project': '{Project.Name}',
                                    'adminPivot': 'teams',
                                    'controller': 'ContributedPage',
                                    'action': 'Execute' }} }} }} }} }}".Replace(" ", "");

                var result = RestApiService.InvokeAsync(Collection, "/_apis/Contribution/HierarchyQuery",
                    method: "POST",
                    body: body,
                    apiVersion: "6.1").SyncResult();

                yield return t;
                yield break;
            }

            // Set description

            if (Has_Description && !t.Description.Equals(Description) && PowerShell.ShouldProcess(t, $"Set team's description to '{Description}'"))
            {
                t = teamClient.UpdateTeamAsync(new WebApiTeam()
                {
                    Description = Description ?? string.Empty
                }, Project.Id.ToString(), t.Id.ToString())
                .GetResult($"Error setting team '{t.Name}''s description to '{Description}'");
            }

            // Set Team Field / Area Path settings

            bool isDirty = false;

            if (Has_DefaultAreaPath && PowerShell.ShouldProcess(t, $"Set team's default area path (team field) to '{DefaultAreaPath}'"))
            {
                var usesAreaPath = t.TeamField.Field.ReferenceName.Equals("System.AreaPath");

                if (usesAreaPath)
                {
                    Logger.Log("Treating Team Field Value as Area Path");
                    DefaultAreaPath = NodeUtil.NormalizeNodePath(DefaultAreaPath, Project.Name, "Areas", 
                        includeTeamProject: true, 
                        includeLeadingSeparator: true);

                    var a = new { Node = DefaultAreaPath, StructureGroup = TreeStructureGroup.Areas };

                    if (!Data.TryGetItem<Models.ClassificationNode>(out var area, a))
                    {
                        if (!Force) throw new ArgumentException($"Area Path '{DefaultAreaPath}' does not exist. To create a new area, use the -Force switch.");

                        area = Data.NewItem<Models.ClassificationNode>(a);
                    }

                    DefaultAreaPath = area.FullPath.Substring(1);
                }

                if (AreaPaths == null ||
                    AreaPaths.Length == 0 ||
                    !AreaPaths.Any(ap =>
                        ap.Equals("*") ||
                        NodeUtil.NormalizeNodePath(ap, Project.Name, "Areas", includeTeamProject: true).Equals(DefaultAreaPath)))
                {
                    Logger.Log("DefaultAreaPath is not included in AreaPaths. Adding DefaultAreaPath (TeamFieldValue) to AreaPaths.");

                    AreaPaths = new string[] { DefaultAreaPath };
                    Has_AreaPaths = true;
                }

                Logger.Log($"Setting default area path (team field) to {DefaultAreaPath}");

                teamFieldPatch.DefaultValue = DefaultAreaPath;
                isDirty = true;
            }

            // Set area path values

            if (Has_AreaPaths && PowerShell.ShouldProcess(t, $"Set [{string.Join("; ", AreaPaths)}] as team's area path(s)"))
            {
                var values = new List<TeamFieldValue>();
                teamFieldPatch.DefaultValue ??= t.TeamField.DefaultValue;

                if (!OverwriteAreaPaths)
                {
                    values.AddRange(t.TeamField.Values);
                }

                foreach (var a in AreaPaths)
                {
                    Logger.Log($"Processing area path(s) '{a}'");

                    var includeChildren = a.Equals("*") || a.EndsWith("\\*") || a.EndsWith("/*");
                    var path = a.Equals("*") ?
                        teamFieldPatch.DefaultValue :
                        NodeUtil.NormalizeNodePath(includeChildren ? a.Substring(0, a.Length - 2) : a, Project.Name, "Areas", includeTeamProject: true);

                    if (path.IsWildcard())
                    {
                        Logger.Log($"Area path is an wildcard. Searching for matching area paths.");
                        var matchingAreas = GetItems<Models.ClassificationNode>(new { Node = path, StructureGroup = TreeStructureGroup.Areas });

                        foreach (var matchingArea in matchingAreas)
                        {
                            Logger.Log($"Adding area '{matchingArea.FullPath}'");
                            values.Add(new TeamFieldValue() { Value = matchingArea.FullPath, IncludeChildren = includeChildren });
                        }

                        continue;
                    }

                    Logger.Log($"Adding area '{path}'");

                    TeamFieldValue existingValue;

                    if ((existingValue = values.FirstOrDefault(v => v.Value.Equals(path))) != null)
                    {
                        existingValue.IncludeChildren = includeChildren;
                    }
                    else
                    {
                        values.Add(new TeamFieldValue()
                        {
                            Value = path,
                            IncludeChildren = includeChildren
                        });
                    }
                }

                if (!values.Any(v => v.Value.Equals(teamFieldPatch.DefaultValue)))
                {
                    values.Add(new TeamFieldValue()
                    {
                        Value = teamFieldPatch.DefaultValue,
                        IncludeChildren = false
                    });
                }

                teamFieldPatch.Values = values;
                isDirty = true;
            }

            if (isDirty)
            {
                workClient.UpdateTeamFieldValuesAsync(teamFieldPatch, ctx)
                    .GetResult("Error applying team field value and/or area path settings");
            }

            // Set backlog and iteration path settings

            isDirty = false;
            var patch = new TeamSettingsPatch();

            if (Has_BacklogIteration && PowerShell.ShouldProcess(t, $"Set the team's backlog iteration to '{BacklogIteration}'"))
            {
                BacklogIteration = NodeUtil.NormalizeNodePath(BacklogIteration, Project.Name, "Iterations", includeTeamProject: true);

                var iter = new { Node = BacklogIteration, StructureGroup = TreeStructureGroup.Iterations };

                if (!Data.TryGetItem<Models.ClassificationNode>(out var iteration, iter))
                {
                    iteration = Data.NewItem<Models.ClassificationNode>(iter);
                }

                patch.BacklogIteration = patch.DefaultIteration = iteration.Identifier;

                isDirty = true;
            }

            if (Has_BacklogVisibilities && PowerShell.ShouldProcess(t, $"Set the team's backlog visibilities as '{backlogVisibilities.ToJsonString()}'"))
            {
                var backlogLevels = GetItems<Models.BacklogLevelConfiguration>().ToList();
                var translatedVisibilities = new Dictionary<string, bool>();

                foreach (var kv in backlogVisibilities)
                {
                    var newKey = backlogLevels.FirstOrDefault(l => kv.Key.Equals(l.Name, StringComparison.OrdinalIgnoreCase))?.Id ?? kv.Key;
                    translatedVisibilities.Add(newKey, kv.Value);
                }

                patch.BacklogVisibilities = translatedVisibilities;
                isDirty = true;
            }

            if (Has_DefaultIterationMacro && PowerShell.ShouldProcess(t, $"Set the team's default iteration macro to {DefaultIterationMacro}"))
            {
                patch.DefaultIterationMacro = DefaultIterationMacro;
                isDirty = true;
            }

            if (Has_WorkingDays && PowerShell.ShouldProcess(t, $"Set the team's working days to {workingDays.ToJsonString()}"))
            {
                patch.WorkingDays = workingDays.ToArray();
                isDirty = true;
            }

            if (Has_BugsBehavior && PowerShell.ShouldProcess(t, $"Set the team's bugs behavior to '{BugsBehavior}'"))
            {
                patch.BugsBehavior = BugsBehavior;
                isDirty = true;
            }

            if (isDirty)
            {
                workClient.UpdateTeamSettingsAsync(patch, ctx)
                    .GetResult("Error applying iteration settings");
            }

            // Set iteration paths

            if (Parameters.HasParameter(nameof(SetTeam.IterationPaths)) && PowerShell.ShouldProcess(t, $"Set the team's iteration paths to '{IterationPaths.ToJsonString()}'"))
            {
                var currentIterations = workClient.GetTeamIterationsAsync(ctx)
                    .GetResult("Error getting team's current iterations");

                var newIterations = IterationPaths
                    .SelectMany(i => GetItems<Models.ClassificationNode>(new { Node = i, StructureGroup = TreeStructureGroup.Iterations }))
                    .Select(n => new TeamSettingsIteration() { Id = n.Identifier, Name = n.Name })
                    .ToList();

                foreach (var iteration in newIterations)
                {
                    Logger.Log($"Adding iteration '{iteration.Name}'");
                    workClient.PostTeamIterationAsync(iteration, ctx)
                        .GetResult($"Error setting iteration {iteration.Id} as a sprint iteration");
                }

                if (OverwriteIterationPaths)
                {
                    foreach (var iteration in currentIterations.Where(i => !newIterations.Any(ni => ni.Id == i.Id)))
                    {
                        workClient.DeleteTeamIterationAsync(ctx, iteration.Id)
                            .Wait($"Error removing iteration '{iteration.Id}' from the team's sprint iterations");
                    }
                }
            }

            yield return Data.GetItem<Models.Team>(new { Default = false, IncludeSettings = true });
        }
    }
}