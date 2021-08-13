using System.Management.Automation;

namespace TfsCmdlets.Services
{
    internal interface ICommandRuntimeService
    {
        ICommandRuntime CommandRuntime { get; }
    }
}