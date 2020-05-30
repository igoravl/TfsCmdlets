using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    /// <para type="synopsis">
    /// Gets the installation path of a given Team Foundation Server component.
    /// </para>
    /// <para type="description">
    /// Many times a Team Foundation Server admin needs to retrieve the location where 
    /// TFS is actually installed. That can be useful, for instance, to locate tools like 
    /// TfsSecurity or TfsServiceControl. That information is recorded at setup time, 
    /// in a well-known location in the Windows Registry of the server where TFS is installed.
    /// </para>
    /// <example>
    ///   <code>PS&gt; Get-TfsInstallationPath -Version 15.0</code>
    ///   <para>Gets the root folder (the BaseInstallationPath) of TFS in the local server where the cmdlet is being run</para>
    /// </example>
    /// <example>
    ///   <code>PS&gt; Get-TfsInstallationPath -Computer SPTFSSRV -Version 14.0 -Component SharepointExtensions -Credentials (Get-Credentials)</code>
    ///   <para>Gets the location where the SharePoint Extensions have been installed in the remote 
    ///         server SPTFSSRV, prompting for admin credentials to be used for establishing a 
    ///         PS Remoting session to the server</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "InstallationPath")]
    [OutputType(typeof(string))]
    [WindowsOnly]
    public partial class GetInstallationPath : BaseCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// The machine name of the server where the TFS component is installed. 
        /// It must be properly configured for PowerShell Remoting in case it"s a remote machine. 
        /// Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a 
        /// previously opened PowerShell Remote session can be provided instead. 
        /// When omitted, defaults to the local machine where the script is being run
        /// </para>
        /// </summary>
        [Parameter]
        [Alias("Session")]
        public object ComputerName { get; set; }

        /// <summary>
        /// <para type="description">
        /// Indicates the TFS component whose installation path is being searched for. 
        /// For the main TFS installation directory, use BaseInstallation. When omitted, 
        /// defaults to BaseInstallation.
        /// </para>
        /// </summary>
        [Parameter]
        public TfsComponent Component { get; set; } = TfsComponent.BaseInstallation;

        /// <summary>
        /// <para type="description">
        /// The TFS version number, represented by the year in its name. For e.g. TFS 2015, use "2015".
        /// When ommitted, will default to the newest installed version of TFS / Azure DevOps Server
        /// </para>
        /// </summary>
		[Parameter()]
        public string Version { get; set; }

        /// <summary>
        /// <para type="description">
        /// The user credentials to be used to access a remote machine. Those credentials must have 
        /// the required permission to execute a PowerShell Remote session on that computer and also 
        /// the permission to access the Windows Registry.
        /// </para>
        /// </summary>
        [Parameter]
        [Credential]
        public PSCredential Credential { get; set; } = PSCredential.Empty;

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