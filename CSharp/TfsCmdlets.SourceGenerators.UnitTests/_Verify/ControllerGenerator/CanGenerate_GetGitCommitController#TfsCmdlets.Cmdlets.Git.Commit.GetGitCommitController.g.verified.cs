//HintName: TfsCmdlets.Cmdlets.Git.Commit.GetGitCommitController.g.cs
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using Parameter = TfsCmdlets.Models.Parameter;
namespace TfsCmdlets.Cmdlets.Git.Commit
{
    internal partial class GetGitCommitController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IGitHttpClient Client { get; }
        // Commit
        protected bool Has_Commit => Parameters.HasParameter(nameof(Commit));
        protected IEnumerable Commit
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Commit));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Tag
        protected bool Has_Tag { get; set; }
        protected string Tag { get; set; }
        // Branch
        protected bool Has_Branch { get; set; }
        protected string Branch { get; set; }
        // Author
        protected bool Has_Author { get; set; }
        protected string Author { get; set; }
        // Committer
        protected bool Has_Committer { get; set; }
        protected string Committer { get; set; }
        // CompareVersion
        protected bool Has_CompareVersion { get; set; }
        protected Microsoft.TeamFoundation.SourceControl.WebApi.GitVersionDescriptor CompareVersion { get; set; }
        // FromCommit
        protected bool Has_FromCommit { get; set; }
        protected string FromCommit { get; set; }
        // FromDate
        protected bool Has_FromDate { get; set; }
        protected System.DateTime FromDate { get; set; }
        // ItemPath
        protected bool Has_ItemPath { get; set; }
        protected string ItemPath { get; set; }
        // ToCommit
        protected bool Has_ToCommit { get; set; }
        protected string ToCommit { get; set; }
        // ToDate
        protected bool Has_ToDate { get; set; }
        protected System.DateTime ToDate { get; set; }
        // ShowOldestCommitsFirst
        protected bool Has_ShowOldestCommitsFirst { get; set; }
        protected bool ShowOldestCommitsFirst { get; set; }
        // Skip
        protected bool Has_Skip { get; set; }
        protected int Skip { get; set; }
        // Top
        protected bool Has_Top { get; set; }
        protected int Top { get; set; }
        // ExcludeDeletes
        protected bool Has_ExcludeDeletes { get; set; }
        protected bool ExcludeDeletes { get; set; }
        // IncludeLinks
        protected bool Has_IncludeLinks { get; set; }
        protected bool IncludeLinks { get; set; }
        // IncludeWorkItems
        protected bool Has_IncludeWorkItems { get; set; }
        protected bool IncludeWorkItems { get; set; }
        // IncludePushData
        protected bool Has_IncludePushData { get; set; }
        protected bool IncludePushData { get; set; }
        // IncludeUserImageUrl
        protected bool Has_IncludeUserImageUrl { get; set; }
        protected bool IncludeUserImageUrl { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef);
        protected override void CacheParameters()
        {
            // Tag
            Has_Tag = Parameters.HasParameter("Tag");
            Tag = Parameters.Get<string>("Tag");
            // Branch
            Has_Branch = Parameters.HasParameter("Branch");
            Branch = Parameters.Get<string>("Branch");
            // Author
            Has_Author = Parameters.HasParameter("Author");
            Author = Parameters.Get<string>("Author");
            // Committer
            Has_Committer = Parameters.HasParameter("Committer");
            Committer = Parameters.Get<string>("Committer");
            // CompareVersion
            Has_CompareVersion = Parameters.HasParameter("CompareVersion");
            CompareVersion = Parameters.Get<Microsoft.TeamFoundation.SourceControl.WebApi.GitVersionDescriptor>("CompareVersion");
            // FromCommit
            Has_FromCommit = Parameters.HasParameter("FromCommit");
            FromCommit = Parameters.Get<string>("FromCommit");
            // FromDate
            Has_FromDate = Parameters.HasParameter("FromDate");
            FromDate = Parameters.Get<System.DateTime>("FromDate");
            // ItemPath
            Has_ItemPath = Parameters.HasParameter("ItemPath");
            ItemPath = Parameters.Get<string>("ItemPath");
            // ToCommit
            Has_ToCommit = Parameters.HasParameter("ToCommit");
            ToCommit = Parameters.Get<string>("ToCommit");
            // ToDate
            Has_ToDate = Parameters.HasParameter("ToDate");
            ToDate = Parameters.Get<System.DateTime>("ToDate");
            // ShowOldestCommitsFirst
            Has_ShowOldestCommitsFirst = Parameters.HasParameter("ShowOldestCommitsFirst");
            ShowOldestCommitsFirst = Parameters.Get<bool>("ShowOldestCommitsFirst");
            // Skip
            Has_Skip = Parameters.HasParameter("Skip");
            Skip = Parameters.Get<int>("Skip");
            // Top
            Has_Top = Parameters.HasParameter("Top");
            Top = Parameters.Get<int>("Top");
            // ExcludeDeletes
            Has_ExcludeDeletes = Parameters.HasParameter("ExcludeDeletes");
            ExcludeDeletes = Parameters.Get<bool>("ExcludeDeletes");
            // IncludeLinks
            Has_IncludeLinks = Parameters.HasParameter("IncludeLinks");
            IncludeLinks = Parameters.Get<bool>("IncludeLinks");
            // IncludeWorkItems
            Has_IncludeWorkItems = Parameters.HasParameter("IncludeWorkItems");
            IncludeWorkItems = Parameters.Get<bool>("IncludeWorkItems");
            // IncludePushData
            Has_IncludePushData = Parameters.HasParameter("IncludePushData");
            IncludePushData = Parameters.Get<bool>("IncludePushData");
            // IncludeUserImageUrl
            Has_IncludeUserImageUrl = Parameters.HasParameter("IncludeUserImageUrl");
            IncludeUserImageUrl = Parameters.Get<bool>("IncludeUserImageUrl");
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<object>("Repository");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGitCommitController(TfsCmdlets.HttpClients.IGitHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}