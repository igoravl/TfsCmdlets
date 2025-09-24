//HintName: TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement.RemoveReleaseDefinitionFolderController.g.cs
using System.Management.Automation;
using WebApiFolder = Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    internal partial class RemoveReleaseDefinitionFolderController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IReleaseHttpClient Client { get; }
        // Folder
        protected bool Has_Folder { get; set; }
        protected object Folder { get; set; }
        // Recurse
        protected bool Has_Recurse { get; set; }
        protected bool Recurse { get; set; }
        // Force
        protected bool Has_Force { get; set; }
        protected bool Force { get; set; }
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
        // Items
        protected IEnumerable<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder> Items => Folder switch {
            Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder item => new[] { item },
            IEnumerable<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder> items => items,
            _ => Data.GetItems<Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder);
        protected override void CacheParameters()
        {
            // Folder
            Has_Folder = Parameters.HasParameter("Folder");
            Folder = Parameters.Get<object>("Folder");
            // Recurse
            Has_Recurse = Parameters.HasParameter("Recurse");
            Recurse = Parameters.Get<bool>("Recurse");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveReleaseDefinitionFolderController(TfsCmdlets.HttpClients.IReleaseHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}