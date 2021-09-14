using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Exports(typeof(IParameterManager))]
    internal class ParameterManager : IParameterManager
    {
        private IPowerShellService PowerShell { get; set; }

        public ParameterManager(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }

        public ParameterDictionary Get(object overridingParameters = null) 
            => new ParameterDictionary(PowerShell.GetBoundParameters(), overridingParameters);
    }
}