using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Controllers.Git.Item
{
    [CmdletController(typeof(GitItem))]
    partial class GetGitItemController
    {
        [Import]
        private INodeUtil NodeUtil { get; }

        protected override IEnumerable Run()
        {
            var client = GetClient<GitHttpClient>();
            var repo = GetItem<GitRepository>(new { Default = !Has_Repository });

            foreach (var item in Item)
            {
                var itemVersion = ParameterSetName switch
                {
                    "Get by commit SHA" when !Has_Commit => null,
                    "Get by commit SHA" => new GitVersionDescriptor()
                    {
                        VersionType = GitVersionType.Commit,
                        Version = item.ToString()
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

                            yield return client.GetItemAsync(Project.Name, repo.Id, s,
                                    recursionLevel: VersionControlRecursionType.None,
                                    includeContentMetadata: IncludeMetadata,
                                    includeContent: IncludeContent,
                                    versionDescriptor: itemVersion)
                                .GetResult($"Error getting item '{s}' in repository '{repo.Name}'");
                            break;
                        }
                    case string s:
                        {
                            var path = NodeUtil.NormalizeNodePath(s, includeLeadingSeparator: true, separator: '/');
                            var rootPath = path.Substring(0, path.LastIndexOf('/', path.IndexOf('*') - 1));
                            var shouldRecurse = path.Split('/').Count(s => s.IsWildcard()) > 1 || path.Contains("**");

                            Logger.Log($"Retrieving item(s) matching '{path}' under folder '{rootPath}' in repository '{repo.Name}'");

                            var result = client.GetItemsAsync(Project.Name, repo.Id, rootPath,
                                    recursionLevel: shouldRecurse ? VersionControlRecursionType.Full : VersionControlRecursionType.OneLevel,
                                    includeContentMetadata: IncludeMetadata,
                                    includeLinks: true,
                                    versionDescriptor: itemVersion)
                                .GetResult($"Error getting item(s) matching '{path}' under folder '{rootPath}' in repository '{repo.Name}'")
                                .Where(i => i.Path.IsLike(path));

                            if (!IncludeContent)
                            {
                                yield return result;
                                yield break;
                            }

                            Logger.Log("Iterating over repository items to retrieve file contents");

                            foreach (var i in result)
                            {
                                Logger.Log($"Retrieving item '{i.Path}' (including contents) in repository '{repo.Name}'");

                                yield return client.GetItemAsync(Project.Name, repo.Id, i.Path,
                                        recursionLevel: VersionControlRecursionType.None,
                                        includeContentMetadata: IncludeMetadata,
                                        includeContent: IncludeContent,
                                        versionDescriptor: itemVersion)
                                    .GetResult($"Error getting item '{s}' in repository '{repo.Name}'");
                            }

                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or non-existent item '{item}'"));
                            break;
                        }
                }
            }
        }
    }
}