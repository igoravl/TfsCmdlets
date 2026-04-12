//HintName: TfsCmdlets.Cmdlets.Git.Policy.GetGitBranchPolicyController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
namespace TfsCmdlets.Cmdlets.Git.Policy
{
    internal partial class GetGitBranchPolicyController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }
        // PolicyType
        protected bool Has_PolicyType => Parameters.HasParameter(nameof(PolicyType));
        protected IEnumerable PolicyType
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(PolicyType), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Branch
        protected bool Has_Branch { get; set; }
        protected object Branch { get; set; }
        // Repository
        protected bool Has_Repository { get; set; }
        protected object Repository { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration);
        protected override void CacheParameters()
        {
            // Branch
            Has_Branch = Parameters.HasParameter("Branch");
            Branch = Parameters.Get<object>("Branch");
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<object>("Repository");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGitBranchPolicyController(TfsCmdlets.HttpClients.IGitHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}