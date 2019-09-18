using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.Services.Common;

namespace TfsCmdlets
{
    public class SpsGenericHttpClient: GenericHttpClient
    {
        public SpsGenericHttpClient(Uri baseUrl, VssCredentials credentials) : base(SetSpsHost(baseUrl), credentials)
        {
        }

        public SpsGenericHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings) : base(SetSpsHost(baseUrl), credentials, settings)
        {
        }

        public SpsGenericHttpClient(Uri baseUrl, VssCredentials credentials, params DelegatingHandler[] handlers) : base(SetSpsHost(baseUrl), credentials, handlers)
        {
        }

        public SpsGenericHttpClient(Uri baseUrl, HttpMessageHandler pipeline, bool disposeHandler) : base(SetSpsHost(baseUrl), pipeline, disposeHandler)
        {
        }

        public SpsGenericHttpClient(Uri baseUrl, VssCredentials credentials, VssHttpRequestSettings settings, params DelegatingHandler[] handlers) : base(SetSpsHost(baseUrl), credentials, settings, handlers)
        {
        }

        private static Uri SetSpsHost(Uri baseUrl)
        {
            return (new UriBuilder(baseUrl) {Host = "vssps.dev.azure.com"}).Uri;
        }
    }
}
