using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.Process.Field
{
    /// <summary>
    /// Deletes one or more work item field definitions from a collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(WorkItemField))]
    partial class RemoveProcessFieldDefinition
    {
        /// <summary>
        /// Specifies the name of the field(s) to be removed. Wildcards are supported. 
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "By Name")]
        [Alias("Name")]
        public object Field { get; set; } = "*";

        /// <summary>
        /// Specifies the reference name of the field(s) to be removed. Wildcards are supported.
        /// </summary>
        [Parameter(ParameterSetName = "By Reference Name", Mandatory = true)]
        public string[] ReferenceName { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController(typeof(WorkItemField), Client = typeof(IWorkItemTrackingHttpClient))]
    partial class RemoveProcessFieldDefinitionController
    {
        protected override IEnumerable Run()
        {
            foreach (var fld in Items)
            {
                if (!PowerShell.ShouldProcess(Collection, $"Remove field {fld.ReferenceName} ('{fld.Name}')")) continue;

                if (!Force && !PowerShell.ShouldContinue($"Are you sure you want to delete field {fld.ReferenceName} ('{fld.Name}')? " +
                    "You will lose all data stored in this field from all work items that are using it. " +
                    "Other undesired consequences may include breaking queries and/or dashboard widgets that reference it.")) continue;

                Client.DeleteFieldAsync(fld.ReferenceName).Wait();
            }

            return null;
        }
    }
}