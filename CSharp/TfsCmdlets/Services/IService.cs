using TfsCmdlets.Cmdlets;

namespace TfsCmdlets.Services
{
    public interface IService
    {
        ICmdletServiceProvider Provider { get; set; }

        CmdletBase Cmdlet { get; set; }

        ParameterDictionary Parameters { get; set; }
    }
}