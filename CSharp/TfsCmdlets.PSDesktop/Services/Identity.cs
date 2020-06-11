using System;
using System.Linq;
using System.Reflection;
using Microsoft.TeamFoundation.Client;
using TfsIdentity = Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity;
using TfsCmdletsIdentity = TfsCmdlets.Services.Identity;
using System.Collections.Generic;
//Microsoft.VisualStudio.Services.Identity.Identity

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

        internal string UniqueName => InnerIdentity.UniqueName;

        internal IEnumerable<Guid> MemberIds => throw new NotImplementedException(nameof(MemberIds));
    }
}