using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    /// <summary>
    ///   Sets the value of a given Team Foundation Server registry entry.
    /// </summary>
    /// <remarks>
    ///   The 'Set-TfsRegistry' cmdlet changes the value of a TFS registry key to the 
    ///   value specified in the command.
    /// </remarks>
    /// <example>
    ///   <code>Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'</code>
    ///   <para>Gets the current value of the 'EmailEnabled' key in the TFS Registry</para>
    /// </example>
    /// <notes>
    ///   The registry is an internal, hierarchical database that TFS uses to store its 
    ///   configuration and user-level settings and preferences.
    /// 
    ///   IMPORTANT: Retrieving user-scoped values is currently not supported.
    /// </notes>
    [Cmdlet(VerbsCommon.Set, "TfsRegistryValue", SupportsShouldProcess = true)]
    [OutputType(typeof(object))]
    public class SetRegistryValue : CmdletBase
    {
        /// <summary>
        /// Specifies the full path of the TFS Registry key
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Path { get; set; }

        /// <summary>
        /// Specifies the new value of the Registry key. To remove an existing value, 
        /// set it to $null
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        [AllowNull, AllowEmptyString]
        public string Value { get; set; }

        /// <summary>
        /// Specifies the scope under which to search for the key. 
        /// When omitted, defaults to the Server scope.
        /// </summary>
        [Parameter()]
        public RegistryScope Scope { get; set; } = RegistryScope.Server;

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter()]
        public object Server { get; set; }
    }
}