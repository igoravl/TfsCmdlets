//HintName: TfsCmdlets.Cmdlets.TeamProject.NewTeamProjectController.g.cs
using System.Management.Automation;
using System.Threading;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Operations;
namespace TfsCmdlets.Cmdlets.TeamProject
{
    internal partial class NewTeamProjectController: ControllerBase
    {
        private TfsCmdlets.HttpClients.IProjectHttpClient Client { get; }
        // Project
        protected bool Has_Project { get; set; }
        protected string[] Project { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // SourceControl
        protected bool Has_SourceControl { get; set; }
        protected string SourceControl { get; set; }
        // ProcessTemplate
        protected bool Has_ProcessTemplate { get; set; }
        protected object ProcessTemplate { get; set; }
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
        // DataType
        public override Type DataType => typeof(Microsoft.TeamFoundation.Core.WebApi.TeamProject);
        protected override void CacheParameters()
        {
            // Project
            Has_Project = Parameters.HasParameter("Project");
            Project = Parameters.Get<string[]>("Project");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // SourceControl
            Has_SourceControl = Parameters.HasParameter("SourceControl");
            SourceControl = Parameters.Get<string>("SourceControl", "Git");
            // ProcessTemplate
            Has_ProcessTemplate = Parameters.HasParameter("ProcessTemplate");
            ProcessTemplate = Parameters.Get<object>("ProcessTemplate");
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewTeamProjectController(IAsyncOperationAwaiter asyncAwaiter, TfsCmdlets.HttpClients.IProjectHttpClient client, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            AsyncAwaiter = asyncAwaiter;
            Client = client;
        }
    }
}