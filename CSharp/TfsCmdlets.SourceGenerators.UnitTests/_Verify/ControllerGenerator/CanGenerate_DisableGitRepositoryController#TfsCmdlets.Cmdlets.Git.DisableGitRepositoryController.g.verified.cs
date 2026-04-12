//HintName: TfsCmdlets.Cmdlets.Git.DisableGitRepositoryController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using TfsCmdlets.HttpClients;
namespace TfsCmdlets.Cmdlets.Git
{
    internal partial class DisableGitRepositoryController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitExtendedHttpClient Client { get; }
        // Repository
        protected bool Has_Repository { get; set; }
        protected object Repository { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository> Items => Repository switch {
            Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository);
        protected override void CacheParameters()
        {
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<object>("Repository");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public DisableGitRepositoryController(TfsCmdlets.HttpClients.IGitExtendedHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}