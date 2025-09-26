//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.NewReleaseDefinitionFolderController.g.cs
using System.Management.Automation;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    internal partial class NewReleaseDefinitionFolderController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IReleaseHttpClient Client { get; }
        // Folder
        protected bool Has_Folder { get; set; }
        protected string Folder { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
            // Folder
            Has_Folder = Parameters.HasParameter("Folder");
            Folder = Parameters.Get<string>("Folder", "**");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewReleaseDefinitionFolderController(INodeUtil nodeUtil, TfsCmdlets.HttpClients.IReleaseHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            NodeUtil = nodeUtil;
            Client = client;
        }
    }
}