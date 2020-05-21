using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.RestApi
{
    [Cmdlet(VerbsCommon.Get, "RestClient", DefaultParameterSetName="Get by collection")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase))]
    public class GetRestClient: PSCmdlet
    {
        [Parameter(Mandatory=true, Position=0)]
        [Alias("Type")]
        public string TypeName { get; set; }

        [Parameter(ParameterSetName="Get by collection", Mandatory=true)]
        public object Collection { get; set; }

        [Parameter(ParameterSetName="Get by server", Mandatory=true)]
        public object Server { get; set; }

    protected override void ProcessRecord()
    {
        VssConnection provider;

        if(ParameterSetName == "Get by collection")
        {
            provider = this.GetCollection();
        }
        else
        {
            provider = this.GetServer();
        }

        WriteObject(provider.GetClient(Type.GetType(TypeName))); 
    }
}
}
}
