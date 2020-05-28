/*
.SYNOPSIS
    Short description
.DESCRIPTION
    Long description
.EXAMPLE
    PS C:> <example usage>
    Explanation of what the example does
.INPUTS
    Inputs (if any)
.OUTPUTS
    Output (if any)
.NOTES
    General notes
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity
{
    [Cmdlet(VerbsCommon.Get, "Identity")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public class GetIdentity: BaseCmdlet
    {
/*
        [Parameter(Position=0,Mandatory=true,ParameterSetName="Get Identity")]
        public object Identity { get; set; }

        [Parameter(ParameterSetName="Get Identity")]
        public SwitchParameter QueryMembership { get; set; }

        [Parameter(Mandatory=true,ParameterSetName="Get current user")]
        public SwitchParameter Current { get; set; }

        [Parameter(ValueFromPipeline=true)]
        public object Server { get; set; }

    protected override void ProcessRecord()
    {
        if(ParameterSetName == "Get current user")
        {
            srv = Get-TfsConfigurationServer -Current

            if(! srv)
            {
                return
            }

            Identity = srv.AuthorizedIdentity.TeamFoundationId
        }
        else
        {
            if (Identity is Microsoft.VisualStudio.Services.Identity.Identity) { this.Log("Input item is of type Microsoft.VisualStudio.Services.Identity.Identity; returning input item immediately, without further processing."; WriteObject(Identity }); return;);
            _GetServer
        }
        
        client = Get-TfsRestClient "Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient" -Server srv

        if(QueryMembership.IsPresent)
        {
            qm = Microsoft.VisualStudio.Services.Identity.QueryMembership.Direct
        }
        else
        {
            qm = Microsoft.VisualStudio.Services.Identity.QueryMembership.None
        }

        if(_TestGuid Identity)
        {
            this.Log($"Finding identity with ID [{Identity}] and QueryMembership=qm");
            task = client.ReadIdentityAsync([guid]Identity); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving information from identity [{Identity}]" task.Exception.InnerExceptions })
        }
        else
        {
            this.Log($"Finding identity with account name [{Identity}] and QueryMembership=qm");
            task = client.ReadIdentitiesAsync(Microsoft.VisualStudio.Services.Identity.IdentitySearchFilter.AccountName, [string]Identity, "None", qm); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving information from identity [{Identity}]" task.Exception.InnerExceptions })
        }

        WriteObject(result); return;
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
