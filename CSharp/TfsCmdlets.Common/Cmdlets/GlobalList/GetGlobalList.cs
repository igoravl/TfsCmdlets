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
    public class GetGlobalList : BaseCmdlet
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

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            WriteItems<Models.GlobalList>();
        }
    }

    [Exports(typeof(Models.GlobalList))]
    internal class GlobalListDataService : BaseDataService<Models.GlobalList>
    {
        protected override IEnumerable<Models.GlobalList> DoGetItems()
        {
            return Provider.GetService<IGlobalListService>(Cmdlet, Parameters).Export();
        }
    }
}