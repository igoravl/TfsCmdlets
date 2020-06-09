using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity
{
    [OutputType(typeof(Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity))]
    partial class GetIdentity
    {
        private Type IdentityType => typeof(Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity);
    }
}