//HintName: TfsCmdlets.Cmdlets.Wiki.RemoveWikiController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
namespace TfsCmdlets.Cmdlets.Wiki
{
    internal partial class RemoveWikiController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IWikiHttpClient Client { get; }
        // Wiki
        protected bool Has_Wiki { get; set; }
        protected object Wiki { get; set; }
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
        // Items
        protected IEnumerable<Microsoft.TeamFoundation.Wiki.WebApi.WikiV2> Items => Wiki switch {
            Microsoft.TeamFoundation.Wiki.WebApi.WikiV2 item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.Wiki.WebApi.WikiV2> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.Wiki.WebApi.WikiV2>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Wiki.WebApi.WikiV2);
        protected override void CacheParameters()
        {
            // Wiki
            Has_Wiki = Parameters.HasParameter("Wiki");
            Wiki = Parameters.Get<object>("Wiki");
            // ProjectWiki
            Has_ProjectWiki = Parameters.HasParameter("ProjectWiki");
            ProjectWiki = Parameters.Get<bool>("ProjectWiki");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveWikiController(TfsCmdlets.HttpClients.IWikiHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}