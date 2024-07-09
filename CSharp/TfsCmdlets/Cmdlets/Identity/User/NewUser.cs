using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;
using Newtonsoft.Json;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Identity.User
{
    /// <summary>
    /// Creates a new user in the account and optionally adds them to projects.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true, OutputType = typeof(AccountEntitlement))]
    partial class NewUser
    {
        /// <summary>
        /// Specifies the ID of the user to be created. For Azure DevOps Services, use the
        /// user's email address. For TFS, use the user's domain alias.
        /// </summary>
        [Parameter(Position = 0)]
        [Alias("UserId")]
        public string User { get; set; }

        /// <summary>
        /// Specifies the friendly (display) name of the user to be created.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Specifies the license type for the user to be created.
        /// 
        /// When omitted, defaults to Stakeholder.
        /// </summary>
        [Parameter]
        public AccountLicenseType License { get; set; } = AccountLicenseType.Stakeholder;

        /// <summary>
        /// Specifies the projects to which the user should be added. 
        /// Can be supplied as an array of project names or a hashtable/dictionary with project names as keys and group names as values.
        /// When provided as an array, the user is added to the group specified in the DefaultGroup parameter.
        /// 
        /// When omitted, the user is not added to any projects.
        /// </summary>
        [Parameter]
        public object Projects { get; set; }

        /// <summary>
        /// Specifies the default group to which the user should be added, when applicable.
        /// 
        /// When omitted, defaults to Contributor.
        /// </summary>
        [Parameter]
        public GroupEntitlementType DefaultGroup { get; set; } = GroupEntitlementType.Contributor;
    }
}

namespace TfsCmdlets.Controllers.Identity.User
{
    [CmdletController(typeof(AccountEntitlement))]
    partial class NewUserController
    {
        [Import]
        private IRestApiService RestApi { get; set; }

        protected override IEnumerable Run()
        {
            const string licenseSource = "account";
            const string api = "POST https://vsaex.dev.azure.com/{organization}/_apis/userentitlements?api-version=7.1";

            var defaultGroup = $"project{DefaultGroup}";

            string licenseType = License switch
            {
                AccountLicenseType.Basic => "express",
                AccountLicenseType.BasicTestPlans => "advanced",
                AccountLicenseType.VisualStudio => "eligible",
                _ => "stakeholder"
            };

            IDictionary<string, string> projects = Projects switch
            {
                string p => new Dictionary<string, string> { { p, defaultGroup } },
                IDictionary dict => dict.Cast<DictionaryEntry>().ToDictionary(kv => kv.Key.ToString(), kv => kv.Value.ToString()),
                ICollection c => c.Cast<string>().ToDictionary(p => p, g => defaultGroup),
                _ => null
            };

            if (!PowerShell.ShouldProcess(Collection, $"Create user '{User}' with license type '{License}' and the project entitlements \n{string.Join(";", projects.Select(kv => $"{kv.Key}={kv.Value}"))}"))
            {
                yield break;
            }

            var parsedProjects = new Dictionary<Guid, string>();

            foreach (var kv in projects)
            {
                var key = kv.Key switch
                {
                    string s when s.IsGuid() => Guid.Parse(s),
                    string s => Data.GetItem<WebApiTeamProject>(new { Project = s }).Id,
                    _ => throw new Exception("Invalid project name")
                };

                parsedProjects.Add(key, kv.Value);
            }

            var entitlements = new ProjectEntitlements(parsedProjects);

            var body = new
            {
                accessLevel = new
                {
                    licensingSource = licenseSource,
                    accountLicenseType = licenseType
                },
                user = new
                {
                    principalName = User,
                    displayName = DisplayName,
                    subjectKind = "user"
                },
                projectEntitlements = entitlements
            }.ToJsonString();

            dynamic result = RestApi.InvokeTemplateAsync(Collection, api, body)
                .GetResult("Error creating user")
                .ToJsonObject() ?? throw new Exception("Unknown error creating user");

            if (!((bool) result.isSuccess))
            {
                string errorMessage = result.operationResult.errors[0].value;
                Logger.LogError($"Error creating user. {errorMessage}");
                yield break;
            }

            if (Passthru)
            {
                yield return Data.GetItem<AccountEntitlement>(new{User});
            }
        }

        private class ProjectEntitlements : List<ProjectEntitlement>
        {
            public ProjectEntitlements(IDictionary<Guid, string> entitlements)
            {

                foreach (var kv in entitlements)
                {
                    var projectId = kv.Key;

                    Add(new ProjectEntitlement
                    {
                        Group = new GroupRef { GroupType = kv.Value },
                        Project = new ProjectRef { Id = projectId }
                    });
                }
            }
        }

        private class ProjectEntitlement
        {
            [JsonProperty("group")]
            public GroupRef Group { get; set; }
            [JsonProperty("projectRef")]
            public ProjectRef Project { get; set; }
        }

        private class GroupRef
        {
            [JsonProperty("groupType")]
            public string GroupType { get; set; }
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        private class ProjectRef
        {
            [JsonProperty("id")]
            public Guid Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
