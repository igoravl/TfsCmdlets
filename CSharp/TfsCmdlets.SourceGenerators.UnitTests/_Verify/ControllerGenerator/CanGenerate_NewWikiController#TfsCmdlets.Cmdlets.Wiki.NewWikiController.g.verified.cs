//HintName: TfsCmdlets.Cmdlets.Wiki.NewWikiController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
namespace TfsCmdlets.Cmdlets.Wiki
{
    internal partial class NewWikiController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWikiHttpClient Client { get; }
        // Wiki
        protected bool Has_Wiki { get; set; }
        protected string Wiki { get; set; }
        // Repository
        protected bool Has_Repository { get; set; }
        protected object Repository { get; set; }
        // Branch
        protected bool Has_Branch { get; set; }
        protected string Branch { get; set; }
        // Path
        protected bool Has_Path { get; set; }
        protected string Path { get; set; }
        // ProjectWiki
        protected bool Has_ProjectWiki { get; set; }
        protected bool ProjectWiki { get; set; }
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
        public override Type DataType => typeof(Microsoft.TeamFoundation.Wiki.WebApi.WikiV2);
        protected override void CacheParameters()
        {
            // Wiki
            Has_Wiki = Parameters.HasParameter("Wiki");
            Wiki = Parameters.Get<string>("Wiki");
            // Repository
            Has_Repository = Parameters.HasParameter("Repository");
            Repository = Parameters.Get<object>("Repository");
            // Branch
            Has_Branch = Parameters.HasParameter("Branch");
            Branch = Parameters.Get<string>("Branch");
            // Path
            Has_Path = Parameters.HasParameter("Path");
            Path = Parameters.Get<string>("Path", "/");
            // ProjectWiki
            Has_ProjectWiki = Parameters.HasParameter("ProjectWiki");
            ProjectWiki = Parameters.Get<bool>("ProjectWiki");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewWikiController(TfsCmdlets.HttpClients.IWikiHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}