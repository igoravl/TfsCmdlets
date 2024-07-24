using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using System.Reflection;

namespace TfsCmdlets.Cmdlets.RestApi
{
    /// <summary>
    /// Gets an Azure DevOps HTTP Client object instance.
    /// </summary>
    /// <remarks>
    /// Connection objects (Microsoft.VisualStudio.Services.Client.VssConnection in PowerShell Core, 
    /// Microsoft.TeamFoundation.Client.TfsTeamProjectCollection in Windows PowerShell) provide access to 
    /// many HTTP client objects such as Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient 
    /// that wrap many of the REST APIs exposed by Azure DevOps. Those clients inherit the authentication 
    /// information supplied by their parent connection object and can be used as a more convenient mechanism 
    /// to issue API calls.
    /// </remarks>
    [TfsCmdlet(CmdletScope.Collection, DefaultParameterSetName = "Get by collection", OutputType = typeof(VssHttpClientBase))]
    partial class GetRestClient
    {
        /// <summary>
        /// Specifies the full type name (optionally including its assembly name) of the HTTP Client 
        /// class to return.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Type")]
        public string TypeName { get; set; }
    }

    [CmdletController(typeof(VssHttpClientBase))]
    partial class GetRestClientController
    {
        protected override IEnumerable Run()
        {
            var parameterSetName = Parameters.Get<string>("ParameterSetName");
            var typeName = Parameters.Get<string>("TypeName");
            var provider = (parameterSetName == "Get by collection") ? Data.GetCollection() : Data.GetServer();

            Type clientType;

            if (typeName.Contains(","))
            {
                // Fully qualified - use Type.GetType
                clientType = Type.GetType(typeName);
            }
            else
            {
                // Not fully qualified - iterate over all loaded assemblies (may not find if assembly's not loaded yet)
                clientType = AppDomain.CurrentDomain.GetAssemblies().Select(asm => asm.GetType(typeName)).FirstOrDefault();

                if (clientType == null)
                {
                    // Try to guess the assembly name from the class name
                    var assemblyName = typeName.Substring(0, typeName.LastIndexOf("."));

                    try
                    {
                        var libPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        var asm = Assembly.LoadFrom(Path.Combine(libPath, $"{assemblyName}.dll"));
                        clientType = asm.GetType(typeName);
                    }
                    finally { }
                }
            }

            if (clientType == null) PowerShell.WriteError(new Exception($"Invalid or non-existent type '{typeName}'. " +
                "If the type name is correct, either provide the assembly name in the form of 'Type,Assembly' " +
                "or check whether its assembly has been previously loaded"));

            yield return (VssHttpClientBase) provider.GetClientFromType(clientType);
        }
    }
}