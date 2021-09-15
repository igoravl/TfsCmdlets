using System.Collections.Generic;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface ICommand<T>: ICommand
    {
        IEnumerable<T> Invoke(ParameterDictionary parameters);
    }

    public interface ICommand
    {
        object InvokeCommand(ParameterDictionary parameters);

        string CommandName { get; }
    }
}