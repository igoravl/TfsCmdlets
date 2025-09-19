//HintName: TfsCmdlets.Cmdlets.Git.RemoveGitRepositoryController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    internal partial class RemoveGitRepositoryController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }

        // Repository
        protected bool Has_Repository { get; set; }
        protected object Repository { get; set; }

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

            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");

            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }

        [ImportingConstructor]
        public RemoveGitRepositoryController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger, TfsCmdlets.HttpClients.IGitHttpClient client)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}
