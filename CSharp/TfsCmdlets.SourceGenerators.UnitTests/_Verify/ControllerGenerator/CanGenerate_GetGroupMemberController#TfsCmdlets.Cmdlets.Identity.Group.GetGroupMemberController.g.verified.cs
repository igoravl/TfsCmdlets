//HintName: TfsCmdlets.Cmdlets.Identity.Group.GetGroupMemberController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using TfsCmdlets.Cmdlets.Identity.Group;
namespace TfsCmdlets.Cmdlets.Identity.Group
{
    internal partial class GetGroupMemberController: ControllerBase
    {
        // Group
        protected bool Has_Group => Parameters.HasParameter(nameof(Group));
        protected IEnumerable Group
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Group));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // Member
        protected bool Has_Member { get; set; }
        protected string Member { get; set; }
        // Recurse
        protected bool Has_Recurse { get; set; }
        protected bool Recurse { get; set; }
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
        public override Type DataType => typeof(TfsCmdlets.Models.Identity);
        protected override void CacheParameters()
        {
            // Member
            Has_Member = Parameters.HasParameter("Member");
            Member = Parameters.Get<string>("Member", "*");
            // Recurse
            Has_Recurse = Parameters.HasParameter("Recurse");
            Recurse = Parameters.Get<bool>("Recurse");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetGroupMemberController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}