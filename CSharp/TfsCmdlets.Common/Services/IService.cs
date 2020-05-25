using System.Management.Automation;
using TfsCmdlets.ServiceProvider;

namespace TfsCmdlets.Services
{
    internal interface IService
    {
        ICmdletServiceProvider Provider { get; set; }

        Cmdlet Cmdlet { get; set; }
    }

    internal interface IService<out T>: IService
    {
        T Get(object userState = null);
    }
}