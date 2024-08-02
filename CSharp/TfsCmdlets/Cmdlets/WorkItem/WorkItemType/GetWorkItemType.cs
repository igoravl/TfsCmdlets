using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace TfsCmdlets.Cmdlets.WorkItem.WorkItemType
{
    /// <summary>
    /// Gets one or more Work Item Type definitions from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, DefaultParameterSetName = "Get by type", OutputType = typeof(WebApiWorkItemType))]
    partial class GetWorkItemType
    {
        /// <summary>
        /// Specifies one or more work item type names to return. Wildcards are supported. 
        /// When omitted, returns all work item types in the given team project.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by type")]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Type { get; set; } = "*";

        /// <summary>
        /// Speficies a work item whose corresponding type should be returned.
        /// </summary>
        [Parameter(ParameterSetName = "Get by work item", Mandatory = true)]
        public object WorkItem { get; set; }
    }

    [CmdletController(typeof(WebApiWorkItemType), Client=typeof(IWorkItemTrackingHttpClient))]
    partial class GetWorkItemTypeController
    {
        protected override IEnumerable Run()
        {
            var type = Parameters.Get<object>(nameof(GetWorkItemType.Type));

            var shouldSkipWi = !Parameters.HasParameter(nameof(GetWorkItemType.WorkItem)) ||
                (Parameters.Get<object>(nameof(GetWorkItemType.WorkItem)) is int i && i == 0);

            if (Parameters.HasParameter(nameof(GetWorkItemType.WorkItem)) && !shouldSkipWi)
            {
                var workItem = Data.GetItem<WebApiWorkItem>(nameof(GetWorkItemType.WorkItem));
                type = workItem.Fields["System.WorkItemType"];
            }

            var tp = Data.GetProject();

            switch (type)
            {
                case WebApiWorkItemType t:
                    {
                        return new[] { t };
                    }
                case string t:
                    {
                        return Client.GetWorkItemTypesAsync(tp.Id)
                            .GetResult($"Error getting type(s) '{t}'")
                            .Where(t1 => t1.Name.IsLike(t));
                    }
                case IEnumerable<string> types:
                    {
                        return Client.GetWorkItemTypesAsync(tp.Id)
                            .GetResult($"Error getting type(s) '{string.Join(", ", types)}'")
                            .Where(t1 => types.Any(t2 => t1.Name.IsLike(t2)));
                    }
            }

            Logger.LogError(new ArgumentException($"Invalid or non-existent work item type '{type}'"));
            return null;
        }
    }
}