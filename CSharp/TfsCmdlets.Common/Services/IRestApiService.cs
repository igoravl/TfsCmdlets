using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface IRestApiService
    {
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