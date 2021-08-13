using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets.HttpClient
{
    /// <summary>
    /// Custom HTTP Client to handle extended Git repository management
    /// </summary>
    public class GitExtendedHttpClient : GenericHttpClient
    {
        /// <summary>
        /// Enables/disables a Git repository
        /// </summary>
        public void UpdateRepositoryEnabledStatus(Guid projectId, Guid repoId, bool enabled)
        {
            UpdateRepositoryEnabledStatus(projectId.ToString(), repoId, enabled);
        }

        /// <summary>
        /// Enables/disables a Git repository
        /// </summary>
        public void UpdateRepositoryEnabledStatus(string project, Guid repoId, bool enabled)
        {
            PostForm<string>(
                $"{project}/_api/_versioncontrol/UpdateRepositoryOption",
                new Dictionary<string, string>
                {
                    ["repositoryId"] = repoId.ToString(),
                    ["option"] = $"{{'key':'IsDisabled', 'value':{(!enabled).ToString().ToLowerInvariant()}}}"
                },
                true,
                $"/{project}/_settings/repositories?repo={repoId}",
                null,
                new Dictionary<string, string> {
                    ["__v"] = "5",
                    ["repositoryId"] = repoId.ToString()
                }
            );
        }

        #region Constructors and fields

        /// <summary>
        /// Creates a new instance of the GitExtendedHttpClient class
        /// </summary>
        public GitExtendedHttpClient(Uri baseUrl, VssCredentials credentials) : base(baseUrl, credentials)
        {
        }

        /// <summary>
        /// Creates a new instance of the GitExtendedHttpClient class
        /// </summary>
        public GitExtendedHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(
            baseUrl, credentials, settings)
        {
        }

        /// <summary>
        /// Creates a new instance of the GitExtendedHttpClient class
        /// </summary>
        public GitExtendedHttpClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(
            baseUrl, credentials, handlers)
        {
        }

        /// <summary>
        /// Creates a new instance of the GitExtendedHttpClient class
        /// </summary>
        public GitExtendedHttpClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(baseUrl,
            pipeline, disposeHandler)
        {
        }

        /// <summary>
        /// Creates a new instance of the GitExtendedHttpClient class
        /// </summary>
        public GitExtendedHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings,
            params DelegatingHandler[] handlers) : base(baseUrl, credentials, settings, handlers)
        {
        }

        #endregion
    }
}