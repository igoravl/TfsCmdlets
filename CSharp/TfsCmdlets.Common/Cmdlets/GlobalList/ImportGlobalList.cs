using System.Management.Automation;
using System.Xml;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Imports one or more Global Lists from an XML document
    /// </summary>
    /// <remarks>
    /// This cmdletsimports an XML containing one or more global lists and their respective items, 
    /// in the same format used by witadmin. It is functionally equivalent to "witadmin importgloballist"
    /// </remarks>
    /// <example>
    ///   <code>Get-Content gl.xml | Import-GlobalList</code>
    ///   <para>Imports the contents of an XML document called gl.xml to the current project collection</para>
    /// </example>
    /// <notes>
    /// To import global lists, you must be a member of the Project Collection Administrators security group.
    /// </notes>
    [Cmdlet(VerbsData.Import, "TfsGlobalList", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [DesktopOnly]
    public partial class ImportGlobalList : BaseGlobalListCmdlet
    {
        /// <summary>
        /// XML document object containing one or more global list definitions.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Xml")]
        public object InputObject { get; set; }

        /// <summary>
        /// Allows the cmdlet to import a global list that already exists.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }
    }
}