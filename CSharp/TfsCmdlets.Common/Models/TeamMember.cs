namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents a member of a Team Foundation / Azure DevOps Team
    /// </summary>
    public class TeamMember : Identity
    {
        public TeamMember(WebApiIdentity obj, WebApiTeam team) : base(obj, team)
        {
        }
    }
}