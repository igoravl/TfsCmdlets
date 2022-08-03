using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IRestApiService))]
    public class RestApiServiceImpl : IRestApiService
    {
        private GenericHttpClient _client;
        private ILogger Logger { get; set; }
        public Uri Url => _client.Uri;

        Task<HttpResponseMessage> IRestApiService.InvokeAsync(
            Models.Connection connection,
            string path,
            string method,
            string body,
            string requestContentType,
            string responseContentType,
            Dictionary<string, string> additionalHeaders,
            Dictionary<string, string> queryParameters,
            string apiVersion,
            string serviceHostName)
        {
            return GetClient(connection, serviceHostName)
                .InvokeAsync(new HttpMethod(method), path.TrimStart('/'), body,
                             requestContentType, responseContentType, additionalHeaders, queryParameters,
                             apiVersion);
        }

        Task<T> IRestApiService.InvokeAsync<T>(
            Models.Connection connection,
            string path,
            string method,
            string body,
            string requestContentType,
            string responseContentType,
            Dictionary<string, string> additionalHeaders,
            Dictionary<string, string> queryParameters,
            string apiVersion,
            string serviceHostName)
        {
            return GetClient(connection, serviceHostName)
                .InvokeAsync<T>(new HttpMethod(method), path.TrimStart('/'), body,
                                requestContentType, responseContentType, additionalHeaders, queryParameters,
                                apiVersion);
        }

        Task<OperationReference> IRestApiService.QueueOperationAsync(
            Models.Connection connection,
            string path,
            string method,
            string body,
            string requestContentType,
            string responseContentType,
            Dictionary<string, string> additionalHeaders,
            Dictionary<string, string> queryParameters,
            string apiVersion,
            string serviceHostName)
        {
            return GetClient(connection, serviceHostName)
                .InvokeAsync<OperationReference>(new HttpMethod(method), path.TrimStart('/'), body,
                                                 requestContentType, responseContentType, additionalHeaders,
                                                 queryParameters, apiVersion);
        }

        private GenericHttpClient GetClient(Models.Connection connection, string serviceHostName)
        {
            var conn = connection.InnerObject;
            var host = serviceHostName ?? conn.Uri.Host;

            if (!host.Contains("."))
            {
                Logger.Log($"Converting service prefix {serviceHostName} to {serviceHostName}.dev.azure.com");
                host += ".dev.azure.com";
            }

            Logger.Log($"Using service host {host}");

            var client = conn.GetClient<GenericHttpClient>();
            var uri = (new UriBuilder(client.BaseAddress) { Host = host }).Uri;

            if (client.BaseAddress.Host != uri.Host)
            {
                VssConnection vssConn;
#if NET471_OR_GREATER
                vssConn = conn.GetHiddenField<VssConnection>("m_vssConnection");;
#else
                vssConn = conn;
#endif
                var pipeline = vssConn.GetHiddenField<HttpMessageHandler>("m_pipeline");
                client = new GenericHttpClient(uri, pipeline, false);
                vssConn.CallHiddenMethod("RegisterClientServiceInstance", typeof(GenericHttpClient), client);
            }

            return _client = client;
        }

        public Task<ContributionNodeResponse> QueryContributionNodeAsync(
            Models.Connection connection,
            ContributionNodeQuery query,
            Dictionary<string, string> additionalHeaders,
            Dictionary<string, string> queryParameters,
            string apiVersion,
            string serviceHostName)
        {
            return GetClient(connection, serviceHostName)
                .InvokeAsync<ContributionNodeResponse>(
                    HttpMethod.Post,
                    "_apis/Contribution/HierarchyQuery",
                    query.ToJsonString(), 
                    "application/json",
                    "application/json",
                    additionalHeaders, 
                    queryParameters,
                    apiVersion);
        }

        [ImportingConstructor]
        public RestApiServiceImpl(IDataManager data, ILogger logger)
        {
            Logger = logger;
        }
    }
}