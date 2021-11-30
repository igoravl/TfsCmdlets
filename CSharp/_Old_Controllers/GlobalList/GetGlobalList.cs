using System;
using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.GlobalList
{
    [CmdletController]
    internal class GetGlobalList : ControllerBase<Models.GlobalList>
    {
        public override IEnumerable<Models.GlobalList> Invoke()
        {
            throw new NotImplementedException();
        }

        [ImportingConstructor]
        public GetGlobalList(ICurrentConnections currentConnections, IPowerShellService powerShell, IDataManager data, ILogger logger)
          : base(powerShell, data, logger)
        {
        }
    }
}