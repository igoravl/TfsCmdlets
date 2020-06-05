using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// Gets the configuration server database connection string.
    /// </summary>
    /// <related uri="https://tfscmdlets.dev/admin/get-tfsconfigurationserverconnectionstring/">Online version:</related>
    /// <related>Get-TfsInstallationPath</related>
    [Cmdlet(VerbsCommon.Get, "TfsConfigurationServerConnectionString")]
    [OutputType(typeof(string))]
    [WindowsOnly]
    public class GetConfigurationServerConnectionString : BaseCmdlet
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
		[Parameter()]
        [ValidateSet("2005", "2008", "2010", "2012", "2013", "2015", "2017", "2018", "2019", "2020")]
        public int Version { get; set; }

        /// <summary>
        /// The user credentials to be used to access a remote machine. Those credentials must have 
        /// the required permission to execute a PowerShell Remote session on that computer.
        /// </summary>
		[Parameter()]
        [Credential]
        public PSCredential Credential { get; set; } = PSCredential.Empty;

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            const string cmd = "_GetConnectionString -Version $args[0]";
            const string localTemplate = "{0}";
            // const string remoteTemplate = "Invoke-Command -ScriptBlock {{ {0} }} -ArgumentList $args[0] ";
            // const string remoteComputerTemplate = remoteTemplate + " -Computer $args[1] -Credential $args[2]";
            // const string remoteSessionTemplate = remoteTemplate + " -Session $args[1]";

            var funcCode = File.ReadAllText(Path.Combine(
                MyInvocation.MyCommand.Module.ModuleBase,
                "Private/Admin.ps1"
            ));

            object session = null;
            object credential = null;
            string invokeCmd;

            if (Session != null)
            {
                throw new NotImplementedException("Remote sessions are currently not supported");
                // invokeCmd = string.Format(remoteSessionTemplate, cmd);
                // session = Session;
            }
            else if (!ComputerName.Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotImplementedException("Remote computers are currently not supported");
                // invokeCmd = string.Format(remoteComputerTemplate, cmd);
                // session = ComputerName;
                // credential = Credential;
            }
            else
            {
                invokeCmd = string.Format(localTemplate, cmd);
            }

            string version;
            
            if(Version == 0)
            {
                version = null;
            }
            else
            {
                version = $"{TfsVersionTable.GetMajorVersion(Version)}.0";
            }

            var result = this.InvokeCommand.InvokeScript(funcCode + invokeCmd, true, PipelineResultTypes.None, null, 
                version, session, credential);

            WriteObject(result);
        }
    }
}
