using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TfsCmdlets.HttpClients;

namespace TfsCmdlets.Cmdlets.WorkItem.Field
{
    /// <summary>
    /// Gets information from one or more organization-wide work item fields.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemField))]
    partial class GetWorkItemField
    {
        /// <summary>
        /// Specifies the name or the reference name of the field(s) to be returned. Wildcards are supported. 
        /// When omitted, all fields in the given organization are returned.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Field { get; set; } = "*";

        /// <summary>
        /// Limits the search to the specified project.
        /// </summary>
        [Parameter]
        public object Project { get; set; }

        /// <summary>
        /// Specifies whether to include extension fields in the result.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeExtensionFields { get; set; }

        /// <summary>
        /// Specifies whether to include deleted fields in the result.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeDeleted { get; set; }
    }

    // Controller

    [CmdletController]
    partial class GetWorkItemFieldController
    {
        [Import]
        private IWorkItemTrackingHttpClient Client { get; set; }
        
        protected override IEnumerable Run()
        {
            string tpName;

            if(Has_Project) {
                var tp = Data.GetProject();
                tpName = tp.Name;
            }

            foreach (var f in Field)
            {
                switch (f)
                {
                    case string s when s.IsWildcard():
                        {
                            var expand = GetFieldsExpand.None |
                                (IncludeExtensionFields ? GetFieldsExpand.ExtensionFields : GetFieldsExpand.None) |
                                (IncludeDeleted ? GetFieldsExpand.IncludeDeleted : GetFieldsExpand.None);

                            yield return Client.GetFieldsAsync(expand)
                                .GetResult($"Error getting field '{s}'")
                                .Where(field => field.Name.IsLike(s) || field.ReferenceName.IsLike(s));
                            break;
                        }
                    case string s when !string.IsNullOrEmpty(s):
                        {
                            yield return Client.GetFieldAsync(fieldNameOrRefName: s)
                                .GetResult($"Error getting field '{s}'");
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent field '{f}'");
                        }
                }
            }
        }
    }
}