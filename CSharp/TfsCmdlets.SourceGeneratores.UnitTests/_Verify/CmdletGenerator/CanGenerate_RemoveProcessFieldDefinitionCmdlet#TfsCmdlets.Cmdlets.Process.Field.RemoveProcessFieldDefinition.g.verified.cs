//HintName: TfsCmdlets.Cmdlets.Process.Field.RemoveProcessFieldDefinition.g.cs
namespace TfsCmdlets.Cmdlets.Process.Field
{
    [Cmdlet("Remove", "TfsProcessFieldDefinition", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField))]
    public partial class RemoveProcessFieldDefinition: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter()]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}