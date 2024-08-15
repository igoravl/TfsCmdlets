using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Deletes one or more Git fldsitories from a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
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

                Client.DeleteFieldAsync(fld.ReferenceName).Wait();
            }

            return null;
        }
    }
}