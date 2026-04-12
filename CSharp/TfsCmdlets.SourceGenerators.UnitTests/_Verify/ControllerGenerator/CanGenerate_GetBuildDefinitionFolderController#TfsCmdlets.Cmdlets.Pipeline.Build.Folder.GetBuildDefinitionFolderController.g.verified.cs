//HintName: TfsCmdlets.Cmdlets.Pipeline.Build.Folder.GetBuildDefinitionFolderController.g.cs
using System.Management.Automation;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;
using Microsoft.TeamFoundation.Build.WebApi;
namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    internal partial class GetBuildDefinitionFolderController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IBuildHttpClient Client { get; }
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
        protected Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder QueryOrder { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.Build.WebApi.Folder);
        protected override void CacheParameters()
        {
            // QueryOrder
            Has_QueryOrder = Parameters.HasParameter("QueryOrder");
            QueryOrder = Parameters.Get<Microsoft.TeamFoundation.Build.WebApi.FolderQueryOrder>("QueryOrder");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetBuildDefinitionFolderController(TfsCmdlets.HttpClients.IBuildHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}