//HintName: TfsCmdlets.Cmdlets.Team.NewTeamController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.Cmdlets.Team
{
    internal partial class NewTeamController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITeamHttpClient Client { get; }
        // Team
        protected bool Has_Team { get; set; }
        protected string Team { get; set; }
        // DefaultAreaPath
        protected bool Has_DefaultAreaPath { get; set; }
        protected string DefaultAreaPath { get; set; }
        // NoDefaultArea
        protected bool Has_NoDefaultArea { get; set; }
        protected bool NoDefaultArea { get; set; }
        // AreaPaths
        protected bool Has_AreaPaths { get; set; }
        protected string[] AreaPaths { get; set; }
        // BacklogIteration
        protected bool Has_BacklogIteration { get; set; }
        protected string BacklogIteration { get; set; }
        // DefaultIterationMacro
        protected bool Has_DefaultIterationMacro { get; set; }
        protected string DefaultIterationMacro { get; set; }
        // IterationPaths
        protected bool Has_IterationPaths { get; set; }
        protected string[] IterationPaths { get; set; }
        // NoBacklogIteration
        protected bool Has_NoBacklogIteration { get; set; }
        protected bool NoBacklogIteration { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
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
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Team);
        protected override void CacheParameters()
        {
            // Team
            Has_Team = Parameters.HasParameter("Team");
            Team = Parameters.Get<string>("Team");
            // DefaultAreaPath
            Has_DefaultAreaPath = Parameters.HasParameter("DefaultAreaPath");
            DefaultAreaPath = Parameters.Get<string>("DefaultAreaPath");
            // NoDefaultArea
            Has_NoDefaultArea = Parameters.HasParameter("NoDefaultArea");
            NoDefaultArea = Parameters.Get<bool>("NoDefaultArea");
            // AreaPaths
            Has_AreaPaths = Parameters.HasParameter("AreaPaths");
            AreaPaths = Parameters.Get<string[]>("AreaPaths");
            // BacklogIteration
            Has_BacklogIteration = Parameters.HasParameter("BacklogIteration");
            BacklogIteration = Parameters.Get<string>("BacklogIteration", "\\");
            // DefaultIterationMacro
            Has_DefaultIterationMacro = Parameters.HasParameter("DefaultIterationMacro");
            DefaultIterationMacro = Parameters.Get<string>("DefaultIterationMacro", "@CurrentIteration");
            // IterationPaths
            Has_IterationPaths = Parameters.HasParameter("IterationPaths");
            IterationPaths = Parameters.Get<string[]>("IterationPaths");
            // NoBacklogIteration
            Has_NoBacklogIteration = Parameters.HasParameter("NoBacklogIteration");
            NoBacklogIteration = Parameters.Get<bool>("NoBacklogIteration");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewTeamController(TfsCmdlets.HttpClients.ITeamHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}