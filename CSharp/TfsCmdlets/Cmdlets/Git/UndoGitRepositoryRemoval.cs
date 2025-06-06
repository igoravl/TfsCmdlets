using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git
{
    /// <summary>
    /// Restores one or more Git repositories from the Recycle Bin.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, SupportsShouldProcess = true, OutputType = typeof(GitRepository))]
    partial class UndoGitRepositoryRemoval
    {
        /// <summary>
        /// Specifies the repository to be restored. Value can be the name or ID of a Git repository, 
        /// as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git
        /// repository in the Recycle Bin.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Repository { get; set; }
    }

    [CmdletController(typeof(GitRepository))]
    partial class UndoGitRepositoryRemovalController
    {
        [Import]
        private IRestApiService RestApiService { get; }

        protected override IEnumerable Run()
        {
            var repository = Parameters.Get<object>("Repository");
            var repositories = new List<GitRepositoryRef>();

            switch (repository)
            {
                case GitRepository repo:
                    {
                        repositories.Add(new GitRepositoryRef { Id = repo.Id, Name = repo.Name });
                        break;
                    }
                case GitRepositoryRef repoRef:
                    {
                        repositories.Add(repoRef);
                        break;
                    }
                case string s when s.IsGuid():
                    {
                        var guid = new Guid(s);
                        repositories.Add(new GitRepositoryRef { Id = guid });
                        break;
                    }
                case string s:
                    {
                        // For string names, we need to find the deleted repository ID
                        // This requires additional API calls to the recycle bin
                        // For simplicity in this initial implementation, we'll require the user to provide the ID
                        throw new ArgumentException($"Repository name '{s}' specified, but looking up deleted repositories by name requires the repository ID. Please provide the repository ID directly, or use Get-TfsGitRepository with appropriate filters to find the deleted repository object first.");
                    }
                default:
                    {
                        throw new ArgumentException($"Invalid repository '{repository}'");
                    }
            }

            foreach (var repo in repositories)
            {
                if (!PowerShell.ShouldProcess($"[Project: {Project.Name}]/[Repository: {repo.Name ?? repo.Id.ToString()}]", "Restore repository from Recycle Bin")) continue;

                var requestBody = "{ \"deleted\": false }";

                RestApiService.InvokeAsync(
                        Data.GetCollection(),
                        $"/{Project.Name}/_apis/git/repositories/{repo.Id}/recycleBin",
                        "PATCH",
                        requestBody,
                        apiVersion: "7.1")
                    .GetResult($"Error restoring Git repository '{repo.Name ?? repo.Id.ToString()}'");

                // Return the restored repository
                yield return Data.GetItem<GitRepository>(new { Repository = repo.Id, Project = Project });
            }
        }
    }
}