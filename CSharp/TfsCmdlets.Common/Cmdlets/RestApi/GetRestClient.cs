using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.RestApi
{
    [Cmdlet(VerbsCommon.Get, "RestClient", DefaultParameterSetName = "Get by collection")]
    [OutputType(typeof(VssHttpClientBase))]
    public class GetRestClient : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Type")]
        public string TypeName { get; set; }

        [Parameter(ParameterSetName = "Get by collection", Mandatory = true)]
        public object Collection { get; set; }

        [Parameter(ParameterSetName = "Get by server", Mandatory = true)]
        public object Server { get; set; }

        protected override void ProcessRecord()
        {
            var provider = ParameterSetName == "Get by collection" ? this.GetCollection() : this.GetServer();

            WriteObject(provider.GetClientFromType(Type.GetType(TypeName)));
        }
    }
}