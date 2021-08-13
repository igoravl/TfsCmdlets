using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.HttpClient
{
    /// <summary>
    /// Custom HTTP Client to handle team administrator management
    /// </summary>
    public class TeamAdminHttpClient : GenericHttpClient
    {
        /// <summary>
        /// Adds an administrator to a team
        /// </summary>
        public IEnumerable<TeamAdmin> AddTeamAdmin(Guid projectId, Guid teamId, Guid userId)
        {
            return AddTeamAdmin(projectId.ToString(), teamId, userId);
        }

        /// <summary>
        /// Adds an administrator to a team
        /// </summary>
        public IEnumerable<TeamAdmin> AddTeamAdmin(string project, Guid teamId, Guid userId)
        {
            var result = Post<AddTeamAdminRequestData, TeamAdmins>(
                $"/{project}/_api/_identity/AddTeamAdmins",
                new AddTeamAdminRequestData
                {
                    Team = teamId,
                    NewUsers = "[]",
                    ExistingUsers = $"[\"{userId}\"]"
                });

            return result.Admins;
        }

        /// <summary>
        /// Removes an administrator from a team
        /// </summary>
        public bool RemoveTeamAdmin(Guid project, Guid teamId, Guid userId)
        {
            return RemoveTeamAdmin(project.ToString(), teamId, userId);
        }

        /// <summary>
        /// Removes an administrator from a team
        /// </summary>
        public bool RemoveTeamAdmin(string project, Guid teamId, Guid userId)
        {
            var result = PostForm<RemoveTeamAdminResult>(
                $"{project}/_api/_identity/RemoveTeamAdmin",
                new Dictionary<string, string>
                {
                    ["teamId"] = teamId.ToString(),
                    ["tfidToRemove"] = userId.ToString()
                },
                true,
                $"/{project}/_settings/teams?teamId={teamId}",
                null,
                new Dictionary<string, string> {["__v"] = "5"},
                "application/json"
            );

            return result.Success;
        }

        #region Constructors and fields

        /// <summary>
        /// Creates a new instance of the TeamAdminHttpClient class
        /// </summary>
        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials) : base(baseUrl, credentials)
        {
        }

        /// <summary>
        /// Creates a new instance of the TeamAdminHttpClient class
        /// </summary>
        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(
            baseUrl, credentials, settings)
        {
        }

        /// <summary>
        /// Creates a new instance of the TeamAdminHttpClient class
        /// </summary>
        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(
            baseUrl, credentials, handlers)
        {
        }

        /// <summary>
        /// Creates a new instance of the TeamAdminHttpClient class
        /// </summary>
        public TeamAdminHttpClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(baseUrl,
            pipeline, disposeHandler)
        {
        }

        /// <summary>
        /// Creates a new instance of the TeamAdminHttpClient class
        /// </summary>
        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings,
            params DelegatingHandler[] handlers) : base(baseUrl, credentials, settings, handlers)
        {
        }

        #endregion
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