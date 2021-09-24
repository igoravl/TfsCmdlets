using System;
using System.Collections.Generic;
using System.Composition;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IRestApiService))]
    internal class RestApiServiceImpl : IRestApiService
    {
        public ILogger Logger { get; private set; }

        Task<HttpResponseMessage> IRestApiService.InvokeAsync(Models.Connection connection, string path,
            string method,
            string body,
            string requestContentType,
            string responseContentType,
            Dictionary<string, string> additionalHeaders,
            Dictionary<string, string> queryParameters,
            string apiVersion,
            string serviceHostName)
        {
            var conn = connection.InnerConnection;
            path = path.TrimStart('/');

            if (!string.IsNullOrEmpty(serviceHostName))
            {
                if (!serviceHostName.Contains("."))
                {
                    Logger.Log($"Converting service prefix {serviceHostName} to {serviceHostName}.dev.azure.com");
                    serviceHostName += ".dev.azure.com";
                }

                Logger.Log($"Using service host {serviceHostName}");
                GenericHttpClient.UseHost(serviceHostName);
            }

            var client = conn.GetClient<GenericHttpClient>();

            var task = client.InvokeAsync(new HttpMethod(method), path, body,
                requestContentType, responseContentType, additionalHeaders, queryParameters,
                apiVersion);

            return task;
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
            var conn = connection.InnerConnection;
            path = path.TrimStart('/');

            if (!string.IsNullOrEmpty(serviceHostName))
            {
                if (!serviceHostName.Contains("."))
                {
                    Logger.Log($"Converting service prefix {serviceHostName} to {serviceHostName}.dev.azure.com");
                    serviceHostName += ".dev.azure.com";
                }

                Logger.Log($"Using service host {serviceHostName}");
                GenericHttpClient.UseHost(serviceHostName);
            }

            var client = conn.GetClient<GenericHttpClient>();

            var task = client.InvokeAsync<T>(new HttpMethod(method), path, body,
                requestContentType, responseContentType, additionalHeaders, queryParameters,
                apiVersion);

            return task;
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
            return ((IRestApiService)this).InvokeAsync<OperationReference>(
                connection,
                path,
                method,
                body,
                requestContentType,
                responseContentType,
                additionalHeaders,
                queryParameters,
                apiVersion,
                serviceHostName);
        }
 
        [ImportingConstructor]
        public RestApiServiceImpl(IDataManager data, ILogger logger)
        {
            Logger = logger;
        }
   }
}