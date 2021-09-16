using System;
using System.Collections.Generic;
using System.Composition;
using System.Management.Automation;
using System.Net;
using System.Security;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Commands.Credential
{
    [Command]
    internal class NewCredential : CommandBase<VssCredentials>
    {

        public override IEnumerable<VssCredentials> Invoke(ParameterDictionary parameters)
        {
            return Data.GetItems<VssCredentials>(parameters);
        }

        [ImportingConstructor]
        public NewCredential(IConnectionManager connections, IDataManager data, ILogger logger)
        : base(connections, data, logger)
        {
        }
    }
}