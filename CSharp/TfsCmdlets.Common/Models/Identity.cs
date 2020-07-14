using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;
using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using Microsoft.VisualStudio.Services.Identity;
using System.Collections.Generic;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the platform-specific identity object
    /// </summary>
    public partial class Identity : PSObject
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

        internal string IdentityType => IsContainer ? "Group" : "User";

        internal Guid Id { get; private set; }

        internal string DisplayName { get; private set; }

        internal bool IsContainer { get; private set; }

        internal IdentityDescriptor Descriptor { get; private set; }

        internal string UniqueName { get; private set; }

        internal IEnumerable<Guid> MemberIds { get; private set; }

    }
}
