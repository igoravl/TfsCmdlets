using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Operations;
using Microsoft.VisualStudio.Services.Search.WebApi.Contracts;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.HttpClients;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(IRestApiService))]
    public class RestApiServiceImpl : IRestApiService
    {
        private IGenericHttpClient Client {get; set;}
        
        private ILogger Logger { get; set; }
        
        public Uri Url => Client.GetUri();

        Task<HttpResponseMessage> IRestApiService.InvokeTemplateAsync(
            Models.Connection connection,
            string apiTemplate,
            string body,
            string method,
            IDictionary queryParameters,
            string requestContentType,
            string responseContentType,
            Dictionary<string, string> additionalHeaders,
            string apiVersion,
            Func<WebApiTeamProject> project,
            Func<Models.Team> team,
            string customServiceHost)
        {
            // Checks whether the HTTP method is included in the API template

            if (apiTemplate.Contains(" "))
            {
                var tokens = apiTemplate.Split(' ');

                if (IsHttpMethod(tokens[0]))
                {
                    method = tokens[0];
                    apiTemplate = apiTemplate.Substring(tokens[0].Length + 1).Trim();
                }
            }

            apiTemplate = apiTemplate.Replace("https://{instance}/{connection}/", "http://tfs/");

            var queryParams = new Dictionary<string, string>();

            if (queryParameters != null)
            {
                foreach (var key in queryParameters.Keys)
                {
                    queryParams.Add(key.ToString(), queryParameters[key]?.ToString());
                }
            }

            if (Uri.TryCreate(apiTemplate, UriKind.Absolute, out var uri))
            {
                if (string.IsNullOrEmpty(customServiceHost) && !uri.Host.Equals(connection.Uri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    customServiceHost = uri.Host;
                }

                apiTemplate = uri.AbsolutePath.Replace("/%7Borganization%7D/", "");

                var connectionName = connection.DisplayName.Trim('/');

                if (apiTemplate.StartsWith($"/{connectionName}"))
                {
                    apiTemplate = apiTemplate.Substring(connectionName.Length + 1);
                }

                var query = uri.ParseQueryString();

                if (query["api-version"] != null)
                {
                    apiVersion = query["api-version"];
                    query.Remove("api-version");
                }

                foreach (var key in query.AllKeys)
                {
                    if ($"{{{key}}}".Equals(query[key], StringComparison.OrdinalIgnoreCase) && !queryParams.ContainsKey(key))
                    {
                        Logger.LogWarn($"Parameter '{key}' found in the URL query string, but missing from queryParameters argument. To keep this parameter, add it to the queryParameters argument.");
                        continue;
                    }

                    if (queryParams.ContainsKey(key)) continue;

                    queryParams.Add(key, query[key]);
                }
            }

            if (apiTemplate.Contains("%7Bproject%7D") || apiTemplate.Contains("%7BprojectId%7D"))
            {
                var tp = project();
                apiTemplate = apiTemplate.Replace($"%7Bproject%7D", tp.Name);
                apiTemplate = apiTemplate.Replace($"%7BprojectId%7D", tp.Id.ToString());
            }

            if (apiTemplate.Contains("%7Bteam%7D") || apiTemplate.Contains("%7BteamId%7D"))
            {
                var t = team();
                apiTemplate = apiTemplate.Replace($"%7Bteam%7D", t.Name);
                apiTemplate = apiTemplate.Replace($"%7BteamId%7D", t.Id.ToString());
            }

            var keysToRemove = new List<string>();

            foreach (var kvp in queryParams.Where(kvp => apiTemplate.Contains($"%7B{kvp.Key}%7D")))
            {
                keysToRemove.Add(kvp.Key);
                apiTemplate = apiTemplate.Replace($"%7B{kvp.Key}%7D", kvp.Value);
            }

            keysToRemove.ForEach(k => queryParams.Remove(k));

            Logger.Log($"apiTemplate '{apiTemplate}', version '{apiVersion}'");

            string host = null;

            if (connection.IsHosted)
            {
                if (!string.IsNullOrEmpty(customServiceHost))
                {
                    host = customServiceHost;
                }
                else if (!connection.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase))
                {
                    host = connection.Uri.Host;
                }
            }

            return ((IRestApiService)this).InvokeAsync(
                connection, apiTemplate, method, body, requestContentType, responseContentType, additionalHeaders, queryParams, apiVersion, host);
        }

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

        private IGenericHttpClient GetClient(Models.Connection connection, string serviceHostName)
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

            return Client = client;
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

        private bool IsHttpMethod(string method)
        {
            const string methods = "|GET|POST|PUT|PATCH|DELETE|";
            return methods.Contains($"|{method.ToUpperInvariant()}|");
        }

        [ImportingConstructor]
        public RestApiServiceImpl(ILogger logger, IGenericHttpClient client)
        {
            Logger = logger;
            Client = client;
        }
    }
}