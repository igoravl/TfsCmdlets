using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Gets the configuration server database connection string.
    /// </summary>
    /// <related uri="https://tfscmdlets.dev/admin/get-tfsconfigurationConnectionstring/">Online version:</related>
    /// <related>Get-TfsInstallationPath</related>
    [TfsCmdlet(CmdletScope.None, DesktopOnly = true, OutputType = typeof(string))]
    partial class GetConfigurationConnectionString
    {
        /// <summary>
        /// Specifies the name of a Team Foundation Server application tier from which to 
        /// retrieve the connection string.
        /// </summary>
        [Parameter(ParameterSetName = "Use computer name")]
        [ValidateNotNullOrEmpty]
        public string ComputerName { get; set; } = "localhost";

        /// <summary>
        /// The machine name of the server where the TFS component is installed. 
        /// It must be properly configured for PowerShell Remoting in case it's a remote machine. 
        /// Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a 
        /// previously opened PowerShell Remote session can be provided instead. 
        /// When omitted, defaults to the local machine where the script is being run
        /// </summary>
        [Parameter(ParameterSetName = "Use session", Mandatory = true)]
        public PSSession Session { get; set; }

        /// <summary>
        /// The TFS version number, represented by the year in its name. For e.g. TFS 2015, use "2015".
        /// When omitted, will default to the newest installed version of TFS / Azure DevOps Server
        /// </summary>
		[Parameter]
        [ValidateSet("2005", "2008", "2010", "2012", "2013", "2015", "2017", "2018", "2019", "2020")]
        public int Version { get; set; }

        /// <summary>
        /// The user credentials to be used to access a remote machine. Those credentials must have 
        /// the required permission to execute a PowerShell Remote session on that computer.
        /// </summary>
		[Parameter]
        [Credential]
        public PSCredential Credential { get; set; } = PSCredential.Empty;
    }
}
