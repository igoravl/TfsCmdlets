//HintName: TfsCmdlets.Cmdlets.Identity.Group.RemoveGroupMemberController.g.cs
using System.Management.Automation;
using TfsCmdlets.Cmdlets.Identity.Group;
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    internal partial class RemoveGroupMemberController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IIdentityHttpClient Client { get; }
        // Member
        protected bool Has_Member { get; set; }
        protected object Member { get; set; }
        // Group
        protected bool Has_Group { get; set; }
        protected object Group { get; set; }
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
        protected IEnumerable Items => Data.Invoke("Get", "GroupMember");
        protected override void CacheParameters()
        {
            // Member
            Has_Member = Parameters.HasParameter("Member");
            Member = Parameters.Get<object>("Member");
            // Group
            Has_Group = Parameters.HasParameter("Group");
            Group = Parameters.Get<object>("Group");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public RemoveGroupMemberController(TfsCmdlets.HttpClients.IIdentityHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}