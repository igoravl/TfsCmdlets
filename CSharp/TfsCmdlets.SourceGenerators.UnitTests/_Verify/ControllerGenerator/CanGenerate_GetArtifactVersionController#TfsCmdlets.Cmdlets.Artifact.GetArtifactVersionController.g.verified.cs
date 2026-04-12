//HintName: TfsCmdlets.Cmdlets.Artifact.GetArtifactVersionController.g.cs
using Microsoft.VisualStudio.Services.Feed.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
namespace TfsCmdlets.Cmdlets.Artifact
{
    internal partial class GetArtifactVersionController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IFeedHttpClient Client { get; }
        // Version
        protected bool Has_Version => Parameters.HasParameter(nameof(Version));
        protected IEnumerable Version
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Version), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Artifact
        protected bool Has_Artifact { get; set; }
        protected object Artifact { get; set; }
        // Feed
        protected bool Has_Feed { get; set; }
        protected object Feed { get; set; }
        // IncludeDeleted
        protected bool Has_IncludeDeleted { get; set; }
        protected bool IncludeDeleted { get; set; }
        // IncludeDelisted
        protected bool Has_IncludeDelisted { get; set; }
        protected bool IncludeDelisted { get; set; }
        // ProtocolType
        protected bool Has_ProtocolType { get; set; }
        protected string ProtocolType { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Feed.WebApi.PackageVersion);
        protected override void CacheParameters()
        {
            // Artifact
            Has_Artifact = Parameters.HasParameter("Artifact");
            Artifact = Parameters.Get<object>("Artifact");
            // Feed
            Has_Feed = Parameters.HasParameter("Feed");
            Feed = Parameters.Get<object>("Feed");
            // IncludeDeleted
            Has_IncludeDeleted = Parameters.HasParameter("IncludeDeleted");
            IncludeDeleted = Parameters.Get<bool>("IncludeDeleted");
            // IncludeDelisted
            Has_IncludeDelisted = Parameters.HasParameter("IncludeDelisted");
            IncludeDelisted = Parameters.Get<bool>("IncludeDelisted");
            // ProtocolType
            Has_ProtocolType = Parameters.HasParameter("ProtocolType");
            ProtocolType = Parameters.Get<string>("ProtocolType");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetArtifactVersionController(TfsCmdlets.HttpClients.IFeedHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}