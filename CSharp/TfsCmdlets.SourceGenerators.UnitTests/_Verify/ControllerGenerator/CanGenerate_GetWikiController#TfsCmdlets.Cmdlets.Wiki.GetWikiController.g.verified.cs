//HintName: TfsCmdlets.Cmdlets.Wiki.GetWikiController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;
namespace TfsCmdlets.Cmdlets.Wiki
{
    internal partial class GetWikiController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWikiHttpClient Client { get; }
        // Wiki
        protected bool Has_Wiki => Parameters.HasParameter(nameof(Wiki));
        protected IEnumerable Wiki
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Wiki), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // ProjectWiki
        protected bool Has_ProjectWiki { get; set; }
        protected bool ProjectWiki { get; set; }
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
            // ProjectWiki
            Has_ProjectWiki = Parameters.HasParameter("ProjectWiki");
            ProjectWiki = Parameters.Get<bool>("ProjectWiki");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetWikiController(TfsCmdlets.HttpClients.IWikiHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}