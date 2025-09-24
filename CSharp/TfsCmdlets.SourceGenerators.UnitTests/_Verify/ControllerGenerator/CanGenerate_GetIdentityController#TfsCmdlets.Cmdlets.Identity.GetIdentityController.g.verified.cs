//HintName: TfsCmdlets.Cmdlets.Identity.GetIdentityController.g.cs
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Licensing;
namespace TfsCmdlets.Cmdlets.Identity
{
    internal partial class GetIdentityController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IIdentityHttpClient Client { get; }
        // Identity
        protected bool Has_Identity => Parameters.HasParameter(nameof(Identity));
        protected IEnumerable Identity
        {
            get
            {
                var paramValue = Parameters.Get<object>(nameof(Identity));
                if(paramValue is ICollection col) return col;
                return new[] { paramValue };
            }
        }
        // QueryMembership
        protected bool Has_QueryMembership { get; set; }
        protected Microsoft.VisualStudio.Services.Identity.QueryMembership QueryMembership { get; set; }
        // Current
        protected bool Has_Current { get; set; }
        protected bool Current { get; set; }
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
            // QueryMembership
            Has_QueryMembership = Parameters.HasParameter("QueryMembership");
            QueryMembership = Parameters.Get<Microsoft.VisualStudio.Services.Identity.QueryMembership>("QueryMembership", WebApiQueryMembership.Direct);
            // Current
            Has_Current = Parameters.HasParameter("Current");
            Current = Parameters.Get<bool>("Current");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public GetIdentityController(TfsCmdlets.HttpClients.IIdentityHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            Client = client;
        }
    }
}