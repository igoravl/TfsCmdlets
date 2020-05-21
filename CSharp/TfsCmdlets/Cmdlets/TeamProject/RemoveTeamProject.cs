/*
.SYNOPSIS
Deletes one or more team projects. 

.DESCRIPTION

.PARAMETER Project
Specifies the name of a Team Project. Wildcards are supported.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.PARAMETER Credential
Specifies a user account that has permission to perform this action. The default is the cached credential of the user under which the PowerShell process is being run - in most cases that corresponds to the user currently logged in. To provide a user name and password, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its WriteObject(to this argument. For more information, refer to https://msdn.microsoft.com/en-us/library/microsoft.teamfoundation.client.tfsclientcredentials.aspx); return;

.INPUTS
Microsoft.TeamFoundation.Client.TfsTeamProjectCollection
System.String
System.Uri

.NOTES
As with most cmdlets in the TfsCmdlets module, this cmdlet requires a TfsTeamProjectCollection object to be provided via the -Collection argument. If absent, it will default to the connection opened by Connect-TfsTeamProjectCollection.

*/

using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts;

namespace TfsCmdlets.Cmdlets.TeamProject
{
    [Cmdlet(VerbsCommon.Remove, "TeamProject", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveTeamProject
    {
        /*
            [Parameter(Position=0,ValueFromPipeline=true)]
        [SupportsWildcards()]
        [object] 
        Project,

        [Parameter()]
        public object Collection { get; set; }

        [Parameter()]
        public SwitchParameter Hard { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        protected override void BeginProcessing()
        {
# _ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.WorkItemTracking.Client"
        }

        protected override void ProcessRecord()
        {
            tps = Get - TfsTeamProject - Project Project - Collection Collection
    
        if (!tps)
            {
                return
        }

            foreach (tp in tps)
            {
                tpc = tp.TeamProjectCollection
                client = Get - TfsRestClient "Microsoft.TeamFoundation.Core.WebApi.ProjectHttpClient" - Collection tpc
    
            if (ShouldProcess(tp.Name, "Delete team project"))
                {
                    if ((!Hard.IsPresent) || (Force.IsPresent || (ShouldContinue("The team project deletion is IRREVERSIBLE and may cause DATA LOSS. Are you sure you want to proceed?"))))
                    {
                        method = (&{ if (Hard.IsPresent) { "Hard"} else { "Soft"} })

                    _Log $"{method}-deleting team project {tp.Name}"
    
                    token = client.QueueDeleteProject(tp.Guid, Hard.IsPresent).Result
    
                    if (!token)
                        {
                            throw new Exception($"Error queueing team project deletion: {{client}.LastResponseContext.Exception.Message}")
                    }

# Wait for the operation to complete

                        client = Get - TfsRestClient "Microsoft.VisualStudio.Services.Operations.OperationsHttpClient" - Collection tpc

                           opsToken = operationsClient.GetOperation(token.Id).Result
    

                    while (
                        (opsToken.Status != [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Succeeded) - and
                        (opsToken.Status != [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Failed) &&
                        (opsToken.Status != [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Cancelled))
                        {
                            _Log $"Waiting for the queued operation to finish (current status: {{opsToken}.Status})"

                        Start - Sleep - Seconds 1
                        opsToken = operationsClient.GetOperation(token.Id).Result
                    }

                        if (opsToken.Status != [Microsoft.VisualStudio.Services.Operations.OperationStatus]::Succeeded)
                        {
                            _Log $"Queued operation finished with status {{opsToken}.Status}"
    
                        throw new Exception($"Error deleting team project ${Project}: {{opsToken}.DetailedMessage}")
                        }
                    }
                }
            }
        }
    }
*/
    }
}
