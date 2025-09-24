//HintName: TfsCmdlets.Cmdlets.Admin.Registry.SetRegistryValue.g.cs
namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    [Cmdlet("Set", "TfsRegistryValue", SupportsShouldProcess = true)]
    [OutputType(typeof(object))]
    public partial class SetRegistryValue: CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
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