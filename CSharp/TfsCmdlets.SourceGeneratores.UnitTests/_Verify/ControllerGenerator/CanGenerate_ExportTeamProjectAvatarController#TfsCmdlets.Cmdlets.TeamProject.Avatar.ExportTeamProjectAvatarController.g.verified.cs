//HintName: TfsCmdlets.Cmdlets.TeamProject.Avatar.ExportTeamProjectAvatarController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    internal partial class ExportTeamProjectAvatarController: ControllerBase
    {
        // Path
        protected bool Has_Path { get; set; }
        protected string Path { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
        protected override void CacheParameters()
        {
            // Path
            Has_Path = Parameters.HasParameter("Path");
            Path = Parameters.Get<string>("Path");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public ExportTeamProjectAvatarController(IRestApiService restApiService, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApiService = restApiService;
        }
    }
}