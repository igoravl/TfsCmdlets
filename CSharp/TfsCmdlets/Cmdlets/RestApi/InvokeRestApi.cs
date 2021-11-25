using System;
using System.Collections;
using System.Management.Automation;
using System.Net.Http;
using TfsCmdlets.Extensions;
using TfsCmdlets.HttpClient;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.RestApi
{
    /// <summary>
    /// Invoke an Azure DevOps REST API.
    /// </summary>
    /// <remarks>
    /// Invoke-TfsRestApi can automatically parse an example URL from 
    /// https://docs.microsoft.com/en-us/rest/api/azure/devops/ and replace its various tokens 
    /// (such as {organization}, {project} and {team}) as long as collection / project / team 
    /// information are available via either the their respective arguments in this command or the 
    /// corresponding Connect-Tfs* cmdlet. HTTP method and API version are also automatically extracted 
    /// from the supplied example, when available.
    /// </remarks>
    /// <example>
    ///   <code>Invoke-TfsRestApi -Method GET -Path /_apis/projects -ApiVersion 4.1 -Collection DefaultCollection</code>
    ///   <para>Calls a REST API that lists all team projects in a TFS collection named DefaultCollection</para>
    /// </example>
    /// <example>
    ///   <code>Invoke-TfsRestApi 'GET https://extmgmt.dev.azure.com/{organization}/_apis/extensionmanagement/installedextensions?api-version=5.1-preview.1'</code>
    ///   <para>Calls the API described by an example extracted from the docs.microsoft.com web site. 
    ///     HTTP method, host name and API version are all set based on the supplied values; 
    ///     Tokens {organization}, {project} and {team} are properly replaced with the corresponding 
    ///     values provided by the current connection context (via previous calls to 
    ///     Connect-TfsTeamProjectCollection, Connect-TfsTeamProject and/or Connect-TfsTeam).</para>
    /// </example>
    /// <example>
    ///   <code>Invoke-TfsRestApi 'GET https://{instance}/{collection}/_apis/process/processes?api-version=4.1' -Collection http://vsalm:8080/tfs/DefaultCollection</code>
    ///   <para>Calls an API in a TFS instance, parsing the example provided by the docs.microsoft.com web site.</para>
    /// </example>
    [Cmdlet(VerbsLifecycle.Invoke, "TfsRestApi")]
    public class InvokeRestApi : CmdletBase
    {
        /// <summary>
        /// Specifies the path of the REST API to call. Tipically it is the portion of the URL after 
        /// the name of the collection/organization, i.e. in the URL 
        /// https://dev.azure.com/{organization}/_apis/projects?api-version=5.1 the path is 
        /// "/_apis/projects".
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Path { get; set; }

        /// <summary>
        /// Specifies the HTTP method to call the API endpoint. When omitted, defaults to "GET".
        /// </summary>
        [Parameter()]
        public string Method { get; set; } = "GET";

        /// <summary>
        /// Specifies the request body to send to the API endpoint. Tipically contains the JSON payload 
        /// required by the API.
        /// </summary>
        [Parameter()]
        [Alias("Content")]
        public string Body { get; set; }

        /// <summary>
        ///  Specifies the request body content type to send to the API. When omitted, defaults to
        /// "application/json".
        /// </summary>
        [Parameter()]
        public string RequestContentType { get; set; } = "application/json";

        /// <summary>
        ///  Specifies the response body content type returned by the API. When omitted, defaults to
        /// "application/json".
        /// </summary>
        [Parameter()]
        public string ResponseContentType { get; set; } = "application/json";

        /// <summary>
        /// Specifies a hashtable with additional HTTP headers to send to the API endpoint.
        /// </summary>
        [Parameter()]
        public Hashtable AdditionalHeaders { get; set; }

        /// <summary>
        /// Specifies a hashtable with additional query parameters to send to the API endpoint.
        /// </summary>
        [Parameter()]
        public Hashtable QueryParameters { get; set; }

        /// <summary>
        /// Specifies the desired API version. When omitted, defaults to "4.1".
        /// </summary>
        [Parameter()]
        public string ApiVersion { get; set; } = "4.1";

        /// <summary>
        /// Specifies an alternate host name for APIs not hosted in "dev.azure.com", 
        /// e.g. "vsaex.dev.azure.com" or "vssps.dev.azure.com".
        /// </summary>
        [Parameter()]
        public string UseHost { get; set; }

        /// <summary>
        /// Returns the API response as an unparsed string. If omitted, JSON responses will be 
        /// parsed, converted and returned as objects (via ConvertFrom-Json).
        /// </summary>
        [Parameter()]
        public SwitchParameter Raw { get; set; }

        /// <summary>
        /// Returns the System.Threading.Tasks.Task object used to issue the asynchronous call to the API. 
        /// The caller is responsible for finishing the asynchronous call by e.g. accessing the Result property.
        /// </summary>
        [Parameter()]
        public SwitchParameter AsTask { get; set; }

        /// <summary>
        ///  HELP_PARAM_TEAM
        /// </summary>
        [Parameter()]
        public object Team { get; set; }

        /// <summary>
        ///  HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            if (Path.Contains(" "))
            {
                var tokens = Path.Split(' ');

                if (IsHttpMethod(tokens[0]))
                {
                    Method = tokens[0];
                    Path = Path.Substring(tokens[0].Length+1);
                }
            }

            var tpc = this.GetCollection();

            Path = Path.Replace("https://{instance}/{collection}/", "http://tfs/");

            if (Uri.TryCreate(Path, UriKind.Absolute, out var uri))
            {
                if(string.IsNullOrEmpty(UseHost) && !uri.Host.Equals(tpc.Uri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    UseHost = uri.Host;
                }
                
                Path = uri.AbsolutePath.Replace("/%7Borganization%7D/", "");

                if (uri.AbsoluteUri.StartsWith(tpc.Uri.AbsoluteUri))
                {
                    Path = Path.Substring(tpc.Uri.AbsoluteUri.Length);
                }

                var query = uri.ParseQueryString();

                if(query["api-version"] != null)
                {
                    ApiVersion = query["api-version"];
                }
            }

            if (Path.Contains("%7Bproject%7D") || Path.Contains("%7BprojectId%7D"))
            {
                var (_, tp) = GetCollectionAndProject();

                Path = Path
                    .Replace("%7Bproject%7D", tp.Id.ToString())
                    .Replace("%7BprojectId%7D", tp.Id.ToString());

                this.Log($"Replace token {{project[Id]}} in URL with [{tp.Id}]");
            }

            if (Path.Contains("%7Bteam%7D") || Path.Contains("%7BteamId%7D"))
            {
                var (_, _, t) = GetCollectionProjectAndTeam();

                Path = Path
                    .Replace("%7Bteam%7D", t.Id.ToString())
                    .Replace("%7BteamId%7D", t.Id.ToString());

                this.Log($"Replace token {{team}} in URL with [{t.Id}]");
            }

            this.Log($"Path '{Path}', version '{ApiVersion}'");

            string host = null;

            if(tpc.IsHosted)
            {
                if(!string.IsNullOrEmpty(UseHost))
                {
                    host = UseHost;
                }
                else if (!tpc.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase))
                {
                    host = tpc.Uri.Host;
                }
            }

            var client = this.GetService<IRestApiService>();

            var task = client.InvokeAsync(tpc, Path, Method, Body,
                RequestContentType, ResponseContentType,
                AdditionalHeaders.ToDictionary<string, string>(),
                QueryParameters.ToDictionary<string, string>(),
                ApiVersion,
                host);

            this.Log($"{Method} {client.Url.AbsoluteUri}");

            if (AsTask)
            {
                WriteObject(task);
                return;
            }

            var result = task.GetResult("Unknown error when calling REST API");
            var responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseType = result.Content.Headers.ContentType.MediaType;

            WriteObject(!Raw && responseType.Equals("application/json")
                ? PSJsonConverter.Deserialize(responseBody)
                : responseBody);
        }

        private bool IsHttpMethod(string method)
        {
            try
            {
                var m = new HttpMethod(method);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}