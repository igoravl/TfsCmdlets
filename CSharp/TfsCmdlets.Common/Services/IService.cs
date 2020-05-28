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
    }

    internal interface IDataService<out T>: IService
    {
        ParameterDictionary Parameters { get; set; }

        T GetOne(ParameterDictionary overriddenParameters, object filter = null);

        IEnumerable<T> GetMany(ParameterDictionary overriddenParameters, object userState = null);
    }
}