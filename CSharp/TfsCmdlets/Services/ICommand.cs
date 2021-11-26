using System;
using System.Collections.Generic;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Services
{
    public interface ITypedController<T>: IController
    {
        IEnumerable<T> Invoke(ParameterDictionary parameters);
    }

    public interface IController
    {
        object InvokeCommand(ParameterDictionary parameters);

        string Verb {get;}

        string Noun {get;}

        string CommandName {get;}

        Type DataType {get;}
    }
}