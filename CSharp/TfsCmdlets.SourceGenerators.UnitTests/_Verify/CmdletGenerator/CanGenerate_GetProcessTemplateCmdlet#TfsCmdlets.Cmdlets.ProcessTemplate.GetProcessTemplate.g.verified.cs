//HintName: TfsCmdlets.Cmdlets.ProcessTemplate.GetProcessTemplate.g.cs
namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    [Cmdlet("Get", "TfsProcessTemplate")]
    [OutputType(typeof(Microsoft.TeamFoundation.Core.WebApi.Process))]
    public partial class GetProcessTemplate: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Alias("Organization")]
        [Parameter(ValueFromPipeline=true)]
        public object Collection { get; set; }
        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}