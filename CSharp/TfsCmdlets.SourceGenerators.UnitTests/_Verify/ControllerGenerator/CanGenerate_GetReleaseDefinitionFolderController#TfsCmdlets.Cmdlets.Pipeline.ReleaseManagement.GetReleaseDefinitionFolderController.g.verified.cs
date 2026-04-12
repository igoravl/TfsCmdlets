//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.GetReleaseDefinitionFolderController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    internal partial class GetReleaseDefinitionFolderController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IReleaseHttpClient Client { get; }
        // Folder
        protected bool Has_Folder => Parameters.HasParameter(nameof(Folder));
        protected IEnumerable Folder
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Folder), "**");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // QueryOrder
        protected bool Has_QueryOrder { get; set; }
        protected Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.FolderPathQueryOrder QueryOrder { get; set; }
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
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder);
        protected override void CacheParameters()
        {
            // QueryOrder
            Has_QueryOrder = Parameters.HasParameter("QueryOrder");
            QueryOrder = Parameters.Get<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.FolderPathQueryOrder>("QueryOrder");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetReleaseDefinitionFolderController(INodeUtil nodeUtil, TfsCmdlets.HttpClients.IReleaseHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}