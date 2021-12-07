using System;
using System.Management.Automation;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using Microsoft.VisualStudio.Services.Identity;
using System.Collections.Generic;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the platform-specific identity object
    /// </summary>
    public class Identity : PSObject
    {
        internal Identity(WebApiIdentity obj) : base(obj) 
        {
            Id = obj.Id;
            DisplayName = obj.DisplayName;
            IsContainer = obj.IsContainer;
            Descriptor = obj.Descriptor;
            UniqueName = (string) obj.Properties["Account"];
            MemberIds = obj.MemberIds;
        }

#if NET471_OR_GREATER
        internal Identity(Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity obj) : base(obj)
        {
            Id = obj.TeamFoundationId;
            DisplayName = obj.DisplayName;
            IsContainer = obj.IsContainer;
            UniqueName = (string)obj.UniqueName;
            Descriptor = new Microsoft.VisualStudio.Services.Identity.IdentityDescriptor(
                obj.Descriptor.IdentityType, obj.Descriptor.Identifier);
        }
#endif

        internal string IdentityType => IsContainer ? "Group" : "User";

        internal Guid Id { get; private set; }

        internal string DisplayName { get; private set; }

        internal bool IsContainer { get; private set; }

        internal IdentityDescriptor Descriptor { get; private set; }

        internal string UniqueName { get; private set; }

        internal IEnumerable<Guid> MemberIds { get; private set; }

    }
}
