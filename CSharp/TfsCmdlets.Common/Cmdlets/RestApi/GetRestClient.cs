using System;
using System.Linq;
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

            Type clientType;

            if(TypeName.Contains(","))
            {
                // Fully qualified - use Type.GetType
                clientType = Type.GetType(TypeName);
            }
            else
            {
                // Not fully qualified - iterate over all loaded assemblies (may not find if assembly's not loaded yet)
                clientType = AppDomain.CurrentDomain.GetAssemblies().Select(asm => asm.GetType(TypeName)).FirstOrDefault();
            }

            if (clientType == null) throw new Exception($"Invalid or non-existent type '{TypeName}'. " +
                "If the type name is correct, either provide the assembly name in the form of 'Type,Assembly' " +
                "or check whether its assembly has been previously loaded");

            WriteObject(provider.GetClientFromType(clientType));
        }
    }
}