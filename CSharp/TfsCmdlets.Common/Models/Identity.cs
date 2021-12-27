using WebApiIdentity = Microsoft.VisualStudio.Services.Identity.Identity;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the platform-specific identity object
    /// </summary>
    public class Identity : ModelBase<WebApiIdentity>
    {
        public Identity(WebApiIdentity obj)
            : base(obj)
        {
            Id = obj.Id;
            DisplayName = obj.DisplayName;
            IsContainer = obj.IsContainer;
            Descriptor = obj.Descriptor;
            UniqueName = (string)obj.Properties["Account"];
            MemberIds = obj.MemberIds;
            SubjectDescriptor = obj.SubjectDescriptor;
        }

        public string IdentityType => IsContainer ? "Group" : "User";

        public Guid Id { get; private set; }

        public string DisplayName { get; private set; }

        public bool IsContainer { get; private set; }

        public IdentityDescriptor Descriptor { get; private set; }

        public SubjectDescriptor SubjectDescriptor { get; private set; }

        public string UniqueName { get; private set; }

        public IEnumerable<Guid> MemberIds { get; private set; }

        public static Identity FromLegacyIdentity(object identity)
        {
            throw new NotImplementedException();
        }
    }
}
