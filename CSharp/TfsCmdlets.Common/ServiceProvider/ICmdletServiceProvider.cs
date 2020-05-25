using System;
using System.Management.Automation;

namespace TfsCmdlets.ServiceProvider
{
    internal interface ICmdletServiceProvider
    {
        IService GetService(Cmdlet cmdlet, Type serviceType);

        IService<T> GetService<T>(Cmdlet cmdlet);

        T Get<T>(Cmdlet cmdlet, object userState = null);
    }
}
