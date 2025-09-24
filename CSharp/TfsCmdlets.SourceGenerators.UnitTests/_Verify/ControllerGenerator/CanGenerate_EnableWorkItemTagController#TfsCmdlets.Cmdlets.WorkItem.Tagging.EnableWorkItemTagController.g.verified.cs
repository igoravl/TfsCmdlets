//HintName: TfsCmdlets.Cmdlets.WorkItem.Tagging.EnableWorkItemTagController.g.cs
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.Cmdlets.WorkItem.Tagging
{
    internal partial class EnableWorkItemTagController: ToggleWorkItemTagController
    {
        // Tag
        protected bool Has_Tag { get; set; }
        protected object Tag { get; set; }
        // Enabled
        protected bool Has_Enabled { get; set; }
        protected bool Enabled { get; set; }
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
            // Enabled
            Has_Enabled = Parameters.HasParameter("Enabled");
            Enabled = Parameters.Get<bool>("Enabled");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public EnableWorkItemTagController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}