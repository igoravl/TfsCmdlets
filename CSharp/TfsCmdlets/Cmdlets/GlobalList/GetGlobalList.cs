using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Gets the contents of one or more Global Lists.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, DesktopOnly=true, OutputType = typeof(Models.GlobalList))]
    partial class GetGlobalList 
    {
        /// <summary>
        /// Specifies the name of the global list. Wildcards are supported. 
        /// When omitted, defaults to all global lists in the supplied team project collection.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string GlobalList { get; set; } = "*";
    }
}