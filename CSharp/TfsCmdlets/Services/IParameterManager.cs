using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IParameterManager: IEnumerable<KeyValuePair<string,object>>
    {
        void Initialize(Cmdlet cmdlet);

        T Get<T>(string name, T defaultValue = default);

        void Remove(string name);

        bool HasParameter(string parameter);

        object this[string name] { get; set; }

        void PushContext(object overridingParameters);

        void PopContext();
    }
}