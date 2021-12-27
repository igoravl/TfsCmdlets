using System.Net.Http;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.RestApi
{
    [CmdletController]
    partial class InvokeRestApiController
    {
        [Import]
        private IRestApiService RestApiService {get;set;}
        
        public override object InvokeCommand()
        {
            var path = Parameters.Get<string>("path");
            var method = Parameters.Get<string>("Method");
            var body = Parameters.Get<string>("Body");
            var useHost = Parameters.Get<string>("UseHost");
            var apiVersion = Parameters.Get<string>("ApiVersion");
            var requestContentType = Parameters.Get<string>("RequestContentType");
            var responseContentType = Parameters.Get<string>("ResponseContentType");
            var additionalHeaders = Parameters.Get<Hashtable>("AdditionalHeaders");
            var queryParameters = Parameters.Get<Hashtable>("QueryParameters");
            var asTask = Parameters.Get<bool>("AsTask");
            var raw = Parameters.Get<bool>("Raw");
            var noAutoUnwrap = Parameters.Get<bool>("NoAutoUnwrap");

            if (path.Contains(" "))
            {
                var tokens = path.Split(' ');

                if (IsHttpMethod(tokens[0]))
                {
                    method = tokens[0];
                    path = path.Substring(tokens[0].Length+1);
                }
            }

            var tpc = Data.GetCollection();

            path = path.Replace("https://{instance}/{collection}/", "http://tfs/");

            if (Uri.TryCreate(path, UriKind.Absolute, out var uri))
            {
                if(string.IsNullOrEmpty(useHost) && !uri.Host.Equals(tpc.Uri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    useHost = uri.Host;
                }
                
                path = uri.AbsolutePath.Replace("/%7Borganization%7D/", "");

                if (uri.AbsoluteUri.StartsWith((string) tpc.Uri.AbsoluteUri))
                {
                    path = path.Substring(tpc.Uri.AbsoluteUri.Length);
                }

                var query = uri.ParseQueryString();

                if(query["api-version"] != null)
                {
                    apiVersion = query["api-version"];
                }
            }

            if (path.Contains("%7Bproject%7D") || path.Contains("%7BprojectId%7D"))
            {
                var tp = Data.GetProject();

                path = path
                    .Replace("%7Bproject%7D", tp.Id.ToString())
                    .Replace("%7BprojectId%7D", tp.Id.ToString());

                Logger.Log($"Replace token {{project[Id]}} in URL with [{tp.Id}]");
            }

            if (path.Contains("%7Bteam%7D") || path.Contains("%7BteamId%7D"))
            {
                var t = Data.GetTeam();

                path = path
                    .Replace("%7Bteam%7D", t.Id.ToString())
                    .Replace("%7BteamId%7D", t.Id.ToString());

                Logger.Log($"Replace token {{team}} in URL with [{t.Id}]");
            }

            Logger.Log($"path '{path}', version '{apiVersion}'");

            string host = null;

            if(tpc.IsHosted)
            {
                if(!string.IsNullOrEmpty(useHost))
                {
                    host = useHost;
                }
                else if (!tpc.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase))
                {
                    host = tpc.Uri.Host;
                }
            }

            var task = RestApiService.InvokeAsync(tpc, path, method, body,
                requestContentType, 
                responseContentType,
                HashtableExtensions.ToDictionary<string, string>(additionalHeaders),
                HashtableExtensions.ToDictionary<string, string>(queryParameters),
                apiVersion,
                host);

            Logger.Log($"{method} {RestApiService.Url.AbsoluteUri}");

            if (asTask) return task;

            var result = task.GetResult("Unknown error when calling REST API");
            var responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseType = result.Content.Headers.ContentType.MediaType;

            return !raw && responseType.Equals("application/json")
                ? PSJsonConverter.Deserialize(responseBody, (bool) noAutoUnwrap)
                : responseBody;
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