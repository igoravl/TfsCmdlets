using System;
using System.Management.Automation;
using System.Security;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Cmdlets.Credential
{
    /// <summary>
    /// Provides credentials to use when you connect to a Team Foundation Server 
    /// or Azure DevOps organization.
    /// </summary>
    [TfsCmdlet(CmdletScope.None, DefaultParameterSetName = "Cached credentials", OutputType = typeof(VssCredentials))]
    partial class NewCredential 
    {
        /// <summary>
        /// Specifies the URL of the server, collection or organization to connect to.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public Uri Url { get; set; }

        protected override string CommandName => "GetCredential";
        
        protected override bool ReturnsValue => true;
    }
}