using TfsCmdlets.Services;
using System.Collections.Generic;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Gets the contents of one or more Global Lists.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TfsGlobalList")]
    [OutputType(typeof(Models.GlobalList))]
    [DesktopOnly]
    public class GetGlobalList : GetCmdletBase<Models.GlobalList>
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

    [Exports(typeof(Models.GlobalList))]
    internal partial class GlobalListDataService : BaseDataService<Models.GlobalList>
    {
        protected override IEnumerable<Models.GlobalList> DoGetItems()
        {
            return Provider.GetService<IGlobalListService>(Cmdlet, Parameters).Export();
        }
    }
}