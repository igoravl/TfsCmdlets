using System.Collections;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Pipeline.XamlBuild
{
    /// <summary>
    /// Queues a XAML Build.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DesktopOnly = true, SupportsShouldProcess = true)]
    partial class StartXamlBuild
    {
        [Parameter(Mandatory = true, Position = 0)]
        public object BuildDefinition { get; set; }

        [Parameter]
        [ValidateSet("LatestOnQueue", "LatestOnBuild", "Custom")]
        public string GetOption { get; set; } = "LatestOnBuild";

        [Parameter]
        public string GetVersion { get; set; }

        [Parameter]
        public string DropLocation { get; set; }

        [Parameter]
        public Hashtable BuildParameters { get; set; }
    }
}