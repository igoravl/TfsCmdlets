using System.Management.Automation;
using System.Security;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.ConfigServer
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
    ///   <code>Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs</code>
    ///   <para>Connects to the TFS server specified by the URL in the Server argument</para>
    /// </example>
    /// <example>
    ///   <code>Connect-TfsConfigurationServer -Server vsalm</code>
    ///   <para>Connects to a previously registered TFS server by its user-defined name "vsalm". For more information, see Get-TfsRegisteredConfigurationServer</para>
    /// </example>
    /// <para type="input">Microsoft.TeamFoundation.Client.TfsConfigurationServer</para>
    /// <para type="input">System.String</para>
    /// <para type="input">System.Uri</para>

    [Cmdlet(VerbsCommunications.Connect, "TfsConfigurationServer", DefaultParameterSetName = "Prompt for credential")]
    [TfsCmdlet(CmdletScope.None)]
    partial class ConnectConfigurationServer
    {
        /// <summary>
        ///   Specifies either a URL/name of the Team Foundation Server to connect to, or a previously 
        ///   initialized TfsConfigurationServer object. When using a URL, it must be fully qualified. 
        ///   To connect to a Team Foundation Server instance by using its name, it must have been 
        ///   previously registered.
        /// </summary>
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public object Server { get; set; }
    }
}