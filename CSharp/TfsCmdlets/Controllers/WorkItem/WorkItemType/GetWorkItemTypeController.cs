using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using TfsCmdlets.Cmdlets.WorkItem.WorkItemType;

namespace TfsCmdlets.Controllers.WorkItem.WorkItemType
{
    /// <summary>
    /// Exports an XML work item type definition from a team project.
    /// </summary>
    [CmdletController(typeof(WebApiWorkItemType))]
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

            var client = Data.GetClient<WorkItemTrackingHttpClient>();
            var tp = Data.GetProject();

            switch (type)
            {
                case WebApiWorkItemType t:
                    {
                        return new[] { t };
                    }
                case string t:
                    {
                        return client.GetWorkItemTypesAsync(tp.Id)
                            .GetResult($"Error getting type(s) '{t}'")
                            .Where(t1 => t1.Name.IsLike(t));
                    }
                case IEnumerable<string> types:
                    {
                        return client.GetWorkItemTypesAsync(tp.Id)
                            .GetResult($"Error getting type(s) '{string.Join(", ", types)}'")
                            .Where(t1 => types.Any(t2 => t1.Name.IsLike(t2)));
                    }
            }

            Logger.LogError(new ArgumentException($"Invalid or non-existent work item type '{type}'"));
            return null;
        }
    }
}