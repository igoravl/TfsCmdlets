using System.Management.Automation;
using WebApiWorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Gets the contents of one or more work items.
    /// </summary>
    /// <related>https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax</related>
    [Cmdlet(VerbsCommon.Search, "TfsWorkItem", DefaultParameterSetName = "Query by text")]
    [OutputType(typeof(WebApiWorkItem))]
    [TfsCmdlet(CmdletScope.Project)]
    partial class SearchWorkItem : CmdletBase
    {
        /// <summary>
        /// Specifies the text to search for. Supports the Quick Filter syntax described in 
        /// https://docs.microsoft.com/en-us/azure/devops/project/search/advanced-work-item-search-syntax
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull()]
        public string Query { get; set; }

        /// <summary>
        /// Specifies the maximum quantity of results. Supports between 1 and 1000 results. 
        /// When omitted, defaults to 100. Currently this cmdlet does not support result pagination.
        /// </summary>
        [Parameter]
        [ValidateRange(1, 1000)]
        public int Results { get; set; } = 100;
    }
}