/*
.SYNOPSIS
Gets information about a configuration server.

.PARAMETER Server
Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/] 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS.nnTo connect to a Team Foundation Server instance by using its name, it must have been previously registered.

.PARAMETER Current
Returns the configuration server specified in the last call to Connect-TfsConfigurationServer (i.e. the "current" configuration server)

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.INPUTS
Microsoft.TeamFoundation.Client.TfsConfigurationServer
System.String
System.Uri
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    [Cmdlet(VerbsLifecycle.Start, "IdentitySync", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]

    public class StartIdentitySync
    {
        /*
        Param
        (
            [Parameter(Position=0,ValueFromPipeline=true)]
            [object] 
            Server,

            [Parameter()]
            public SwitchParameter Wait { get; set; }

            [Parameter()]
            public object Credential { get; set; }

        protected override void ProcessRecord()
        {
            srv = Get-TfsConfigurationServer -Server Server -Credential Credential

            if(srv.Count != 1)
            {
                throw new Exception($"Invalid or non-existent configuration server {Server}")
            }

            if(! ShouldProcess(srv.Url, "Start identity sync"))
            {
                return
            }

            jobSvc = srv.GetService([type]"Microsoft.TeamFoundation.Framework.Client.ITeamFoundationJobService")
            syncJobId = [guid]"544dd581-f72a-45a9-8de0-8cd3a5f29dfe"
            syncJobDef = jobSvc.QueryJobs() | Where-Object { _.JobId == syncJobId }

            if (! syncJobDef)
            {
                throw new Exception($"Could not find Periodic Identity Synchronization job definition (id {syncJobId}). Unable to start synchronization process.")
            }

            this.Log($"Queuing job "{{syncJobDef}.Name}" with high priority now");

            success = ([bool] jobSvc.QueueJobNow(syncJobDef, true))

            if (! success)
            {
                throw new Exception("Failed to queue synchronization job")
            }

            if(Wait.IsPresent)
            {
                do
                {
                    this.Log("Waiting for the job to complete");
                    Start-Sleep -Seconds 5

                    status = jobSvc.QueryLatestJobHistory(syncJobId)
                    this.Log($"Current job status: {{status}.Result}");
                } while(status.Result == "None")

                WriteObject(result); return;
            }
        }
    }
    */
    }
}