using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    internal interface IService
    {
        ICmdletServiceProvider Provider { get; set;  }

        BaseCmdlet Cmdlet { get; set; }
    }

    internal interface IDataService<out T>: IService where T: class
    {
        T GetItem(object overriddenParameters = null);

        IEnumerable<T> GetItems(object overriddenParameters = null);
    }
}