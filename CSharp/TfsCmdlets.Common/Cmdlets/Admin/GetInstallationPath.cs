using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Gets the installation path of a given Team Foundation Server component.
    /// </summary>
    /// <remarks>
    ///  Many times a Team Foundation Server admin needs to retrieve the location where 
    ///  TFS is actually installed. That can be useful, for instance, to locate tools like 
    ///  TfsSecurity or TfsServiceControl. That information is recorded at setup time, 
    ///  in a well-known location in the Windows Registry of the server where TFS is installed.
    /// </remarks>
    /// <example>
    ///   <code>Get-TfsInstallationPath -Version 2017</code>
    ///   <para>Gets the root folder (the BaseInstallationPath) of TFS in the local server where the cmdlet is being run</para>
    /// </example>
    /// <example>
    ///   <code>Get-TfsInstallationPath -Computer SPTFSSRV -Version 2015 -Component SharepointExtensions -Credentials (Get-Credentials)</code>
    ///   <para>Gets the location where the SharePoint Extensions have been installed in the remote 
    ///         server SPTFSSRV, prompting for admin credentials to be used for establishing a 
    ///         PS Remoting session to the server</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "InstallationPath", DefaultParameterSetName = "Use computer name")]
    [OutputType(typeof(string))]
    [WindowsOnly]
    public class GetInstallationPath : BaseCmdlet
    {
        /// <summary>
        /// The machine name of the server where the TFS component is installed. 
        /// It must be properly configured for PowerShell Remoting in case it's a remote machine. 
        /// Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a 
        /// previously opened PowerShell Remote session can be provided instead. 
        /// When omitted, defaults to the local machine where the script is being run
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
        /// Indicates the TFS component whose installation path is being searched for. 
        /// For the main TFS installation directory, use BaseInstallation. When omitted, 
        /// defaults to BaseInstallation.
        /// </summary>
        [Parameter]
        public TfsComponent Component { get; set; } = TfsComponent.BaseInstallation;

        /// <summary>
        /// The TFS version number, represented by the year in its name. For e.g. TFS 2015, use "2015".
        /// When ommitted, will default to the newest installed version of TFS / Azure DevOps Server
        /// </summary>
		[Parameter()]
        [ValidateSet("2005", "2008", "2010", "2012", "2013", "2015", "2017", "2018", "2019", "2020")]
        public int Version { get; set; }

        /// <summary>
        /// The user credentials to be used to access a remote machine. Those credentials must have 
        /// the required permission to execute a PowerShell Remote session on that computer and also 
        /// the permission to access the Windows Registry.
        /// </summary>
        [Parameter]
        [Credential]
        public PSCredential Credential { get; set; } = PSCredential.Empty;

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            const string cmd = "_GetInstallationPath -Version $args[0] -Component $args[1]";
            const string localTemplate = "{0}";
            const string remoteTemplate = "Invoke-Command -ScriptBlock {{ {0} }} -ArgumentList $args[0] $args[1] ";
            // const string remoteComputerTemplate = remoteTemplate + " -Computer $args[2] -Credential $args[3]";
            // const string remoteSessionTemplate = remoteTemplate + " -Session $args[2]";

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
                version, Component.ToString(), session, credential);

            WriteObject(result);
        }
    }
}