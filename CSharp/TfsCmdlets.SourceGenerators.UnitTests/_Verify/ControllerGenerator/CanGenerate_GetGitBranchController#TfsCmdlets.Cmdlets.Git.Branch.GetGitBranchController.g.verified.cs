//HintName: TfsCmdlets.Cmdlets.Git.Branch.GetGitBranchController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
namespace TfsCmdlets.Cmdlets.Git.Branch
{
    internal partial class GetGitBranchController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }
        // Branch
        protected bool Has_Branch => Parameters.HasParameter(nameof(Branch));
        protected IEnumerable Branch
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Branch), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Repository
        protected bool Has_Repository { get; set; }
        protected object Repository { get; set; }
        // Default
        protected bool Has_Default { get; set; }
        protected bool Default { get; set; }
        // Compare
        protected bool Has_Compare { get; set; }
        protected bool Compare { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats);
        protected override void CacheParameters()
        {
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<object>("Repository");
            // Default
            Has_Default = Parameters.HasParameter("Default");
            Default = Parameters.Get<bool>("Default");
            // Compare
            Has_Compare = Parameters.HasParameter("Compare");
            Compare = Parameters.Get<bool>("Compare");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGitBranchController(TfsCmdlets.HttpClients.IGitHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}