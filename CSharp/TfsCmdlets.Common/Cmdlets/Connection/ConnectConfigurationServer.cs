using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    ///  Connects to a configuration server.
    /// </summary>
    /// <remarks>
    ///  A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, 
    ///  it is represented by the Tfs_Configuration database. Operations that should be performed on a server level 
    ///  (such as setting server-level permissions) require a connection to a TFS configuration server. 
    ///  Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer. 
    ///  NOTE: Currently it is only supported in Windows PowerShell.
    /// </remarks>
    /// <example>
    ///   <code>PS&gt; Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs</code>
    ///   <para>Connects to the TFS server specified by the URL in the Server argument</para>
    /// </example>
    /// <example>
    ///   <code>PS&gt; Connect-TfsConfigurationServer -Server vsalm</code>
    ///   <para>Connects to a previously registered TFS server by its user-defined name "vsalm". For more information, see Get-TfsRegisteredConfigurationServer</para>
    /// </example>
    /// <para type="input">Microsoft.TeamFoundation.Client.TfsConfigurationServer</para>
    /// <para type="input">System.String</para>
    /// <para type="input">System.Uri</para>

    [Cmdlet(VerbsCommunications.Connect, "ConfigurationServer", DefaultParameterSetName="Explicit credentials")]
    [WindowsOnly]
    public partial class ConnectConfigurationServer: BaseCmdlet
    {
        /// <summary>
        ///   Specifies either a URL/name of the Team Foundation Server to connect to, or a previously 
        ///   initialized TfsConfigurationServer object. When using a URL, it must be fully qualified. 
        ///   To connect to a Team Foundation Server instance by using its name, it must have been 
        ///   previously registered.
        /// </summary>
		[Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
		[ValidateNotNull]
		public object Server { get; set; }

        /// <summary>HELP_PARAM_CREDENTIAL</summary>
		[Parameter(ParameterSetName="Explicit credentials")]
		public object Credential { get; set; }

        /// <summary>HELP_PARAM_INTERACTIVE</summary>
		[Parameter(ParameterSetName="Prompt for credentials", Mandatory=true)]
		public SwitchParameter Interactive { get; set; }

        /// <summary>HELP_PARAM_PASSTHRU</summary>
		[Parameter]
		public SwitchParameter Passthru { get; set; }

        partial void DoProcessRecord();

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            DoProcessRecord();
        }
	}
}
