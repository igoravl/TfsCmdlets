//HintName: TfsCmdlets.Cmdlets.TeamProject.RemoveTeamProjectController.g.cs
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
namespace TfsCmdlets.Cmdlets.TeamProject
{
    internal partial class RemoveTeamProjectController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IProjectHttpClient Client { get; }
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
        // Hard
        protected bool Has_Hard { get; set; }
        protected bool Hard { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
            // Hard
            Has_Hard = Parameters.HasParameter("Hard");
            Hard = Parameters.Get<bool>("Hard");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveTeamProjectController(IOperationsHttpClient operationsClient, TfsCmdlets.HttpClients.IProjectHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            OperationsClient = operationsClient;
            Client = client;
        }
    }
}