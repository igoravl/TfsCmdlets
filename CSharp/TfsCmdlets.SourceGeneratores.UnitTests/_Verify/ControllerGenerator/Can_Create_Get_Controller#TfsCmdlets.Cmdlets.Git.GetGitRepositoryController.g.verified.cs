//HintName: TfsCmdlets.Cmdlets.Git.GetGitRepositoryController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    internal partial class GetGitRepositoryController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }

        // Repository
        protected bool Has_Repository => Parameters.HasParameter(nameof(Repository));
        protected IEnumerable Repository
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Repository), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }

        // Default
        protected bool Has_Default { get; set; }
        protected bool Default { get; set; }

        // IncludeParent
        protected bool Has_IncludeParent { get; set; }
        protected bool IncludeParent { get; set; }

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
            // Default
            Has_Default = Parameters.HasParameter("Default");
            Default = Parameters.Get<bool>("Default");

            // IncludeParent
            Has_IncludeParent = Parameters.HasParameter("IncludeParent");
            IncludeParent = Parameters.Get<bool>("IncludeParent");

            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }

        [ImportingConstructor]
        public GetGitRepositoryController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger, TfsCmdlets.HttpClients.IGitHttpClient client)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}
