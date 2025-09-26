//HintName: TfsCmdlets.Cmdlets.TeamProject.Avatar.RemoveTeamProjectAvatarController.g.cs
using Microsoft.TeamFoundation.Core.WebApi;
namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    internal partial class RemoveTeamProjectAvatarController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IProjectHttpClient Client { get; }
        // Project
        protected bool Has_Project { get; set; }
        protected object Project { get; set; }
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
        protected IEnumerable Items => Data.Invoke("Get", "TeamProjectAvatar");
        protected override void CacheParameters()
        {
            // Project
            Has_Project = Parameters.HasParameter("Project");
            Project = Parameters.Get<object>("Project");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveTeamProjectAvatarController(TfsCmdlets.HttpClients.IProjectHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}