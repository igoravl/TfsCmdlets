//HintName: TfsCmdlets.Cmdlets.Team.SetTeamController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.Team
{
    internal partial class SetTeamController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITeamHttpClient Client { get; }
        // Team
        protected bool Has_Team { get; set; }
        protected object Team { get; set; }
        // Default
        protected bool Has_Default { get; set; }
        protected bool Default { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // DefaultAreaPath
        protected bool Has_DefaultAreaPath { get; set; }
        protected string DefaultAreaPath { get; set; }
        // AreaPaths
        protected bool Has_AreaPaths { get; set; }
        protected string[] AreaPaths { get; set; }
        // OverwriteAreaPaths
        protected bool Has_OverwriteAreaPaths { get; set; }
        protected bool OverwriteAreaPaths { get; set; }
        // BacklogIteration
        protected bool Has_BacklogIteration { get; set; }
        protected string BacklogIteration { get; set; }
        // DefaultIterationMacro
        protected bool Has_DefaultIterationMacro { get; set; }
        protected string DefaultIterationMacro { get; set; }
        // IterationPaths
        protected bool Has_IterationPaths { get; set; }
        protected string[] IterationPaths { get; set; }
        // OverwriteIterationPaths
        protected bool Has_OverwriteIterationPaths { get; set; }
        protected bool OverwriteIterationPaths { get; set; }
        // WorkingDays
        protected bool Has_WorkingDays { get; set; }
        protected System.DayOfWeek[] WorkingDays { get; set; }
        // BugsBehavior
        protected bool Has_BugsBehavior { get; set; }
        protected Microsoft.TeamFoundation.Work.WebApi.BugsBehavior BugsBehavior { get; set; }
        // BacklogVisibilities
        protected bool Has_BacklogVisibilities { get; set; }
        protected System.Collections.Hashtable BacklogVisibilities { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
        // Project
        protected bool Has_Project => Parameters.HasParameter("Project");
        protected WebApiTeamProject Project => Data.GetProject();
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // Items
        protected IEnumerable<TfsCmdlets.Models.Team> Items => Team switch {
            TfsCmdlets.Models.Team item => new[] { item },
            IEnumerable<TfsCmdlets.Models.Team> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.Team>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Team);
        protected override void CacheParameters()
        {
            // Team
            Has_Team = Parameters.HasParameter("Team");
            Team = Parameters.Get<object>("Team");
            // Default
            Has_Default = Parameters.HasParameter("Default");
            Default = Parameters.Get<bool>("Default");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // DefaultAreaPath
            Has_DefaultAreaPath = Parameters.HasParameter("DefaultAreaPath");
            DefaultAreaPath = Parameters.Get<string>("DefaultAreaPath");
            // AreaPaths
            Has_AreaPaths = Parameters.HasParameter("AreaPaths");
            AreaPaths = Parameters.Get<string[]>("AreaPaths");
            // OverwriteAreaPaths
            Has_OverwriteAreaPaths = Parameters.HasParameter("OverwriteAreaPaths");
            OverwriteAreaPaths = Parameters.Get<bool>("OverwriteAreaPaths");
            // BacklogIteration
            Has_BacklogIteration = Parameters.HasParameter("BacklogIteration");
            BacklogIteration = Parameters.Get<string>("BacklogIteration", "\\");
            // DefaultIterationMacro
            Has_DefaultIterationMacro = Parameters.HasParameter("DefaultIterationMacro");
            DefaultIterationMacro = Parameters.Get<string>("DefaultIterationMacro");
            // IterationPaths
            Has_IterationPaths = Parameters.HasParameter("IterationPaths");
            IterationPaths = Parameters.Get<string[]>("IterationPaths");
            // OverwriteIterationPaths
            Has_OverwriteIterationPaths = Parameters.HasParameter("OverwriteIterationPaths");
            OverwriteIterationPaths = Parameters.Get<bool>("OverwriteIterationPaths");
            // WorkingDays
            Has_WorkingDays = Parameters.HasParameter("WorkingDays");
            WorkingDays = Parameters.Get<System.DayOfWeek[]>("WorkingDays");
            // BugsBehavior
            Has_BugsBehavior = Parameters.HasParameter("BugsBehavior");
            BugsBehavior = Parameters.Get<Microsoft.TeamFoundation.Work.WebApi.BugsBehavior>("BugsBehavior");
            // BacklogVisibilities
            Has_BacklogVisibilities = Parameters.HasParameter("BacklogVisibilities");
            BacklogVisibilities = Parameters.Get<System.Collections.Hashtable>("BacklogVisibilities");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public SetTeamController(IRestApiService restApiService, INodeUtil nodeUtil, IWorkHttpClient workClient, TfsCmdlets.HttpClients.ITeamHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApiService = restApiService;
            NodeUtil = nodeUtil;
            WorkClient = workClient;
            Client = client;
        }
    }
}