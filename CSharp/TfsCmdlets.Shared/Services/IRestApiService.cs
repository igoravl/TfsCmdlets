using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface IRestApiService
    {
        /// <summary>
        /// Invokes an Azure DevOps REST API based on a template URL.
        /// </summary>
        /// <description>
        /// This method is used to invoke an Azure DevOps REST API based on the template URL format used in the Azure DevOps REST API documentation.
        /// </description>
        /// <param name="connection">The connection information.</param>
        /// <param name="apiTemplate">The API template.</param>
        /// <param name="body">The request body (optional).</param>
        /// <param name="method">The HTTP method (default is "GET").</param>
        /// <param name="queryParameters">The query parameters (optional).</param>
        /// <param name="requestContentType">The request content type (default is "application/json").</param>
        /// <param name="responseContentType">The response content type (default is "application/json").</param>
        /// <param name="additionalHeaders">Additional headers to include in the request (optional).</param>
        /// <param name="apiVersion">The API version (default is "4.1").</param>
        /// <param name="project">A delegate that returns the TeamProject, used only when there is a {project}/{projectId} parameter in the template URL (optional).</param>
        /// <param name="team">A function that returns the Team, used only when there is a {team}/{teamId} parameter in the template URL (optional).</param>
        /// <param name="customServiceHost">The custom service host (optional).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<HttpResponseMessage> InvokeTemplateAsync(
            Models.Connection connection,
            string apiTemplate,
            string body = null,
            string method = "GET",
            IDictionary queryParameters = null,
            string requestContentType = "application/json",
            string responseContentType = "application/json",
            Dictionary<string, string> additionalHeaders = null,
            string apiVersion = "4.1",
            Func<WebApiTeamProject> project = null,
            Func<Models.Team> team = null,
            string customServiceHost = null);

        /// <summary>
        /// Invokes an Azure DevOps REST API endpoint asynchronously.
        /// </summary>
        /// <param name="connection">The connection information.</param>
        /// <param name="path">The path of the API endpoint.</param>
        /// <param name="method">The HTTP method to use (default is "GET").</param>
        /// <param name="body">The request body (optional).</param>
        /// <param name="requestContentType">The content type of the request (default is "application/json").</param>
        /// <param name="responseContentType">The expected content type of the response (default is "application/json").</param>
        /// <param name="additionalHeaders">Additional headers to include in the request (optional).</param>
        /// <param name="queryParameters">Query parameters to include in the request (optional).</param>
        /// <param name="apiVersion">The version of the API to use (default is "4.1").</param>
        /// <param name="serviceHostName">The host name of the service (optional).</param>
        /// <returns>A task representing the asynchronous operation, which returns the HTTP response message.</returns>
        Task<HttpResponseMessage> InvokeAsync(
            Models.Connection connection,
            string path,
            string method = "GET",
            string body = null,
            string requestContentType = "application/json",
            string responseContentType = "application/json",
            Dictionary<string, string> additionalHeaders = null,
            Dictionary<string, string> queryParameters = null,
            string apiVersion = "4.1",
            string serviceHostName = null);

        /// <summary>
        /// Invokes an Azure DevOps REST API asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the response object.</typeparam>
        /// <param name="connection">The connection information.</param>
        /// <param name="path">The path of the API endpoint.</param>
        /// <param name="method">The HTTP method to use (default is "GET").</param>
        /// <param name="body">The request body (default is null).</param>
        /// <param name="requestContentType">The content type of the request (default is "application/json").</param>
        /// <param name="responseContentType">The content type of the response (default is "application/json").</param>
        /// <param name="additionalHeaders">Additional headers to include in the request (default is null).</param>
        /// <param name="queryParameters">Query parameters to include in the request (default is null).</param>
        /// <param name="apiVersion">The version of the API to use (default is "4.1").</param>
        /// <param name="serviceHostName">The host name of the service (default is null).</param>
        /// <returns>A task representing the asynchronous operation, which returns the response object.</returns>
        Task<T> InvokeAsync<T>(
            Models.Connection connection,
            string path,
            string method = "GET",
            string body = null,
            string requestContentType = "application/json",
            string responseContentType = "application/json",
            Dictionary<string, string> additionalHeaders = null,
            Dictionary<string, string> queryParameters = null,
            string apiVersion = "4.1",
            string serviceHostName = null);

        Task<OperationReference> QueueOperationAsync(
            Models.Connection connection,
            string path,
            string method = "GET",
            string body = null,
            string requestContentType = "application/json",
            string responseContentType = "application/json",
            Dictionary<string, string> additionalHeaders = null,
            Dictionary<string, string> queryParameters = null,
            string apiVersion = "4.1",
            string serviceHostName = null);

        Task<ContributionNodeResponse> QueryContributionNodeAsync(
            Models.Connection connection,
            ContributionNodeQuery query,
            Dictionary<string, string> additionalHeaders = null,
            Dictionary<string, string> queryParameters = null,
            string apiVersion = "6.1",
            string serviceHostName = null);

        Uri Url { get; }
    }
}