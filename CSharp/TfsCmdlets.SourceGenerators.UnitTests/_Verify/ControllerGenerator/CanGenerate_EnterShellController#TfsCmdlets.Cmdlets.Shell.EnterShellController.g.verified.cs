//HintName: TfsCmdlets.Cmdlets.Shell.EnterShellController.g.cs
using System.Management.Automation;
using System.Management.Automation.Runspaces;
namespace TfsCmdlets.Cmdlets.Shell
{
    internal partial class EnterShellController: ControllerBase
    {
        // WindowTitle
        protected bool Has_WindowTitle { get; set; }
        protected string WindowTitle { get; set; }
        // DoNotClearHost
        protected bool Has_DoNotClearHost { get; set; }
        protected bool DoNotClearHost { get; set; }
        // NoLogo
        protected bool Has_NoLogo { get; set; }
        protected bool NoLogo { get; set; }
        // NoProfile
        protected bool Has_NoProfile { get; set; }
        protected bool NoProfile { get; set; }
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        protected override void CacheParameters()
        {
            // WindowTitle
            Has_WindowTitle = Parameters.HasParameter("WindowTitle");
            WindowTitle = Parameters.Get<string>("WindowTitle", "Azure DevOps Shell");
            // DoNotClearHost
            Has_DoNotClearHost = Parameters.HasParameter("DoNotClearHost");
            DoNotClearHost = Parameters.Get<bool>("DoNotClearHost");
            // NoLogo
            Has_NoLogo = Parameters.HasParameter("NoLogo");
            NoLogo = Parameters.Get<bool>("NoLogo");
            // NoProfile
            Has_NoProfile = Parameters.HasParameter("NoProfile");
            NoProfile = Parameters.Get<bool>("NoProfile");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public EnterShellController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}