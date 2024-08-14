using System.Management.Automation;
using Microsoft.TeamFoundation.Policy.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Policy
{
    /// <summary>
    /// Gets the Git branch policy configuration of the given Git branches.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, OutputType = typeof(PolicyConfiguration))]
    partial class GetGitBranchPolicy
    {
        /// <summary>
        /// Specifies the policy type of the branch policy to return. Wildcards are supported. 
        /// When omitted, all branch policies defined for the given branch are returned.
        /// </summary>
        [Parameter(Position = 0)]
        public object PolicyType { get; set; } = "*";

        /// <summary>
        /// Specifies the name of the branch to query for branch policies.
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1)]
        [Alias("RefName")]
        public object Branch { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(PolicyConfiguration), Client=typeof(IGitHttpClient))]
    partial class GetGitBranchPolicyController 
    {
        protected override IEnumerable Run()
        {
            var b = GetItem<GitBranchStats>();
            var url = new Uri(b.Commit.Url);
            var repoId = url.Segments[url.Segments.Length - 3].TrimEnd('/');
            var projectId = url.Segments[url.Segments.Length - 7].TrimEnd('/');
            var repo = GetItem<GitRepository>(new{Repository = repoId, Project = projectId});

            var branch = $"refs/heads/{b.Name}";

            Logger.Log($"Getting branch policies in repository '{repo.Name}'");

            foreach (var policyType in PolicyType)
            {
                Guid policyTypeId = Guid.Empty;
                string pattern = null;

                switch (policyType)
                {
                    case PolicyType pt:
                        {
                            policyTypeId = pt.Id;
                            break;
                        }
                    case string s when s.IsGuid():
                        {
                            policyTypeId = new Guid(s);
                            break;
                        }
                    case string s:
                        {
                            pattern = s;
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid policy type '{policyType}'"));
                            break;
                        }
                }

                bool getById = (policyTypeId != Guid.Empty);

                var policies = (getById ?
                        Client.GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch, policyTypeId): 
                        Client.GetPolicyConfigurationsAsync(repo.ProjectReference.Name, repo.Id, branch))
                    .GetResult($"Error getting policy definitions from branch {branch} in repository {repo.Name}")
                    .PolicyConfigurations;

                foreach (var pol in policies) 
                {
                    if(!getById && !pol.Type.DisplayName.IsLike(pattern)) continue;

                    yield return pol;
                }
            }
        }
    }
}
