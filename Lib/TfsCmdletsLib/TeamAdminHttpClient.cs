using System;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets
{
    public class TeamAdminHttpClient : VssHttpClientBase
    {
        private static readonly Guid LocationId = new Guid("A46086B1-2E4A-413C-A40C-C01B0E4CB1F8");

        private ApiResourceLocationCollection _ResourceLocations;

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials) : base(baseUrl, credentials)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(baseUrl, credentials, settings)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(baseUrl, credentials, handlers)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(baseUrl, pipeline, disposeHandler)
        {
        }

        public TeamAdminHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings, params DelegatingHandler[] handlers) : base(baseUrl, credentials, settings, handlers)
        {
        }

        public ApiResourceLocationCollection ResourceLocations
        {
            get
            {
                _ResourceLocations = (ApiResourceLocationCollection)typeof(VssHttpClientBase).InvokeMember("m_resourceLocations", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);

                return _ResourceLocations;
            }
        }

        public async Task<TeamAdmins> AddTeamAdministratorAsync(Guid projectId, Guid teamId, Guid userId)
        {
            return await AddTeamAdministratorAsync(projectId.ToString(), teamId, userId);
        }

        public async Task<TeamAdmins> AddTeamAdministratorAsync(string project, Guid teamId, Guid userId)
        {
            EnsureApiLocation();

            return await this.PostAsync<AddTeamAdminRequestData, TeamAdmins>(
                new AddTeamAdminRequestData { Team = teamId, NewUsers = "[]", ExistingUsers = $"[\"{userId}\"]" },
                LocationId,
                new { project });
        }

        private async void EnsureApiLocation()
        {
            if (await GetResourceLocationAsync(LocationId) != null)
            {
                return;
            }

            ResourceLocations.AddResourceLocation(new ApiResourceLocation
            {
                Id = LocationId, // Identifier
                Area = "identity", //ServiceType,
                ResourceName = "addteamadmin", //DisplayName,
                RouteTemplate = "/{project}/_api/_identity/AddTeamAdmins", //RelativePath,
                ResourceVersion = 5, //ResourceVersion,
                MinVersion = new Version("1.0"), //MinVersion,
                MaxVersion = new Version("5.2"), //MaxVersion,
                ReleasedVersion = new Version("1.0")
            });
        }
    }

    [DataContract]
    public class TeamAdmins
    {
        [DataMember(Name = "admins")]
        public Admin[] Admins { get; set; }
    }

    public class Admin
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
    }

    [DataContract]
    public class AddTeamAdminRequestData
    {
        [DataMember(Name = "teamId")]
        public Guid Team { get; set; }

        [DataMember(Name = "newUsersJson")]
        public string NewUsers { get; set; }

        [DataMember(Name = "existingUsersJson")]
        public string ExistingUsers { get; set; }
    }
}
