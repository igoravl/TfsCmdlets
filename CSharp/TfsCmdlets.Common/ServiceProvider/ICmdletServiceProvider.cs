using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.ServiceProvider
{
    internal interface ICmdletServiceProvider
    {
        T GetService<T>(Cmdlet cmdlet) where T: IService;

        T GetOne<T>(Cmdlet cmdlet, object userState = null);

        IEnumerable<T> GetMany<T>(Cmdlet cmdlet, object userState = null);
    }
}
