using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    /// <summary>
    /// Gets the contents of one or more work items.
    /// </summary>
    [TfsCmdlet(CmdletScope.Team, DefaultParameterSetName = "Query by revision", OutputType = typeof(WebApiWorkItem))]
    partial class GetWorkItem
    {
        /// <summary>
        /// HELP_PARAM_WORKITEM
        /// </summary>
        /// <seealso cref="Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem">
        /// A WorkItem object
        /// </seealso>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by revision", ValueFromPipeline = true)]
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "Query by date", ValueFromPipeline = true)]
        [Parameter(Position = 0, ParameterSetName = "Get deleted")]
        [Alias("id")]
        [ValidateNotNull()]
        public object WorkItem { get; set; }

        /// <summary>
        /// Specifies the title to look up for in a work item. Wildcards are supported. 
        /// When a wildcard is used, matches a portion of the title 
        /// (uses the operator "contains" in the WIQL query). Otherwise, matches the whole field 
        /// with the operator "=", unless -Ever is also specified. In that case, uses the operator 
        /// "was ever".
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] Title { get; set; }

        /// <summary>
        /// Specifies the description to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] Description { get; set; }

        /// <summary>
        /// Specifies the area path to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string AreaPath { get; set; }

        /// <summary>
        /// Specifies the iteration path to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string IterationPath { get; set; }

        /// <summary>
        /// Specifies the work item type to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [Alias("Type")]
        [SupportsWildcards]
        public string[] WorkItemType { get; set; }

        /// <summary>
        /// Specifies the state (field 'System.State') to look up for in a work item. Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] State { get; set; }

        /// <summary>
        /// Specifies the reason (field 'System.Reason') to look up for in a work item. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] Reason { get; set; }

        /// <summary>
        /// Specifies the Value Area (field 'Microsoft.VSTS.Common.ValueArea') to look up for in a work item. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] ValueArea { get; set; }

        /// <summary>
        /// Specifies the board column to look up for in a work item. 
        /// Wildcards are supported. 
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [SupportsWildcards]
        public string[] BoardColumn { get; set; }

        /// <summary>
        /// Specifies whether the work item is in the sub-column Doing or Done in a board.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public bool BoardColumnDone { get; set; }

        /// <summary>
        /// Specifies the name or email of the user that created the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public object[] CreatedBy { get; set; }

        /// <summary>
        ///  Specifies the date when the work item was created.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public DateTime[] CreatedDate { get; set; }

        /// <summary>
        /// Specifies the name or email of the user that did the latest change to the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public object ChangedBy { get; set; }

        /// <summary>
        /// Specifies the date of the latest change to the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public DateTime[] ChangedDate { get; set; }

        /// <summary>
        /// Specifies the date of the most recent change to the state of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public DateTime[] StateChangeDate { get; set; }

        /// <summary>
        /// Specifies the priority of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public int[] Priority { get; set; }

        /// <summary>
        /// Specifies the tags to look up for in a work item. When multiple tags are supplied, 
        /// they are combined with an OR operator - in other works, returns  work items that 
        /// contain ANY ofthe supplied tags.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Switches the query to historical query mode, by changing operators to 
        /// "WAS EVER" where possible.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        public SwitchParameter Ever { get; set; }

        /// <summary>
        /// Specifies a work item revision number to retrieve. When omitted, returns
        /// the latest revision of the work item.
        /// </summary>
        [Parameter(ParameterSetName = "Query by revision")]
        [Alias("rev")]
        public int Revision { get; set; }

        /// <summary>
        /// Returns the field values as they were defined in the work item revision that
        /// was the latest revision by the date specified.
        /// </summary>
        [Parameter(ParameterSetName = "Simple query")]
        [Parameter(Mandatory = true, ParameterSetName = "Query by date")]
        public DateTime AsOf { get; set; }

        /// <summary>
        /// Specifies a query written in WIQL (Work Item Query Language)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by WIQL")]
        [Alias("WIQL", "QueryText", "SavedQuery", "QueryPath")]
        public string Query { get; set; }

        /// <summary>
        /// Specifies which fields should be retrieved. When omitted, defaults to a set of
        /// standard fields that include Id, Title, Description, some state-related fields and more.
        /// </summary>
        [Parameter]
        [Parameter(Position = 0, ParameterSetName = "Query by filter")]
        public string[] Fields { get; set; } = new[] 
            {"System.AreaPath", "System.TeamProject", "System.IterationPath",
             "System.WorkItemType", "System.State", "System.Reason",
             "System.CreatedDate", "System.CreatedBy", "System.ChangedDate",
             "System.ChangedBy", "System.CommentCount", "System.Title",
             "System.BoardColumn", "System.BoardColumnDone", "Microsoft.VSTS.Common.StateChangeDate",
             "Microsoft.VSTS.Common.Priority", "Microsoft.VSTS.Common.ValueArea", "System.Description",
             "System.Tags" };

        /// <summary>
        /// Specifies a filter clause (the portion of a WIQL query after the WHERE keyword).
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Query by filter")]
        public string Where { get; set; }

        /// <summary>
        /// Fetches work items in "time-precision mode": search criteria in WIQL queries 
        /// take into account time information as well, not only dates.
        /// </summary>
        [Parameter(ParameterSetName = "Query by WIQL")]
        [Parameter(ParameterSetName = "Query by filter")]
        [Parameter(ParameterSetName = "Simple query")]
        public SwitchParameter TimePrecision { get; set; }

        /// <summary>
        /// Opens the specified work item in the default web browser.
        /// </summary>
        [Parameter(ParameterSetName = "Query by revision")]
        public SwitchParameter ShowWindow { get; set; }

        /// <summary>
        /// Gets deleted work items.
        /// </summary>
        [Parameter(ParameterSetName = "Get deleted", Mandatory = true)]
        public SwitchParameter Deleted { get; set; }

        /// <summary>
        /// Gets information about all links and attachments in the work item. When omitted, only fields are retrieved.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeLinks { get; set; }
    }
}