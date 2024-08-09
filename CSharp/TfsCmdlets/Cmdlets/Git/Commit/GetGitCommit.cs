using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Commit
{
    /// <summary>
    /// Gets information from one or more Git commits in a remote repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, DefaultParameterSetName = "Search commits", OutputType = typeof(GitCommitRef))]
    partial class GetGitCommit
    {
        /// <summary>
        /// Specifies the hash (SHA) of the commit to return.
        /// </summary>
        [Parameter(ParameterSetName = "Get by commit SHA", Mandatory = true, Position = 0)]
        public object Commit { get; set; }

        /// <summary>
        /// Specifies the tag name of the commit to return.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag", Mandatory = true)]
        public string Tag { get; set; }

        /// <summary>
        /// Specifies the branch name of the commit to return.
        /// </summary>
        [Parameter(ParameterSetName = "Get by branch", Mandatory = true)]
        public string Branch { get; set; }

        /// <summary>
        /// Limits the search to commits authored by this user.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string Author { get; set; }

        /// <summary>
        /// Limits the search to commits committed by this user.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string Committer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public GitVersionDescriptor CompareVersion { get; set; }

        /// <summary>
        /// Specifies the "commit-ish" to start the search from.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string FromCommit { get; set; }

        /// <summary>
        /// Specifies the date and time of the commit to start the search from.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Limits the search to commits that affect this path.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string ItemPath { get; set; }

        /// <summary>
        /// Specifies the "commit-ish" to end the search at.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public string ToCommit { get; set; }

        /// <summary>
        /// Specifies the date and time of the commit to end the search at.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Sorts the results from oldest to newest commit.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public SwitchParameter ShowOldestCommitsFirst { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public int Skip { get; set; }

        /// <summary>
        /// Specifies the maximum number of commits to return.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public int Top { get; set; }

        /// <summary>
        /// Prevents deleted items from being included in the results.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag")]
        [Parameter(ParameterSetName = "Get by branch")]
        [Parameter(ParameterSetName = "Search commits")]
        public SwitchParameter ExcludeDeletes { get; set; }

        /// <summary>
        /// Includes links to related resources (such as work items) in the results.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeLinks { get; set; }

        /// <summary>
        /// Includes push data in the results.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludePushData { get; set; }

        /// <summary>
        /// Includes the user's image URL in the results.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeUserImageUrl { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitCommitRef), Client = typeof(IGitHttpClient))]
    partial class GetGitCommitController
    {
        protected override IEnumerable Run()
        {
            var repository = GetItem<GitRepository>(new { Repository = Has_Repository ? Repository : Project.Name });
            string commitSha;

            if (Has_Commit)
            {
                // Get commits by SHA

                foreach (var commit in Commit)
                {
                    switch (commit)
                    {
                        case GitCommitRef gcr:
                            {
                                yield return gcr;
                                yield break;
                            }
                        case string s:
                            {
                                commitSha = s;
                                break;
                            }
                        default:
                            {
                                Logger.LogError(new ArgumentException($"Invalid or non-existent commit '{commit}'"));
                                continue;
                            }
                    }

                    yield return Client.GetCommitAsync(repository.ProjectReference.Id.ToString(), commitSha, repository.Id.ToString())
                        .GetResult($"Error getting commit '{commitSha}' in repository '{repository.Name}'");
                }
                yield break;
            }

            GitVersionDescriptor itemVersion;

            if (Has_Tag)
            {
                itemVersion = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Tag,
                    Version = Tag
                };
            }
            else if (Has_Branch)
            {
                itemVersion = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Branch,
                    Version = Branch
                };
            }
            else
            {
                if (repository.DefaultBranch == null)
                {
                    Logger.LogError($"Repository '{repository.Name}' has no default branch set. Please specify a different repository or branch.");
                    yield break;
                }

                itemVersion = new GitVersionDescriptor()
                {
                    VersionType = GitVersionType.Branch,
                    Version = repository.DefaultBranch.Substring(repository.DefaultBranch.LastIndexOf('/') + 1)
                };
            }

            // Search for commits

            var criteria = new GitQueryCommitsCriteria()
            {
                Author = Author,
                Committer = Committer,
                CompareVersion = CompareVersion,
                ExcludeDeletes = ExcludeDeletes,
                FromCommitId = FromCommit,
                ItemPath = ItemPath,
                ItemVersion = itemVersion,
                IncludeLinks = IncludeLinks,
                IncludePushData = IncludePushData,
                IncludeUserImageUrl = IncludeUserImageUrl,
                ShowOldestCommitsFirst = ShowOldestCommitsFirst,
                ToCommitId = ToCommit,
                Skip = Skip,
                Top = Top == 0 ? null : Top,
                FromDate = Has_FromDate ? this.FromDate.ToString("yyyy-MM-ddTHH:mm:ssK") : null,
                ToDate = Has_ToDate ? this.ToDate.ToString("yyyy-MM-ddTHH:mm:ssK") : null,
            };

            var result = Client.GetCommitsBatchAsync(criteria, repository.Id)
                .GetResult("Error getting Git commits");

            foreach (var commit in result)
            {
                yield return commit;
            }
        }
    }
}