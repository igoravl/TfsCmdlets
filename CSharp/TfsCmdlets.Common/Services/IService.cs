using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IService
    {
        ICmdletServiceProvider Provider { get; set;  }

        BaseCmdlet Cmdlet { get; set; }
    }

    public interface IDataService<out T>: IService where T: class
    {
        T GetInstanceOf(ParameterDictionary overriddenParameters = null, object filter = null);

        IEnumerable<T> GetCollectionOf(ParameterDictionary overriddenParameters = null, object userState = null);
    }
}