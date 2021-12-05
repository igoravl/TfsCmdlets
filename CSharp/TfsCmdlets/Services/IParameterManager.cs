using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IParameterManager: IDictionary<string,object>
    {
        void Initialize(Cmdlet cmdlet);

        T Get<T>(string name, T defaultValue = default);

        bool HasParameter(string parameter);

        void PushContext(object overridingParameters);

        void PopContext();

        void Reset();
    }
}