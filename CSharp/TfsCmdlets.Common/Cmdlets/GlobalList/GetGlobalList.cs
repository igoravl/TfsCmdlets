using System.Management.Automation;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Gets the contents of one or more Global Lists.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGlobalList")]
    [OutputType(typeof(TfsGlobalList))]
    [DesktopOnly]
    public partial class GetGlobalList : BaseCmdlet<TfsGlobalList>
    {
        /// <summary>
        /// Specifies the name of the global list. Wildcards supported. 
        /// When omitted, defaults to all global lists in the supplied team project collection.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string GlobalList { get; set; } = "*";

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }
    }
}