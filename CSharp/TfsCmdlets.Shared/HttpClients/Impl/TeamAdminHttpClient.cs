using System.Net.Http;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.HttpClients;

/// <summary>
/// Custom HTTP Client to handle team administrator management
/// </summary>
public class TeamAdminHttpClient : GenericHttpClient
{
    /// <summary>
    /// Adds an administrator to a team
    /// </summary>
    public IEnumerable<TeamAdmin> AddTeamAdmin(Guid projectId, Guid teamId, IEnumerable<Guid> userIds)
    {
        return AddTeamAdmin(projectId.ToString(), teamId, userIds);
    }

    /// <summary>
    /// Adds an administrator to a team
    /// </summary>
    public IEnumerable<TeamAdmin> AddTeamAdmin(string project, Guid teamId, IEnumerable<Guid> userIds)
    {
        var result = Post<AddTeamAdminRequestData, TeamAdmins>(
            $"/{project}/_api/_identity/AddTeamAdmins",
            new AddTeamAdminRequestData
            {
                Team = teamId,
                NewUsers = "[]",
                ExistingUsers = $"[{string.Join(", ", userIds.Select(id => "\"" + id + "\""))}]"
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