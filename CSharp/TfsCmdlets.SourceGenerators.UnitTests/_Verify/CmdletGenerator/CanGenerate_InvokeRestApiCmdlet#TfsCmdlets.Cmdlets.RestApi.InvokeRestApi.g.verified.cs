//HintName: TfsCmdlets.Cmdlets.RestApi.InvokeRestApi.g.cs
namespace TfsCmdlets.Cmdlets.RestApi
{
    [Cmdlet("Invoke", "TfsRestApi", SupportsShouldProcess = true)]
    public partial class InvokeRestApi: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_TEAM
        /// </summary>
        [Parameter()]
        public object Team { get; set; }
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }
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