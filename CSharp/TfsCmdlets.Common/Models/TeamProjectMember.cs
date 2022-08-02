using System.Runtime.Serialization;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents a member of a Team Foundation / Azure DevOps Team
    /// </summary>
    [DataContract]
    public class TeamProjectMember
    {
        public string TeamProject { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "mail")]
        public string Mail { get; set; }
    }

    [DataContract]
    public class TeamProjectMemberCollection
    {
        [DataMember(Name = "isCurrentUserAdmin")]
        public bool IsCurrentUserAdmin { get; set; }
        
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "hasMore")]
        public bool HasMore { get; set; }
        
        [DataMember(Name = "members")]
        public TeamProjectMember[] Members { get; set; } 
    }
}