//HintName: TfsCmdlets.Cmdlets.Artifact.GetArtifactController.g.cs
using Microsoft.VisualStudio.Services.Feed.WebApi;
namespace TfsCmdlets.Cmdlets.Artifact
{
    internal partial class GetArtifactController: ControllerBase
    {
        // Artifact
        protected bool Has_Artifact => Parameters.HasParameter(nameof(Artifact));
        protected IEnumerable Artifact
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Artifact), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Feed
        protected bool Has_Feed { get; set; }
        protected object Feed { get; set; }
        // IncludeDeleted
        protected bool Has_IncludeDeleted { get; set; }
        protected bool IncludeDeleted { get; set; }
        // IncludeDescription
        protected bool Has_IncludeDescription { get; set; }
        protected bool IncludeDescription { get; set; }
        // IncludePrerelease
        protected bool Has_IncludePrerelease { get; set; }
        protected bool IncludePrerelease { get; set; }
        // IncludeDelisted
        protected bool Has_IncludeDelisted { get; set; }
        protected bool IncludeDelisted { get; set; }
        // ProtocolType
        protected bool Has_ProtocolType { get; set; }
        protected string ProtocolType { get; set; }
        // IncludeAllVersions
        protected bool Has_IncludeAllVersions { get; set; }
        protected bool IncludeAllVersions { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.Feed.WebApi.Package);
        protected override void CacheParameters()
        {
            // Feed
            Has_Feed = Parameters.HasParameter("Feed");
            Feed = Parameters.Get<object>("Feed");
            // IncludeDeleted
            Has_IncludeDeleted = Parameters.HasParameter("IncludeDeleted");
            IncludeDeleted = Parameters.Get<bool>("IncludeDeleted");
            // IncludeDescription
            Has_IncludeDescription = Parameters.HasParameter("IncludeDescription");
            IncludeDescription = Parameters.Get<bool>("IncludeDescription");
            // IncludePrerelease
            Has_IncludePrerelease = Parameters.HasParameter("IncludePrerelease");
            IncludePrerelease = Parameters.Get<bool>("IncludePrerelease");
            // IncludeDelisted
            Has_IncludeDelisted = Parameters.HasParameter("IncludeDelisted");
            IncludeDelisted = Parameters.Get<bool>("IncludeDelisted");
            // ProtocolType
            Has_ProtocolType = Parameters.HasParameter("ProtocolType");
            ProtocolType = Parameters.Get<string>("ProtocolType");
            // IncludeAllVersions
            Has_IncludeAllVersions = Parameters.HasParameter("IncludeAllVersions");
            IncludeAllVersions = Parameters.Get<bool>("IncludeAllVersions");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetArtifactController(IFeedHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}