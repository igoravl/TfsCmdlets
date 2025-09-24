//HintName: TfsCmdlets.Cmdlets.RestApi.GetRestClient.g.cs
namespace TfsCmdlets.Cmdlets.RestApi
{
    [Cmdlet("Get", "TfsRestClient", DefaultParameterSetName = "Get by collection")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase))]
    public partial class GetRestClient: CmdletBase
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