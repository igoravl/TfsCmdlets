using TfsCmdlets.Cmdlets;

namespace TfsCmdlets.Services
{
    internal interface IService
    {
        ICmdletServiceProvider Provider { get; set; }

        BaseCmdlet Cmdlet { get; set; }

        ParameterDictionary Parameters { get; set; }
    }
}