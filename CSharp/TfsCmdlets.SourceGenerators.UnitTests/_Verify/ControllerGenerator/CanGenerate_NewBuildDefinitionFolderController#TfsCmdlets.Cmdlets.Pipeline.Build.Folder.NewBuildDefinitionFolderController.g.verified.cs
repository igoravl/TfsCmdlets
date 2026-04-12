//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Folder.NewBuildDefinitionFolderController.g.cs
using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    internal partial class NewBuildDefinitionFolderController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IBuildHttpClient Client { get; }
        // Folder
        protected bool Has_Folder { get; set; }
        protected object Folder { get; set; }
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
        // Items
        protected IEnumerable<Microsoft.TeamFoundation.Build.WebApi.Folder> Items => Folder switch {
            Microsoft.TeamFoundation.Build.WebApi.Folder item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.Build.WebApi.Folder> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.Build.WebApi.Folder>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Build.WebApi.Folder);
        protected override void CacheParameters()
        {
            // Folder
            Has_Folder = Parameters.HasParameter("Folder");
            Folder = Parameters.Get<object>("Folder");
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
        public NewBuildDefinitionFolderController(TfsCmdlets.HttpClients.IBuildHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}