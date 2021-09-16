using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IParameterManager)), Shared]
    internal class ParameterManagerImpl : IParameterManager
    {
        private IPowerShellService PowerShell { get; set; }

        public ParameterDictionary GetParameters(object overridingParameters = null) 
            => new ParameterDictionary(PowerShell.GetBoundParameters(), overridingParameters);


        [ImportingConstructor]
        public ParameterManagerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
    }
}