using Microsoft.VisualStudio.Services.Licensing;
using TfsCmdlets.Cmdlets.Identity;
using Microsoft.VisualStudio.Services.Licensing.Client;

namespace TfsCmdlets.Cmdlets.Identity.User
{
    /// <summary>
    /// Gets information about one or more Azure DevOps users.
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
        public string FriendlyName { get; set; }

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
        /// When provided as an array, the user is added to the Contributors group of each project.
        /// 
        /// When omitted, the user is not added to any projects.
        /// </summary>
        [Parameter]
        public object Projects { get; set; }
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

            string licenseType = License switch {
                AccountLicenseType.Basic => "express",
                AccountLicenseType.BasicTestPlans => "advanced",
                AccountLicenseType.VisualStudio => "eligible",
                _ => "stakeholder"
            };

            var projectEntitlements = Projects switch {
                IDictionary entitlements => new ProjectEntitlements(entitlements),
                IEnumerable<string> projects => new ProjectEntitlements(projects, "Contributors"),
                _ => throw new ArgumentException("Projects must be a hashtable or an array of strings")
            };

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
                projectEntitlements = projectEntitlements
            }.ToJsonString();

            if(!PowerShell.ShouldProcess(Collection, $"Create user '{User}' with license type '{License}' and the following project entitlements:\n{projectEntitlements}"))
            {
                yield break;
            }

            var result = RestApi.InvokeAsync(Collection, api, body).GetResult("Error creating user");

            if(result != null && Passthru)
            {
                yield return result;
            }
        }

        private class ProjectEntitlements{
            private Dictionary<string, string> _entitlements;

            public ProjectEntitlements(IEnumerable<string> entitlements, string defaultGroup){
                _entitlements = entitlements.ToDictionary(e => e, e => defaultGroup);
            }
            public ProjectEntitlements(IDictionary entitlements){
                _entitlements = entitlements.Cast<DictionaryEntry>().ToDictionary(e => e.Key.ToString(), e => e.Value.ToString());
            }

            public override string ToString(){
                return string.Join("\n", _entitlements.Select(e => $"{e.Key}: {e.Value}"));
            }
        }
    }
}