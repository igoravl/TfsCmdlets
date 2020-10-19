using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using TfsCmdlets.Extensions;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Admin.Registry
{
    /// <summary>
    ///   Gets the value of a given Team Foundation Server registry entry.
    /// </summary>
    /// <example>
    ///   <code>Get-TfsRegistryValue -Path '/Service/Integration/Settings/EmailEnabled'</code>
    ///   <para>Gets the current value of the 'EmailEnabled' key in the TFS Registry</para>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "TfsRegistryValue")]
    [OutputType(typeof(object))]
    [DesktopOnly]
    public class GetInstallationPath : CmdletBase
    {
        /// <summary>
        /// Indicates the TFS component whose installation path is being searched for. 
        /// For the main TFS installation directory, use BaseInstallation. When omitted, 
        /// defaults to BaseInstallation.
        /// </summary>
        [Parameter(Position=0, Mandatory=true)]
        public string Path { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ParameterSetName = "Get by collection")]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_SERVER
        /// </summary>
        [Parameter(ParameterSetName = "Get by server", Mandatory = true)]
        public object Server { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
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

                if(clientType == null)
                {
                    // Try to guess the assembly name from the class name
                    var assemblyName = TypeName.Substring(0, TypeName.LastIndexOf("."));

                    try
                    {
                        var asm = Assembly.LoadFrom(
                            $"{this.MyInvocation.MyCommand.Module.ModuleBase}/Lib/{EnvironmentUtil.PSEdition}/{assemblyName}.dll");
                        clientType = asm.GetType(TypeName);
                    }
                    finally {}
                }
            }

            if (clientType == null) throw new Exception($"Invalid or non-existent type '{TypeName}'. " +
                "If the type name is correct, either provide the assembly name in the form of 'Type,Assembly' " +
                "or check whether its assembly has been previously loaded");

            WriteObject(provider.GetClientFromType(clientType));
        }
    }
}