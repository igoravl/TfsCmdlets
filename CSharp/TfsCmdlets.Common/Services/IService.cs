using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.ServiceProvider;

namespace TfsCmdlets.Services
{
    internal interface IService
    {
        ICmdletServiceProvider Provider { get; set;  }

        Cmdlet Cmdlet { get; set; }

        ParameterDictionary Parameters { get; set; }
    }

    internal interface IService<out T>: IService
    {
        T GetOne(object filter = null);

        IEnumerable<T> GetMany(object userState = null);
    }
}