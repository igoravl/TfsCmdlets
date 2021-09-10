using System.Management.Automation;

namespace TfsCmdlets.Services
{
    public interface ICommandRuntimeService
    {
        ICommandRuntime CommandRuntime { get; }
    }
}