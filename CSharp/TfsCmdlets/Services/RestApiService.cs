using System;
using System.Collections;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IRestApiService : IService
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

        Uri Url {get;}
    }

    [Exports(typeof(IRestApiService))]
    internal class RestApiServiceImpl : BaseService, IRestApiService
    {
        private GenericHttpClient _client;

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
            var conn = connection.InnerConnection;
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
                var pipeline = conn.GetHiddenField<HttpMessageHandler>("m_pipeline");
                client = new GenericHttpClient(uri, pipeline, false);

#if NETCOREAPP3_1_OR_GREATER
                conn.CallHiddenMethod("RegisterClientServiceInstance", typeof(GenericHttpClient), client);
#else
                throw new NotImplementedException("RegisterClientServiceInstance is not implemented in PS Desktop");
#endif
            }

            return _client = client;
        }
    }
}