using System.Runtime.Serialization;

namespace TfsCmdlets.HttpClients
{
    [HttpClient(typeof(TeamAdminHttpClient))]
    partial interface ITeamAdminHttpClient
    {
    }

    #region Data Classes

    /// <summary>
    /// Represents a collection of team administrators
    /// </summary>
    [DataContract]
    public class TeamAdmins
    {
        /// <summary>
        /// Collection of team administrators
        /// </summary>
        [DataMember(Name = "admins")] 
        public TeamAdmin[] Admins { get; set; }
    }

    /// <summary>
    /// Represents a team administrator
    /// </summary>
    public class TeamAdmin
    {
        /// <summary>
        /// Identity Type
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// Friendly Display Name
        /// </summary>
        public string FriendlyDisplayName { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Sub-header
        /// </summary>
        public string SubHeader { get; set; }

        /// <summary>
        /// Team Foundation Id
        /// </summary>
        public string TeamFoundationId { get; set; }

        /// <summary>
        /// Entity Id
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// List of Errors
        /// </summary>
        public object[] Errors { get; set; }

        /// <summary>
        /// List of Warnings
        /// </summary>
        public object[] Warnings { get; set; }

        /// <summary>
        /// User Domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// User Account Name
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Is Windows User
        /// </summary>
        public bool IsWindowsUser { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string MailAddress { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{FriendlyDisplayName} ({AccountName})";
        }
    }

    /// <summary>
    /// The request body to submit to the "Add Admin" service
    /// </summary>
    [DataContract]
    public class AddTeamAdminRequestData
    {
        /// <summary>
        /// TeamId
        /// </summary>
        [DataMember(Name = "teamId")] 
        public Guid Team { get; set; }

        /// <summary>
        /// List of New Users
        /// </summary>
        [DataMember(Name = "newUsersJson")]
        public string NewUsers { get; set; }

        /// <summary>
        /// List of Existing Users
        /// </summary>
        [DataMember(Name = "existingUsersJson")]
        public string ExistingUsers { get; set; }
    }

    /// <summary>
    /// The request body to submit to the "Remove Admin" service
    /// </summary>
    [DataContract]
    public class RemoveTeamAdminResult
    {
        /// <summary>
        /// Indicates the success of the operation
        /// </summary>
        [DataMember(Name = "success")] 
        public bool Success { get; set; }
    }

    #endregion
}