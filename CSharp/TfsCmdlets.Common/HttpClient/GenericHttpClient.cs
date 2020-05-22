using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.HttpClient
{
    public class GenericHttpClient : VssHttpClientBase
    {
        #region Constructors and fields

        private static string _Host;

        public GenericHttpClient(Uri baseUrl, VssCredentials credentials) : base(SetHost(baseUrl), credentials)
        {
        }

        public GenericHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(SetHost(baseUrl), credentials, settings)
        {
        }

        public GenericHttpClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(SetHost(baseUrl), credentials, handlers)
        {
        }

        public GenericHttpClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(SetHost(baseUrl), pipeline, disposeHandler)
        {
        }

        public GenericHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings, params DelegatingHandler[] handlers) : base(SetHost(baseUrl), credentials, settings, handlers)
        {
        }

        #endregion

        public Uri Uri { get; private set; }

        public static void UseHost(string host)
        {
            _Host = host;
        }

        private static Uri SetHost(Uri baseUrl)
        {
            if (_Host == null)
            {
                return baseUrl;
            }

            baseUrl = (new UriBuilder(baseUrl) { Host = _Host }).Uri;
            _Host = null;

            return baseUrl;
        }

        /// <summary>
        /// Sends a GET request to an Azure DevOps API
        /// </summary>
        /// <typeparam name="T">The typed model (JSON proxy class) corresponding to the API result</typeparam>
        /// <param name="apiPath">The path to the API, relative to the collection/organization URL</param>
        /// <param name="apiVersion">The version of the requested API (e.g. "5.1")</param>
        /// <param name="additionalHeaders">Any additional HTTP headers that must be sent along the request</param>
        /// <param name="queryParameters">Any query parameters ("query string") that are part of the request</param>
        /// <param name="mediaType">The MIME content type of the response</param>
        /// <param name="userState">User-defined, arbitrary data sent as a "userstate" HTTP header</param>
        /// <returns>The response of the API, converted to the model type T</returns>
        public T Get<T>(
            string apiPath,
            string apiVersion = "1.0",
            IDictionary<string, string> additionalHeaders = null,
            IDictionary<string, string> queryParameters = null,
            string mediaType = "application/json",
            object userState = null)
        {
            var msg = CreateMessage(HttpMethod.Get, apiPath, apiVersion, 
                additionalHeaders, queryParameters, mediaType,
                null, null);

            return SendAsync<T>(msg, userState).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sends a GET request to an Azure DevOps API
        /// </summary>
        /// <param name="apiPath">The path to the API, relative to the collection/organization URL</param>
        /// <param name="apiVersion">The version of the requested API (e.g. "5.1")</param>
        /// <param name="additionalHeaders">Any additional HTTP headers that must be sent along the request</param>
        /// <param name="queryParameters">Any query parameters ("query string") that are part of the request</param>
        /// <param name="mediaType">The MIME content type of the response</param>
        /// <param name="userState">User-defined, arbitrary data sent as a "userstate" HTTP header</param>
        /// <returns>The response of the API as an HttpResponseMessage object</returns>
        public HttpResponseMessage Get(
            string apiPath,
            string apiVersion = "1.0",
            IDictionary<string, string> additionalHeaders = null,
            IDictionary<string, string> queryParameters = null,
            string mediaType = "application/json",
            object userState = null)
        {
            var msg = CreateMessage(HttpMethod.Get, apiPath, apiVersion, 
                additionalHeaders, queryParameters, 
                mediaType, null, null);

            return Send(msg, userState);
        }

        /// <summary>
        /// Sends a POST request to an Azure DevOps API
        /// </summary>
        /// <typeparam name="T">The typed model (JSON proxy class) corresponding to the API parameter (content) object</typeparam>
        /// <typeparam name="TResult">The typed model (JSON proxy class) corresponding to the API result</typeparam>
        /// <param name="apiPath">The path to the API, relative to the collection/organization URL</param>
        /// <param name="value">The API parameters sent as the request body</param>
        /// <param name="apiVersion">The version of the requested API (e.g. "5.1")</param>
        /// <param name="additionalHeaders">Any additional HTTP headers that must be sent along the request</param>
        /// <param name="queryParameters">Any query parameters ("query string") that are part of the request</param>
        /// <param name="mediaType">The MIME content type of the response</param>
        /// <param name="userState">User-defined, arbitrary data sent as a "userstate" HTTP header</param>
        /// <returns>The response of the API as an HttpResponseMessage object</returns>
        public TResult Post<T, TResult>(
            string apiPath,
            T value,
            string apiVersion = "1.0",
            IDictionary<string, string> additionalHeaders = null,
            IDictionary<string, string> queryParameters = null,
            string mediaType = "application/json",
            object userState = null)
        {
            var content = new ObjectContent<T>(value, new VssJsonMediaTypeFormatter());

            var msg = CreateMessage(HttpMethod.Post, apiPath, apiVersion, 
                additionalHeaders, queryParameters, 
                mediaType, null, content);

            var result = SendAsync<TResult>(msg, userState).GetAwaiter().GetResult();

            return result;
        }

        /// <summary>
        /// Sends a POST request to an Azure DevOps API
        /// </summary>
        /// <param name="apiPath">The path to the API, relative to the collection/organization URL</param>
        /// <param name="content">The API parameters sent as the request body</param>
        /// <param name="apiVersion">The version of the requested API (e.g. "5.1")</param>
        /// <param name="additionalHeaders">Any additional HTTP headers that must be sent along the request</param>
        /// <param name="queryParameters">Any query parameters ("query string") that are part of the request</param>
        /// <param name="mediaType">The MIME content type of the response</param>
        /// <param name="userState">User-defined, arbitrary data sent as a "userstate" HTTP header</param>
        /// <returns>The response of the API as an HttpResponseMessage object</returns>
        public HttpResponseMessage Post(
            string apiPath,
            HttpContent content,
            string apiVersion = "1.0",
            IDictionary<string, string> additionalHeaders = null,
            IDictionary<string, string> queryParameters = null,
            string mediaType = "application/json",
            object userState = null)
        {
            var msg = CreateMessage(HttpMethod.Post, apiPath, apiVersion, 
                additionalHeaders, queryParameters, 
                mediaType, null, content);

            return Send(msg, userState);
        }

        public async Task<HttpResponseMessage> InvokeAsync(
            HttpMethod method,
            string apiPath,
            string content = null,
            string requestMediaType = "application/json",
            string responseMediaType = "application/json",
            IDictionary<string, string> additionalHeaders = null,
            IDictionary<string, string> queryParameters = null,
            string apiVersion = "1.0",
            object userState = null)
        {
            HttpContent httpContent = null;

            if (!string.IsNullOrWhiteSpace(content))
            {
                httpContent = new StringContent(content, Encoding.UTF8, requestMediaType);
            }

            var msg = CreateMessage(method, apiPath, apiVersion,
                additionalHeaders, queryParameters,
                responseMediaType, null, httpContent);

            return await SendAsync(msg, userState);
        }

        public T PostForm<T>(
            string formPath,
            Dictionary<string,string> formData,
            bool sendVerificationToken = false,
            string tokenRequestPath = null,
            IDictionary<string, string> additionalHeaders = null,
            IDictionary<string, string> queryParameters = null,
            string responseMediaType = "text/html",
            object userState = null)
        {
            if (formData == null) { throw new ArgumentNullException(nameof(formData)); }

            if (sendVerificationToken)
            {
                var response = Get(tokenRequestPath);
                var html = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var matches = Regex.Match(html, "name=\"__RequestVerificationToken\" value=\"([^\\\"]*)");

                if (matches.Groups.Count > 0)
                {
                    formData["__RequestVerificationToken"] = matches.Groups[1].Value;
                }

                var cookies = response.Headers.GetValues("Set-Cookie")?.ToList();

                if (cookies?.Count > 0)
                {
                    if (additionalHeaders == null)
                    {
                        additionalHeaders = new Dictionary<string, string>();
                    }

                    var tokenCookie = string.Join("; ", cookies
                        .Where(c => c.StartsWith("__RequestVerificationToken"))
                        .Select(c => c.Substring(0, c.IndexOf(';'))));

                    if (additionalHeaders.ContainsKey("Cookie"))
                    {
                        var presetCookie = additionalHeaders["Cookie"];
                        additionalHeaders["Cookie"] = $"{tokenCookie};{presetCookie}";
                    }
                    else
                    {
                        additionalHeaders["Cookie"] = $"{tokenCookie};";
                    }
                }
            }

            var content = new FormUrlEncodedContent(formData);

            var msg = CreateMessage(HttpMethod.Post, formPath, "1",
                additionalHeaders, queryParameters,
                responseMediaType, null, content);

            return SendAsync<T>(msg, userState).GetAwaiter().GetResult();
        }

        private HttpRequestMessage CreateMessage(HttpMethod method, string apiPath, string apiVersion,
            IDictionary<string, string> additionalHeaders,
            IDictionary<string, string> queryParameters, string mediaType, object routeValues,
            HttpContent content)
        {
            var msg = CreateRequestMessage(
                method,
                additionalHeaders,
                new ApiResourceLocation
                {
                    ReleasedVersion = new Version(1, 0),
                    MinVersion = new Version(1, 0),
                    MaxVersion = new Version(9, 9),
                    RouteTemplate = apiPath,
                    ResourceVersion = 1
                },
                routeValues,
                new ApiResourceVersion(apiVersion),
                content,
                queryParameters,
                mediaType);
 
            Uri = msg.RequestUri;
            
            return msg;
        }
    }
}