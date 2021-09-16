using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    public interface IDataManager
    {
        T GetItem<T>(object parameters = null);

        IEnumerable<T> GetItems<T>(object parameters = null);

        IEnumerable<T> Invoke<T>(string verb, object parameters = null);
    }
}