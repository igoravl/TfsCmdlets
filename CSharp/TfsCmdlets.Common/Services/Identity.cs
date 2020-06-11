using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using TfsIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsCmdletsIdentity = TfsCmdlets.Services.Identity;
using Microsoft.VisualStudio.Services.Identity;
using System.Collections.Generic;

namespace TfsCmdlets.Services
{
    /// <summary>
    /// Encapsulates the platform-specific identity object
    /// </summary>
    public partial class Identity: PSObject
    {
        internal Identity(object obj) : base(obj) { }

        internal string IdentityType => IsContainer? "Group": "User";

        /// <summary>
        /// Converts an Identity object to a TfsIdentity
        /// </summary>
        public static implicit operator TfsIdentity(TfsCmdletsIdentity c) => c?.InnerIdentity;

        /// <summary>
        /// Converts a TfsIdentity object to an Identity 
        /// </summary>
        public static implicit operator Identity(TfsIdentity c) => new TfsCmdletsIdentity(c);

        internal TfsIdentity InnerIdentity => this.BaseObject as TfsIdentity;

        internal Guid Id => InnerIdentity.Id;

        internal string DisplayName => InnerIdentity.DisplayName;

        internal bool IsContainer => InnerIdentity.IsContainer;

        internal IdentityDescriptor Descriptor => InnerIdentity.Descriptor;

        internal string UniqueName => (string)InnerIdentity.Properties["Account"];

        internal IEnumerable<Guid> MemberIds => InnerIdentity.MemberIds;

    }
}
