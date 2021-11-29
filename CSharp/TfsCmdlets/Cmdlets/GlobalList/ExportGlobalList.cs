using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Exports the contents of one or more Global Lists to XML.
    /// </summary>
    /// <remarks>
    /// This cmdlet generates an XML containing one or more global lists and their respective items, 
    /// in the same format used by witadmin. It is functionally equivalent to "witadmin exportgloballist"
    /// </remarks>
    /// <example>
    ///   <code>Export-TfsGlobalList | Out-File "gl.xml"</code>
    ///   <para>Exports all global lists in the current project collection to a file called gl.xml.</para>
    /// </example>
    /// <example>
    ///   <code>Export-TfsGlobalList -Name "Builds - *"</code>
    ///   <para>Exports all build-related global lists (with names starting with "Build - ") and
    ///     return the resulting XML document.</para>
    /// </example>
    /// <notes>
    /// To export or list global lists, you must be a member of the Project Collection Valid Users 
    /// group or have the "View collection-level information" permission set to Allow.
    /// </notes>
    /// <input>Microsoft.TeamFoundation.Client.TfsTeamProjectCollection</input>
    /// <input>System.String</input>
    /// <input>System.Uri</input>
    [Cmdlet(VerbsData.Export, "TfsGlobalList")]
    [OutputType(typeof(string))]
    [DesktopOnly]
    public class ExportGlobalList : CollectionScopedGetCmdlet
    {
        /// <summary>
        /// Specifies the name of the global list to be exported. Wildcards are supported. 
        /// When omitted, it defaults to all global lists in the supplied team project collection. 
        /// When using wilcards, a single XML document will be producer containing all matching 
        /// global lists.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object GlobalList { get; set; } = "*";
    }
}