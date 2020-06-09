using System;
using System.Collections;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.RestApi
{
    /// <summary>
    /// Invoke an Azure DevOps REST API
    /// </summary>
    [Cmdlet(VerbsLifecycle.Invoke, "TfsRestApi")]
    public class InvokeRestApi : BaseCmdlet
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
        protected override void ProcessRecord()
        {
            if (Path.IsLike("*{project}*"))
            {
                //TODO: Team / TP

                //if (Team.Project)
                //{
                //    Project = Team.Project
                //};

                //tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
                //Path = Path.Replace("{project}", tp.Guid)

                //this.Log($"Replace token {{project}} in URL with [{tp.Guid}]");
            }

            if (Path.IsLike("*{team}*"))
            {
                //TODO: Team
                //t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)
                //Path = Path.Replace("{team}", t.Id)

                //this.Log($"Replace token {team} in URL with "{{t}.Id}"");
            }

            var collection = this.GetCollection();

            if(Uri.IsWellFormedUriString(Path, UriKind.Absolute))
            {
                var uri = new Uri(Path);

                if(uri.AbsoluteUri.StartsWith(collection.Uri.AbsoluteUri))
                {
                    Path = Path.Substring(collection.Uri.AbsoluteUri.Length);
                }
            }

            this.Log($"Calling API '{Path}', version 'ApiVersion', via {Method}");

            var client = this.GetService<IRestApiService>();
            var task = client.InvokeAsync(collection, Path, Method, Body,
                RequestContentType, ResponseContentType,
                AdditionalHeaders.ToDictionary<string,string>(), 
                QueryParameters.ToDictionary<string,string>(), 
                ApiVersion);

            this.Log($"Actual URL called: {client.Uri}");

            if (AsTask)
            {
                WriteObject(task);
                return;
            }

            var result = task.GetResult("Unknown error when calling REST API");
            var responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseType = result.Content.Headers.ContentType.MediaType;

            if (Raw)
            {
                var o = new PSObject(result);

                o.AddNoteProperty("ResponseBody", responseBody);

                if (responseType.Equals("application/json"))
                    o.AddNoteProperty("ResponseObject", PSJsonConverter.Deserialize(responseBody));

                WriteObject(o);
                return;
            }

            WriteObject(responseType.Equals("application/json")
                ? PSJsonConverter.Deserialize(responseBody)
                : responseBody);
        }
    }
}