using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    /// <para type="synopsis">
    /// Connects to a configuration server.
    /// </para>
    /// </summary>
    /// <list type="alertSet">
    ///   <item>
    ///     <description>
    ///     A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, 
    ///     it is represented by the Tfs_Configuration database. Operations that should be performed on a server level 
    ///     (such as setting server-level permissions) require a connection to a TFS configuration server. 
    ///     Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer 
    ///     </description>
    ///   </item>
    /// </list>
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
	[OutputType(typeof(VssConnection))]
    public class ConnectConfigurationServer: BaseCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized 
        /// TfsConfigurationServer object. When using a URL, it must be fully qualified. To connect to a 
        /// Team Foundation Server instance by using its name, it must have been previously registered.
        /// </para>
        /// </summary>
		[Parameter(Mandatory=true, Position=0, ValueFromPipeline=true)]
		[ValidateNotNull]
		public object Server { get; set; }

        /// <summary>
        /// <para type="description">
        /// HELP_CREDENTIAL
        /// </para>
        /// </summary>
		[Parameter(ParameterSetName="Explicit credentials")]
		public object Credential { get; set; }

        /// <summary>
        /// <para type="description">
        /// HELP_INTERACTIVE
        /// </para>
        /// </summary>
		[Parameter(ParameterSetName="Prompt for credentials", Mandatory=true)]
		public SwitchParameter Interactive { get; set; }

        /// <summary>
        /// <para type="description">
        /// HELP_PASSTHRU
        /// </para>
        /// </summary>
		[Parameter]
		public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var srv = this.GetServer();
            srv.Connect();

            CurrentConnections.Set(srv);

            this.Log($"Connected to {srv.Uri}, ID {srv.ServerId}, as {srv.AuthorizedIdentity.DisplayName}");

            if (Passthru)
            {
                WriteObject(srv);
            }
        }
	}
}
