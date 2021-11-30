using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Imports a process template definition from disk.
    /// </summary>
    [Cmdlet(VerbsData.Import, "TfsProcessTemplate", SupportsShouldProcess = true)]
    [DesktopOnly]
    public partial class ImportProcessTemplate : CmdletBase
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
        [Parameter()]
        [ValidateSet("Visible")]
        public string State { get; set; } = "Visible";

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        // TODO

    }
}