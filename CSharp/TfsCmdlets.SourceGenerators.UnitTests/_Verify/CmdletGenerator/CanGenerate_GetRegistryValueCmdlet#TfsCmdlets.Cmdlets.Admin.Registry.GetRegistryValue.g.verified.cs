//HintName: TfsCmdlets.Cmdlets.Admin.Registry.GetRegistryValue.g.cs
namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    [Cmdlet("Get", "TfsRegistryValue")]
    [OutputType(typeof(object))]
    public partial class GetRegistryValue: CmdletBase
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