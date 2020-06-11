using TfsIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using TfsCmdletsIdentity = TfsCmdlets.Services.Identity;
using System.Collections.Generic;
using System;

namespace TfsCmdlets.Services
{
    partial class Identity
    {
        /// <summary>
        /// Converts an Identity object to a TfsIdentity
        /// </summary>
        public static implicit operator TfsIdentity(TfsCmdletsIdentity c) => c?.InnerIdentity;

        /// <summary>
        /// Converts a TfsIdentity object to an Identity 
        /// </summary>
        public static implicit operator Identity(TfsIdentity c) => new TfsCmdletsIdentity(c);

        internal TfsIdentity InnerIdentity => this.BaseObject as TfsIdentity;

        internal string DisplayName => InnerIdentity.DisplayName;

        internal bool IsContainer => InnerIdentity.IsContainer;

        internal object Descriptor => InnerIdentity.Descriptor;

        internal string UniqueName => (string)InnerIdentity.Properties["Account"];

        internal IEnumerable<Guid> MemberIds => InnerIdentity.MemberIds;
    }
}