using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    /// <summary>
    /// Encapsulates the platform-specific identity object
    /// </summary>
    public partial class Identity: PSObject
    {
        internal Identity(object obj) : base(obj) { }

        internal string IdentityType => IsContainer? "Group": "User";
    }
}
