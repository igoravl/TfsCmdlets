/*
.SYNOPSIS
Gets the installation path of a given Team Foundation Server component.

.DESCRIPTION
Many times a Team Foundation Server admin needs to retrieve the location where TFS is actually installed. That can be useful, for instance, to locate tools like TfsSecurity or TfsServiceControl. That information is recorded at setup time, in a well-known location in the Windows Registry of the server where TFS is installed.

.PARAMETER ComputerName
The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it"s a remote machine. Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a previously opened PowerShell Remote session can be provided instead.
When omitted, defaults to the local machine where the script is being run

.PARAMETER Component
Indicates the TFS component whose installation path is being searched for. For the main TFS installation directory, use BaseInstallation.
When omitted, defaults to BaseInstallation.

.PARAMETER Version
The TFS version number, in the format "##.#". For TFS 2015, use "14.0"

.PARAMETER Credential
The user credentials to be used to access a remote machine. Those credentials must have the required permission to execute a PowerShell Remote session on that computer and also the permission to access the Windows Registry.

.EXAMPLE
Get-TfsInstallationPath -Version 15.0
Gets the root folder (the BaseInstallationPath) of TFS in the local server where the cmdlet is being run

.EXAMPLE
Get-TfsInstallationPath -Computer SPTFSSRV -Version 14.0 -Component SharepointExtensions -Credentials (Get-Credentials)
Gets the location where the SharePoint Extensions have been installed in the remote server SPTFSSRV, prompting for admin credentials to be used for establishing a PS Remoting session to the server
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    [Cmdlet(VerbsCommon.Get, "InstallationPath")]
    [OutputType(typeof(string))]
    public class GetInstallationPath : BaseCmdlet
    {
        [Parameter]
        [Alias("Session")]
        public object ComputerName { get; set; }

        [Parameter]
        public TfsComponent Component { get; set; } = TfsComponent.BaseInstallation;

		[Parameter(Mandatory = true)]
        [ValidateSet("11.0", "12.0", "14.0", "15.0", "16.0")]
        public string Version { get; set; }

        [Parameter]
        [Credential]
        public PSCredential Credential { get; set; } = PSCredential.Empty;

        /*
	protected override void ProcessRecord()
	{
		scriptBlock = _NewScriptBlock -EntryPoint "_GetInstallationPath" -Dependency "_TestRegistryValue", "_GetRegistryValue"oi

		WriteObject(_InvokeScriptBlock -ScriptBlock scriptBlock -Computer Computer -Credential Credential -ArgumentList Version, Component); return;
	}
}
*/
    }
}