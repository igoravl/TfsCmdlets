//HintName: TfsCmdlets.Cmdlets.TeamProject.RenameTeamProjectController.g.cs
using System.Management.Automation;
using System.Threading;
using Microsoft.VisualStudio.Services.Operations;
namespace TfsCmdlets.Cmdlets.TeamProject
{
    internal partial class RenameTeamProjectController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IOperationsHttpClient Client { get; }
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
        // NewName
        protected bool Has_NewName { get; set; }
        protected string NewName { get; set; }
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
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // NewName
            Has_NewName = Parameters.HasParameter("NewName");
            NewName = Parameters.Get<string>("NewName");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RenameTeamProjectController(IRestApiService restApiService, TfsCmdlets.HttpClients.IOperationsHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApiService = restApiService;
            Client = client;
        }
    }
}