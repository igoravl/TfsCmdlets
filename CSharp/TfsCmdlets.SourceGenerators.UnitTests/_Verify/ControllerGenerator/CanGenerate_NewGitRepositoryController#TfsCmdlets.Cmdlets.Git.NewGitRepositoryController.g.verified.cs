//HintName: TfsCmdlets.Cmdlets.Git.NewGitRepositoryController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.Cmdlets.Git
{
    internal partial class NewGitRepositoryController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }
        // Repository
        protected bool Has_Repository { get; set; }
        protected string Repository { get; set; }
        // ForkFrom
        protected bool Has_ForkFrom { get; set; }
        protected object ForkFrom { get; set; }
        // SourceBranch
        protected bool Has_SourceBranch { get; set; }
        protected string SourceBranch { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository);
        protected override void CacheParameters()
        {
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<string>("Repository");
            // ForkFrom
            Has_ForkFrom = Parameters.HasParameter("ForkFrom");
            ForkFrom = Parameters.Get<object>("ForkFrom");
            // SourceBranch
            Has_SourceBranch = Parameters.HasParameter("SourceBranch");
            SourceBranch = Parameters.Get<string>("SourceBranch");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewGitRepositoryController(TfsCmdlets.HttpClients.IGitHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}