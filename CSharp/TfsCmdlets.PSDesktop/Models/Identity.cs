using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using System.Collections.Generic;
using Microsoft.TeamFoundation.Framework.Client;

namespace TfsCmdlets.Models
{
    partial class Identity : PSObject
    {
        internal Identity(TeamFoundationIdentity obj) : base(obj)
        {
            Id = obj.TeamFoundationId;
            DisplayName = obj.DisplayName;
            IsContainer = obj.IsContainer;
            UniqueName = (string)obj.UniqueName;
            Descriptor = new Microsoft.VisualStudio.Services.Identity.IdentityDescriptor(
                obj.Descriptor.IdentityType, obj.Descriptor.Identifier);
        }
    }
}