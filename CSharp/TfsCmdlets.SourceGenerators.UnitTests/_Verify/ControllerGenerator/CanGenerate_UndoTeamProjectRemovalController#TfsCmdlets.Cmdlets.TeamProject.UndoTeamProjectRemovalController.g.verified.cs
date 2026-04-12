//HintName: TfsCmdlets.Cmdlets.TeamProject.UndoTeamProjectRemovalController.g.cs
using System.Management.Automation;
namespace TfsCmdlets.Cmdlets.TeamProject
{
    internal partial class UndoTeamProjectRemovalController: ControllerBase
    {
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.Core.WebApi.TeamProject> Items => Project switch {
            Microsoft.TeamFoundation.Core.WebApi.TeamProject item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.Core.WebApi.TeamProject> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.Core.WebApi.TeamProject>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject);
        protected override void CacheParameters()
        {
            // Project
            Has_Project = Parameters.HasParameter("Project");
            Project = Parameters.Get<object>("Project");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public UndoTeamProjectRemovalController(IRestApiService restApiService, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApiService = restApiService;
        }
    }
}