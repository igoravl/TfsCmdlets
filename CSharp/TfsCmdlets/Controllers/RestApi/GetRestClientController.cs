using System.Reflection;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Controllers.RestApi
{
    [CmdletController(typeof(VssHttpClientBase))]
    partial class GetRestClientController
    {
        public override IEnumerable<VssHttpClientBase> Invoke()
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
