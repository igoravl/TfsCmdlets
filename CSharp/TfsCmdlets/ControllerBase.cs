using System;
using System.Collections.Generic;
using System.Linq;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    internal abstract class ControllerBase<T> : DisconnectedControllerBase<T> where T : class
    {
        protected Connection Collection { get; private set; }

        public ControllerBase(
            [InjectConnection(ClientScope.Collection)] Connection collection, 
            ILogger logger, 
            IParameterManager parameterManager, 
            IPowerShellService powerShell)
             : base(logger, parameterManager, powerShell)
        {
            Collection = collection;
        }
    }
}