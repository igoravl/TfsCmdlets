using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    public interface IDataCommand<T>: ICommand
    {
        IEnumerable<T> Invoke(ParameterDictionary parameters);
    }

    public interface ICommand
    {
        object InvokeCommand(ParameterDictionary parameters);

        string Verb {get;}

        string Noun {get;}

        string CommandName {get;}

        Type DataType {get;}
    }
}