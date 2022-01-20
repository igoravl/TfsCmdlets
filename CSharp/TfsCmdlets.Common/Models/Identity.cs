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

        public Identity(WebApiIdentity obj, WebApiTeam team)
            : this(obj)
        {
            SetTeam(team);
        }

        public string IdentityType => IsContainer ? "Group" : "User";

        public Guid Id { get; private set; }

        public string DisplayName { get; private set; }

        public bool IsContainer { get; private set; }

        public IdentityDescriptor Descriptor { get; private set; }

        public SubjectDescriptor SubjectDescriptor { get; private set; }

        public string UniqueName { get; private set; }

        public IEnumerable<Guid> MemberIds { get; private set; }

        public Guid TeamId => (Guid)InnerObject.Properties[nameof(TeamId)];

        public Guid ProjectId => (Guid)InnerObject.Properties[nameof(ProjectId)];

        public string TeamName => (string)InnerObject.Properties[nameof(TeamName)];

        public string ProjectName => (string)InnerObject.Properties[nameof(ProjectName)];

        internal void SetTeam(WebApiTeam team)
        {
            AddProperty(nameof(TeamId), team.Id);
            AddProperty(nameof(ProjectId), team.ProjectId);
            AddProperty(nameof(TeamName), team.Name);
            AddProperty(nameof(ProjectName), team.ProjectName);
        }
    }
}
