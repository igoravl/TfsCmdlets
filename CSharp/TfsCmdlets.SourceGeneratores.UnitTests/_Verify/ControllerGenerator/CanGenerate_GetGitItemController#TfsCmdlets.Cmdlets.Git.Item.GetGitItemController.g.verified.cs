//HintName: TfsCmdlets.Cmdlets.Git.Item.GetGitItemController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
namespace TfsCmdlets.Cmdlets.Git.Item
{
    internal partial class GetGitItemController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }
        // Item
        protected bool Has_Item => Parameters.HasParameter(nameof(Item));
        protected IEnumerable Item
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Item), "/*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Commit
        protected bool Has_Commit { get; set; }
        protected string Commit { get; set; }
        // Tag
        protected bool Has_Tag { get; set; }
        protected string Tag { get; set; }
        // Branch
        protected bool Has_Branch { get; set; }
        protected string Branch { get; set; }
        // IncludeContent
        protected bool Has_IncludeContent { get; set; }
        protected bool IncludeContent { get; set; }
        // IncludeMetadata
        protected bool Has_IncludeMetadata { get; set; }
        protected bool IncludeMetadata { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitItem);
        protected override void CacheParameters()
        {
            // Commit
            Has_Commit = Parameters.HasParameter("Commit");
            Commit = Parameters.Get<string>("Commit");
            // Tag
            Has_Tag = Parameters.HasParameter("Tag");
            Tag = Parameters.Get<string>("Tag");
            // Branch
            Has_Branch = Parameters.HasParameter("Branch");
            Branch = Parameters.Get<string>("Branch");
            // IncludeContent
            Has_IncludeContent = Parameters.HasParameter("IncludeContent");
            IncludeContent = Parameters.Get<bool>("IncludeContent");
            // IncludeMetadata
            Has_IncludeMetadata = Parameters.HasParameter("IncludeMetadata");
            IncludeMetadata = Parameters.Get<bool>("IncludeMetadata");
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<object>("Repository");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGitItemController(INodeUtil nodeUtil, TfsCmdlets.HttpClients.IGitHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}