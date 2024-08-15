using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.Process.Field
{
    /// <summary>
    /// Gets information from one or more organization-wide work item fields.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(WorkItemField))]
    partial class GetProcessFieldDefinition
    {
        /// <summary>
        /// Specifies the name of the field(s) to be returned. Wildcards are supported. 
        /// When omitted, all fields in the given organization are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "By Name")]
        [Alias("Name")]
        [SupportsWildcards()]
        public object Field { get; set; } = "*";

        /// <summary>
        /// Specifies the reference name of the field(s) to be returned. Wildcards are supported.
        /// </summary>
        [Parameter(ParameterSetName = "By Reference Name", Mandatory = true)]
        public string[] ReferenceName { get; set; }

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

    [CmdletController(typeof(WorkItemField), Client = typeof(IWorkItemTrackingHttpClient))]
    partial class GetProcessFieldDefinitionController
    {
        protected override IEnumerable Run()
        {
            string tpName;

            if (Has_Project)
            {
                var tp = Data.GetProject();
                tpName = tp.Name;
            }

            var expand = GetFieldsExpand.None |
                (IncludeExtensionFields ? GetFieldsExpand.ExtensionFields : GetFieldsExpand.None) |
                (IncludeDeleted ? GetFieldsExpand.IncludeDeleted : GetFieldsExpand.None);

            switch (ParameterSetName)
            {
                case "By Name":
                    {
                        foreach (var f in Field)
                        {
                            switch (f)
                            {
                                case WorkItemField wif:
                                    {
                                        yield return wif;
                                        yield break;
                                    }
                                case string s when s.IsWildcard():
                                    {
                                        yield return Client.GetFieldsAsync(expand)
                                            .GetResult($"Error getting fields '{s}'")
                                            .Where(field => field.Name.IsLike(s));
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
                        break;
                    }
                case "By Reference Name":
                    {
                        foreach (var refName in ReferenceName)
                        {
                            yield return Client.GetFieldsAsync(expand)
                                .GetResult($"Error getting field with reference name '{ReferenceName}'")
                                .Where(field => field.ReferenceName.IsLike(refName));
                        }
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"Unknown parameter set '{ParameterSetName}'");
                    }
            }
        }
    }
}