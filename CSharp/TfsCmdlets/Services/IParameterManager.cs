using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Models;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IParameterManager
    {
        ParameterDictionary Get(object overridingParameters = null);
    }
}