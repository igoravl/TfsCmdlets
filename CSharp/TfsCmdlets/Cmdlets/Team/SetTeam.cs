using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Team
{
    /// <summary>
    /// Changes the details of a team.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "TfsTeam", SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiTeam))]
    public class SetTeam : CmdletBase
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
        /// Specifies the backlog area paths that are associated with this team. Provide a list 
        /// of area paths in the form '/path1/path2/[*]'. When the path ends with an asterisk, all
        /// child area path will be included recursively. Otherwise, only the area itself (without 
        /// its children) will be included.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] AreaPaths { get; set; }

        /// <summary>
        /// Specifies the team's backlog iteration path. When omitted, defaults to the team project's root iteration.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string BacklogIteration { get; set; } = "\\";

        /// <summary>
        /// Specifies the backlog iteration paths that are associated with this team. Provide a list 
        /// of iteration paths in the form '/path1/path2'.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string[] IterationPaths { get; set; }

        /// <summary>
        /// Specifies the default iteration macro. When omitted, defaults to "@CurrentIteration".
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public string DefaultIterationMacro { get; set; } = "@CurrentIteration";

        /// <summary>
        ///  Specifies the team's Working Days. When omitted, defaults to Monday thru Friday
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public IEnumerable<string> WorkingDays = new[] { "monday", "tuesday", "wednesday", "thursday", "friday" };

        /// <summary>
        /// Specifies how bugs should behave when added to a board.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        [ValidateSet("AsTasks", "AsRequirements", "Off")]
        public string BugsBehavior { get; set; }

        /// <summary>
        /// Specifies which backlog levels (e.g. Epics, Features, Stories) should be visible.
        /// </summary>
        [Parameter(ParameterSetName = "Set team settings")]
        public Hashtable BacklogVisibilities { get; set; }
    }

    // TODO

    //    partial class TeamDataService
    //    {
    //        protected override Models.Team DoSetItem()
    //        {
    //            var (tpc, tp, t) = GetCollectionProjectAndTeam(ParameterDictionary.From(this.Parameters, new
    //            {
    //                Default = false
    //            }));

    //            if (HasParameter(nameof(SetTeam.Default)))
    //            {
    //                if (!PowerShell.ShouldProcess(tp.Name, $"Set default team to {t.Name}")) return t;

    //                var body = $@"
    //{{
    //    'contributionIds': [
    //        'ms.vss-admin-web.admin-teams-data-provider'
    //    ],
    //    'dataProviderContext': {{
    //        'properties': {{
    //            'setDefaultTeam': true,
    //            'teamId': '{t.Id}',
    //            'sourcePage': {{
    //                'url': '{((ReferenceLink)tp.Links.Links["web"]).Href}/_settings/teams',
    //                'routeId': 'ms.vss-admin-web.project-admin-hub-route',
    //                'routeValues': {{
    //                    'project': '{tp.Name}',
    //                    'adminPivot': 'teams',
    //                    'controller': 'ContributedPage',
    //                    'action': 'Execute'
    //                }}
    //            }}
    //        }}
    //    }}
    //}}";

    //                var svc = GetService<IRestApiService>();

    //                var result = svc.InvokeAsync(tpc, "/_apis/Contribution/HierarchyQuery",
    //                    method: "POST",
    //                    body: body,
    //                    apiVersion: "6.1").SyncResult();

    //                return t;
    //            }

    //            var description = parameters.Get<string>(nameof(SetTeam.Description));
    //            var defaultTeam = parameters.Get<bool>(nameof(SetTeam.Default));
    //            var defaultAreaPath = parameters.Get<string>(nameof(SetTeam.DefaultAreaPath));
    //            var areaPaths = parameters.Get<IEnumerable<string>>(nameof(SetTeam.AreaPaths));
    //            var backlogIteration = parameters.Get<string>(nameof(SetTeam.BacklogIteration));
    //            var iterationPaths = parameters.Get<IEnumerable<string>>(nameof(SetTeam.IterationPaths));

    //            var teamClient = GetClient<TeamHttpClient>(parameters);
    //            var projectClient = GetClient<ProjectHttpClient>(parameters);
    //            var workClient = GetClient<WorkHttpClient>(parameters);

    //            // Set description

    //            if (HasParameter("Description") && ShouldProcess(t, $"Set team's description to '{description}'"))
    //            {
    //                teamClient.UpdateTeamAsync(new WebApiTeam()
    //                {
    //                    Description = description ?? string.Empty
    //                }, tp.Id.ToString(), t.Id.ToString())
    //                .GetResult($"Error setting team '{t.Name}''s description to '{description}'");
    //            }

    //            // Set default team

    //            if (defaultTeam && ShouldProcess(tp, $"Set team '{t.Name} as default'"))
    //            {
    //                throw new NotImplementedException("Set team as default is currently not supported");
    //            }

    //            // Set Team Field / Area Path settings

    //            var ctx = new TeamContext(tp.Name, t.Name);
    //            var teamFieldPatch = new TeamFieldValuesPatch();

    //            if (!string.IsNullOrEmpty(defaultAreaPath) &&
    //                ShouldProcess(t, $"Set team's default area path (team field) to '{defaultAreaPath}'"))
    //            {
    //                if (tpc.IsHosted)
    //                {
    //                    this.Log("Conected to Azure DevOps Services. Treating Team Field Value as Area Path");
    //                    defaultAreaPath = NodeUtil.NormalizeNodePath(defaultAreaPath, tp.Name, "Areas", includeTeamProject: true);
    //                }

    //                if (areaPaths == null)
    //                {
    //                    this.Log("AreaPaths is empty. Adding DefaultAreaPath (TeamFieldValue) to AreaPaths as default value.");

    //                    areaPaths = new string[] { defaultAreaPath };
    //                }

    //                var area = new { Node = defaultAreaPath };

    //                if (!TestItem<Models.ClassificationNode>(area))
    //                {
    //                    NewItem<Models.ClassificationNode>(area);
    //                }

    //                this.Log($"Setting default area path (team field) to {defaultAreaPath}");

    //                teamFieldPatch.DefaultValue = defaultAreaPath;
    //            }

    //            if (areaPaths != null &&
    //                ShouldProcess(t, $"Set {string.Join(", ", areaPaths)} as team's area paths"))
    //            {
    //                var values = new List<TeamFieldValue>();

    //                foreach (var a in areaPaths)
    //                {
    //                    values.Add(new TeamFieldValue()
    //                    {
    //                        Value = NodeUtil.NormalizeNodePath(a.TrimEnd('\\', '*'), tp.Name, scope: "Areas", includeTeamProject: true),
    //                        IncludeChildren = a.EndsWith("*")
    //                    });
    //                }

    //                teamFieldPatch.Values = values;

    //                workClient.UpdateTeamFieldValuesAsync(teamFieldPatch, ctx)
    //                    .GetResult("Error applying team field value and/or area path settings");
    //            }

    //            // Set backlog and iteration path settings

    //            bool isDirty = false;
    //            var iterationPatch = new TeamSettingsPatch();

    //            if (backlogIteration != null &&
    //                ShouldProcess(t, $"Set the team's backlog iteration to '{backlogIteration}'"))
    //            {
    //                this.Log($"Setting backlog iteration to {backlogIteration}");

    //                var iteration = GetItem<Models.ClassificationNode>(new
    //                {
    //                    Node = backlogIteration,
    //                    StructureGroup = TreeStructureGroup.Iterations
    //                });

    //                iterationPatch.BacklogIteration =
    //                    iterationPatch.DefaultIteration =
    //                        iteration.Identifier;

    //                isDirty = true;
    //            }

    //            if (isDirty) workClient.UpdateTeamSettingsAsync(iterationPatch, ctx)
    //                 .GetResult("Error applying iteration and/or board settings");

    //            // TODO: Finish migration

    //            //         if (BacklogVisibilities && ShouldProcess(Team, $"Set the team"s backlog visibilities to {_DumpObj {BacklogVisibilities}}"))
    //            //         {
    //            //             this.Log($"Setting backlog iteration to {BacklogVisibilities}");
    //            //             patch.BacklogVisibilities = _NewDictionary @([string], [bool]) BacklogVisibilities

    //            //             isDirty = true
    //            //         }

    //            //         if (DefaultIterationMacro && ShouldProcess(Team, $"Set the team"s default iteration macro to {DefaultIterationMacro}"))
    //            //         {
    //            //             this.Log($"Setting default iteration macro to {DefaultIterationMacro}");
    //            //             patch.DefaultIterationMacro = DefaultIterationMacro

    //            //             isDirty = true
    //            //         }

    //            //         if (WorkingDays && ShouldProcess(Team, $"Set the team"s working days to {_DumpObj {WorkingDays}}"))
    //            //         {
    //            //             this.Log($"Setting working days to {{WorkingDays}|ConvertTo=-Json -Compress}");
    //            //             patch.WorkingDays = WorkingDays

    //            //             isDirty = true
    //            //         }

    //            //         if(BugsBehavior && ShouldProcess(Team, $"Set the team"s bugs behavior to {_DumpObj {BugsBehavior}}"))
    //            //         {
    //            //             this.Log($"Setting bugs behavior to {_DumpObj {BugsBehavior}}");
    //            //             patch.BugsBehavior = BugsBehavior

    //            //             isDirty = true
    //            //         }

    //            //         if(isDirty)
    //            //         {
    //            //             task = client.UpdateTeamSettingsAsync(patch, ctx)
    //            //             result = task.Result; if(task.IsFaulted) { _throw new Exception("Error applying iteration settings" task.Exception.InnerExceptions })
    //            //         }

    //            //         if(Passthru.IsPresent)
    //            //         {
    //            //             WriteObject(t); return;
    //            //         }
    //            //     }
    //            // }

    //            return GetItem<Models.Team>();
    //        }
    //    }
}