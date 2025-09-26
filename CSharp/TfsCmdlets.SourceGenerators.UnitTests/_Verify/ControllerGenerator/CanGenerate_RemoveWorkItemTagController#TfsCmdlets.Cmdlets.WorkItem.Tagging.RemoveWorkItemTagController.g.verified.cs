//HintName: TfsCmdlets.Cmdlets.WorkItem.Tagging.RemoveWorkItemTagController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.Tagging;
namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    internal partial class RemoveWorkItemTagController: ControllerBase
    {
        private TfsCmdlets.HttpClients.ITaggingHttpClient Client { get; }
        // Tag
        protected bool Has_Tag { get; set; }
        protected object Tag { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> Items => Tag switch {
            Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Core.WebApi.WebApiTagDefinition);
        protected override void CacheParameters()
        {
            // Tag
            Has_Tag = Parameters.HasParameter("Tag");
            Tag = Parameters.Get<object>("Tag");
            // Force
            Has_Force = Parameters.HasParameter("Force");
            Force = Parameters.Get<bool>("Force");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveWorkItemTagController(TfsCmdlets.HttpClients.ITaggingHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}