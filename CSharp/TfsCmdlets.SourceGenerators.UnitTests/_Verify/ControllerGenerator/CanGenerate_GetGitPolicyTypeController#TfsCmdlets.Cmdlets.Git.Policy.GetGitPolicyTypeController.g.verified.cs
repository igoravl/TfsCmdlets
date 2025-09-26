//HintName: TfsCmdlets.Cmdlets.Git.Policy.GetGitPolicyTypeController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
namespace TfsCmdlets.Cmdlets.Git.Policy
{
    internal partial class GetGitPolicyTypeController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IPolicyHttpClient Client { get; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.Policy.WebApi.PolicyType);
        protected override void CacheParameters()
        {
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGitPolicyTypeController(TfsCmdlets.HttpClients.IPolicyHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}