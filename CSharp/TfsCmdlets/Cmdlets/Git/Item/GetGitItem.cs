using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Item
{
    /// <summary>
    /// Gets information from one or more items (folders and/or files) in a remote Git repository.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, NoAutoPipeline = true, DefaultParameterSetName = "Get by commit SHA", OutputType = typeof(GitItem))]
    partial class GetGitItem
    {
        /// <summary>
        /// Specifies the path to items (folders and/or files) in the supplied Git repository. Wildcards are supported. 
        /// When omitted, all items in the root of the Git repository are retrieved.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Path")]
        [SupportsWildcards]
        public object Item { get; set; } = "/*";

        /// <summary>
        /// Specifies the hash (SHA) representing the version of the item(s) to retrieve.
        /// </summary>
        [Parameter(ParameterSetName = "Get by commit SHA")]
        public string Commit { get; set; }

        /// <summary>
        /// Specifies the tag representing the version of the item(s) to retrieve.
        /// </summary>
        [Parameter(ParameterSetName = "Get by tag", Mandatory = true)]
        public string Tag { get; set; }

        /// <summary>
        /// Specifies the branch name representing the version of the item(s) to retrieve.
        /// </summary>
        [Parameter(ParameterSetName = "Get by branch", Mandatory = true)]
        public string Branch { get; set; }

        /// <summary>
        /// Returns the content of the item(s) in addition to metadata.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeContent { get; set; }

        /// <summary>
        /// Returns metadata about the item(s)
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeMetadata { get; set; }

        /// <summary>
        /// HELP_PARAM_GIT_REPOSITORY
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitItem), Client=typeof(IGitHttpClient))]
    partial class GetGitItemController
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        protected override IEnumerable Run()
        {
            var repo = GetItem<GitRepository>(new { Default = !Has_Repository });

            if(repo.Size == 0)
            {
                Logger.LogWarn($"Repository '{repo.Name}' is empty.");
                yield break;
            }

            foreach (var item in Item)
            {
                var projectName = repo.ProjectReference.Name;
                var itemVersion = ParameterSetName switch
                {
                    "Get by commit SHA" when !Has_Commit => null,
                    "Get by commit SHA" => new GitVersionDescriptor()
                    {
                        VersionType = GitVersionType.Commit,
                        Version = Commit
                    },
                    "Get by tag" => new GitVersionDescriptor()
                    {
                        VersionType = GitVersionType.Tag,
                        Version = Tag
                    },
                    "Get by branch" => new GitVersionDescriptor()
                    {
                        VersionType = GitVersionType.Branch,
                        Version = Branch
                    },
                    _ => throw new ArgumentException($"Invalid parameter set name '{ParameterSetName}'")
                };

                switch (item)
                {
                    case GitItem i:
                        {
                            yield return i;
                            break;
                        }
                    case string s when !s.IsWildcard():
                        {
                            Logger.Log($"Retrieving item '{s}' in repository '{repo.Name}'");

                            var path = NodeUtil.NormalizeNodePath(s, includeLeadingSeparator: true, separator: '/');

                            yield return new Models.GitItem(Client.GetItemAsync(projectName, repo.Id, s,
                                    recursionLevel: VersionControlRecursionType.None,
                                    includeContentMetadata: IncludeMetadata,
                                    includeContent: IncludeContent,
                                    versionDescriptor: itemVersion)
                                .GetResult($"Error getting item '{s}' in repository '{repo.Name}'"), projectName, repo.Name);
                            break;
                        }
                    case string s:
                        {
                            var path = NodeUtil.NormalizeNodePath(s, includeLeadingSeparator: true, separator: '/');
                            var rootPath = path.Substring(0, path.LastIndexOf('/', path.IndexOf('*') - 1));
                            var shouldRecurse = path.Split('/').Count(s => s.IsWildcard()) > 1 || path.Contains("**");

                            Logger.Log($"Retrieving item(s) matching '{path}' under folder '{rootPath}' in repository '{repo.Name}'");

                            var result = Client.GetItemsAsync(projectName, repo.Id, rootPath,
                                    recursionLevel: shouldRecurse ? VersionControlRecursionType.Full : VersionControlRecursionType.OneLevel,
                                    includeContentMetadata: IncludeMetadata,
                                    includeLinks: true,
                                    versionDescriptor: itemVersion)
                                .GetResult($"Error getting item(s) matching '{path}' under folder '{rootPath}' in repository '{repo.Name}'")
                                .Where(i => i.Path.IsLike(path));

                            if (!IncludeContent)
                            {
                                yield return result.Select(i => new Models.GitItem(i, projectName, repo.Name));
                                yield break;
                            }

                            Logger.Log("Iterating over repository items to retrieve file contents");

                            foreach (var i in result)
                            {
                                Logger.Log($"Retrieving item '{i.Path}' (including contents) in repository '{repo.Name}'");

                                yield return new Models.GitItem(Client.GetItemAsync(projectName, repo.Id, i.Path,
                                        recursionLevel: VersionControlRecursionType.None,
                                        includeContentMetadata: IncludeMetadata,
                                        includeContent: IncludeContent,
                                        versionDescriptor: itemVersion)
                                    .GetResult($"Error getting item '{s}' in repository '{repo.Name}'"), projectName, repo.Name);
                            }
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException($"Invalid or non-existent item '{item}'");
                        }
                }
            }
        }
    }
}