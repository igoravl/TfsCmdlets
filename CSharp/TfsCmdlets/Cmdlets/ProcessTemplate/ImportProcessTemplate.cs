using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Imports a process template definition from disk.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true, SupportsShouldProcess = true)]
    partial class ImportProcessTemplate
    {
        /// <summary>
        /// Specifies the folder containing the process template to be imported. This folder must contain 
        /// the file ProcessTemplate.xml
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public string Path { get; set; }

        /// <summary>
        /// Specifies the state of the template after it is imported. When set to Invisible, the process template
        /// will not be listed in the server UI.
        /// </summary>
        [Parameter]
        [ValidateSet("Visible")]
        public string State { get; set; } = "Visible";
    }
}