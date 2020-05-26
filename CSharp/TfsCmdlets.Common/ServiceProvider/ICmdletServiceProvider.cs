using System;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.ServiceProvider
{
    internal interface ICmdletServiceProvider
    {
        IService GetService(Cmdlet cmdlet, Type serviceType);

        IService<T> GetService<T>(Cmdlet cmdlet);

        T GetInstanceOf<T>(Cmdlet cmdlet, object userState = null);
    }
}
