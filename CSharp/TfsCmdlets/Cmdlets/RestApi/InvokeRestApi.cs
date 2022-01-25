using System.Management.Automation;

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
    [TfsCmdlet(CmdletScope.Team)]
    partial class InvokeRestApi
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
        [Parameter]
        public string Method { get; set; } = "GET";

        /// <summary>
        /// Specifies the request body to send to the API endpoint. Tipically contains the JSON payload 
        /// required by the API.
        /// </summary>
        [Parameter]
        [Alias("Content")]
        public string Body { get; set; }

        /// <summary>
        ///  Specifies the request body content type to send to the API. When omitted, defaults to
        /// "application/json".
        /// </summary>
        [Parameter]
        public string RequestContentType { get; set; } = "application/json";

        /// <summary>
        ///  Specifies the response body content type returned by the API. When omitted, defaults to
        /// "application/json".
        /// </summary>
        [Parameter]
        public string ResponseContentType { get; set; } = "application/json";

        /// <summary>
        /// Specifies a hashtable with additional HTTP headers to send to the API endpoint.
        /// </summary>
        [Parameter]
        public Hashtable AdditionalHeaders { get; set; }

        /// <summary>
        /// Specifies a hashtable with additional query parameters to send to the API endpoint.
        /// </summary>
        [Parameter(Position=1)]
        [Alias("Parameters")]
        public Hashtable QueryParameters { get; set; }

        /// <summary>
        /// Specifies the desired API version. When omitted, defaults to "4.1".
        /// </summary>
        [Parameter]
        public string ApiVersion { get; set; } = "4.1";

        /// <summary>
        /// Specifies an alternate host name for APIs not hosted in "dev.azure.com", 
        /// e.g. "vsaex.dev.azure.com" or "vssps.dev.azure.com".
        /// </summary>
        [Parameter]
        public string UseHost { get; set; }

        /// <summary>
        /// Prevents the automatic expansion (unwrapping) of the 'value' property in the response JSON.
        /// </summary>
        [Parameter]
        public SwitchParameter NoAutoUnwrap { get; set; }

        /// <summary>
        /// Returns the API response as an unparsed string. If omitted, JSON responses will be 
        /// parsed, converted and returned as objects (via ConvertFrom-Json).
        /// </summary>
        [Parameter]
        public SwitchParameter Raw { get; set; }

        /// <summary>
        /// Saves the API response to a file. If omitted, the response will be written to the stardard output stream.
        /// </summary>
        [Parameter]
        public string Destination { get; set; }

        /// <summary>
        /// Returns the System.Threading.Tasks.Task object used to issue the asynchronous call to the API. 
        /// The caller is responsible for finishing the asynchronous call by e.g. accessing the Result property.
        /// </summary>
        [Parameter]
        public SwitchParameter AsTask { get; set; }
    }
}