//HintName: TfsCmdlets.Cmdlets.Artifact.GetArtifactFeedController.g.cs
using Microsoft.VisualStudio.Services.Feed.WebApi;
namespace TfsCmdlets.Cmdlets.Artifact
{
    internal partial class GetArtifactFeedController: ControllerBase
    {
        // Feed
        protected bool Has_Feed => Parameters.HasParameter(nameof(Feed));
        protected IEnumerable Feed
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Feed), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Scope
        protected bool Has_Scope { get; set; }
        protected TfsCmdlets.ProjectOrCollectionScope Scope { get; set; }
        // Role
        protected bool Has_Role { get; set; }
        protected Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole Role { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Feed.WebApi.Feed);
        protected override void CacheParameters()
        {
            // Scope
            Has_Scope = Parameters.HasParameter("Scope");
            Scope = Parameters.Get<TfsCmdlets.ProjectOrCollectionScope>("Scope", ProjectOrCollectionScope.All);
            // Role
            Has_Role = Parameters.HasParameter("Role");
            Role = Parameters.Get<Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole>("Role", Microsoft.VisualStudio.Services.Feed.WebApi.FeedRole.Reader);
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetArtifactFeedController(IFeedHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}