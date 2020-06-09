using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity
{
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    partial class GetIdentity
    {
        private Type IdentityType => typeof(Microsoft.VisualStudio.Services.Identity.Identity);
    }
}