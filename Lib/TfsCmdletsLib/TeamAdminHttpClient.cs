using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets
{
    public class TeamAdminHttpClient : GenericHttpClient
    {
        public IEnumerable<TeamAdmin> AddTeamAdmin(Guid projectId, Guid teamId, Guid userId)
        {
            return AddTeamAdmin(projectId.ToString(), teamId, userId);
        }

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

        public bool RemoveTeamAdmin(Guid project, Guid teamId, Guid userId)
        {
            return RemoveTeamAdmin(project.ToString(), teamId, userId);
        }

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

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials) : base(baseUrl, credentials)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(
            baseUrl, credentials, settings)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(
            baseUrl, credentials, handlers)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(baseUrl,
            pipeline, disposeHandler)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings,
            params DelegatingHandler[] handlers) : base(baseUrl, credentials, settings, handlers)
        {
        }

        #endregion
    }

    #region Data Classes

    [DataContract]
    public class TeamAdmins
    {
        [DataMember(Name = "admins")] public TeamAdmin[] Admins { get; set; }
    }

    public class TeamAdmin
    {
        public string IdentityType { get; set; }
        public string FriendlyDisplayName { get; set; }
        public string DisplayName { get; set; }
        public string SubHeader { get; set; }
        public string TeamFoundationId { get; set; }
        public string EntityId { get; set; }
        public object[] Errors { get; set; }
        public object[] Warnings { get; set; }
        public string Domain { get; set; }
        public string AccountName { get; set; }
        public bool IsWindowsUser { get; set; }
        public string MailAddress { get; set; }

        public override string ToString()
        {
            return $"{FriendlyDisplayName} ({AccountName})";
        }
    }

    [DataContract]
    public class AddTeamAdminRequestData
    {
        [DataMember(Name = "teamId")] public Guid Team { get; set; }

        [DataMember(Name = "newUsersJson")] public string NewUsers { get; set; }

        [DataMember(Name = "existingUsersJson")]
        public string ExistingUsers { get; set; }
    }

    [DataContract]
    public class RemoveTeamAdminResult
    {
        [DataMember(Name = "success")] public bool Success { get; set; }
    }

    #endregion
}