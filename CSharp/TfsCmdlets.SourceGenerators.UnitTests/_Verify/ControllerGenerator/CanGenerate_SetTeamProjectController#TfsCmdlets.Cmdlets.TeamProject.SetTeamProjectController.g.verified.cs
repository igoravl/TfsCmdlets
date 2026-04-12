//HintName: TfsCmdlets.Cmdlets.TeamProject.SetTeamProjectController.g.cs
using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
namespace TfsCmdlets.Cmdlets.TeamProject
{
    internal partial class SetTeamProjectController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IProjectHttpClient Client { get; }
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // AvatarImage
        protected bool Has_AvatarImage { get; set; }
        protected string AvatarImage { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
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
        protected IEnumerable<Microsoft.TeamFoundation.Core.WebApi.TeamProject> Items => Project switch {
            Microsoft.TeamFoundation.Core.WebApi.TeamProject item => new[] { item },
            IEnumerable<Microsoft.TeamFoundation.Core.WebApi.TeamProject> items => items,
            _ => Data.GetItems<Microsoft.TeamFoundation.Core.WebApi.TeamProject>()
        };
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject);
        protected override void CacheParameters()
        {
            // Project
            Has_Project = Parameters.HasParameter("Project");
            Project = Parameters.Get<object>("Project");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // AvatarImage
            Has_AvatarImage = Parameters.HasParameter("AvatarImage");
            AvatarImage = Parameters.Get<string>("AvatarImage");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public SetTeamProjectController(IAsyncOperationAwaiter asyncAwaiter, TfsCmdlets.HttpClients.IProjectHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            AsyncAwaiter = asyncAwaiter;
            Client = client;
        }
    }
}