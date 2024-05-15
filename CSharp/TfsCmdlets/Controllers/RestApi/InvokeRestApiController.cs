using System.Net.Http;
using TfsCmdlets.Util;

namespace TfsCmdlets.Controllers.RestApi
{
    [CmdletController]
    partial class InvokeRestApiController
    {
        [Import]
        private IRestApiService RestApiService { get; set; }

        protected override IEnumerable Run()
        {
            if (Path.Contains(" "))
            {
                var tokens = Path.Split(' ');

                if (IsHttpMethod(tokens[0]))
                {
                    Method = tokens[0];
                    Path = Path.Substring(tokens[0].Length + 1).Trim();
                }
            }

            Path = Path.Replace("https://{instance}/{collection}/", "http://tfs/");

            var queryParams = new Dictionary<string, string>();

            if (QueryParameters != null)
            {
                foreach (var key in QueryParameters.Keys)
                {
                    queryParams.Add(key.ToString(), QueryParameters[key]?.ToString());
                }
            }

            if (Uri.TryCreate(Path, UriKind.Absolute, out var uri))
            {
                if (string.IsNullOrEmpty(UseHost) && !uri.Host.Equals(Collection.Uri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    UseHost = uri.Host;
                }

                Path = uri.AbsolutePath.Replace("/%7Borganization%7D/", "");

                var collectionName = Collection.DisplayName.Trim('/');

                if (Path.StartsWith($"/{collectionName}"))
                {
                    Path = Path.Substring(collectionName.Length + 1);
                }

                var query = uri.ParseQueryString();

                if (query["api-version"] != null) ApiVersion = query["api-version"];

                foreach (var key in query.AllKeys.Where(k => !k.Equals("api-version")))
                {
                    if ($"{{{key}}}".Equals(query[key], StringComparison.OrdinalIgnoreCase) && !queryParams.ContainsKey(key))
                    {
                        Logger.LogWarn($"Parameter '{key}' found in the URL query string, but missing from QueryParameters argument. To keep this parameter, add it to the QueryParameters argument.");
                        continue;
                    }

                    if (queryParams.ContainsKey(key)) continue;

                    queryParams.Add(key, query[key]);
                }
            }

            if (Path.Contains("%7Bproject%7D") || Path.Contains("%7BprojectId%7D"))
            {
                Path = Path.Replace($"%7Bproject%7D", Project.Name);
                Path = Path.Replace($"%7BprojectId%7D", Project.Id.ToString());
            }

            if (Path.Contains("%7Bteam%7D") || Path.Contains("%7BteamId%7D"))
            {
                Path = Path.Replace($"%7Bteam%7D", Team.Name);
                Path = Path.Replace($"%7BteamId%7D", Team.Id.ToString());
            }

            var keysToRemove = new List<string>();

            foreach (var kvp in queryParams.Where(kvp => Path.Contains($"%7B{kvp.Key}%7D")))
            {
                keysToRemove.Add(kvp.Key);
                Path = Path.Replace($"%7B{kvp.Key}%7D", kvp.Value);
            }

            keysToRemove.ForEach(k => queryParams.Remove(k));

            Logger.Log($"Path '{Path}', version '{ApiVersion}'");

            string host = null;

            if (Collection.IsHosted)
            {
                if (!string.IsNullOrEmpty(UseHost))
                {
                    host = UseHost;
                }
                else if (!Collection.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase))
                {
                    host = Collection.Uri.Host;
                }
            }

            var task = RestApiService.InvokeAsync(Collection, Path, Method, Body,
                RequestContentType,
                ResponseContentType,
                AdditionalHeaders.ToDictionary<string, string>(),
                queryParams,
                ApiVersion,
                host);

            Logger.Log($"{Method} {RestApiService.Url.AbsoluteUri}");

            if (AsTask)
            {
                yield return task;
                yield break;
            }

            var result = task.GetResult("Unknown error when calling REST API");

            if(Has_Destination)
            {
                using var file = File.Create(Destination);
                result.Content.ReadAsStreamAsync().GetAwaiter().GetResult().CopyTo(file);

                yield break;
            }

            var responseType = result.Content.Headers.ContentType?.MediaType;

            switch (responseType)
            {
                case "application/json" when !Raw:
                    {
                        var responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        yield return PSJsonConverter.Deserialize(responseBody, (bool)NoAutoUnwrap);
                        break;
                    }
                case "application/json":
                case "application/xml":
                case "text/plain":
                case "text/html":
                    {
                        var responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        yield return responseBody;
                        break;
                    }
                default:
                    {
                        var responseBody = result.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                        yield return responseBody;
                        break;
                    }
            }
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