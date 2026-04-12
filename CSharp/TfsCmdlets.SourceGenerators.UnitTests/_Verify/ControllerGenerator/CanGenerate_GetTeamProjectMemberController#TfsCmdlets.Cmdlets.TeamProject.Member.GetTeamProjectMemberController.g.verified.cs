//HintName: TfsCmdlets.Cmdlets.TeamProject.Member.GetTeamProjectMemberController.g.cs
using System.Management.Automation;
using TfsCmdlets.Models;
using TfsIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
namespace TfsCmdlets.Cmdlets.TeamProject.Member
{
    internal partial class GetTeamProjectMemberController: ControllerBase
    {
        // Member
        protected bool Has_Member => Parameters.HasParameter(nameof(Member));
        protected IEnumerable Member
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Member), "*");
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // AsIdentity
        protected bool Has_AsIdentity { get; set; }
        protected bool AsIdentity { get; set; }
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
        public override Type DataType => typeof(TfsCmdlets.Models.TeamProjectMember);
        protected override void CacheParameters()
        {
            // AsIdentity
            Has_AsIdentity = Parameters.HasParameter("AsIdentity");
            AsIdentity = Parameters.Get<bool>("AsIdentity");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetTeamProjectMemberController(IRestApiService restApiService, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApiService = restApiService;
        }
    }
}