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
            var task = RestApiService.InvokeTemplateAsync(
                connection: Collection, 
                apiTemplate: Path, 
                body: Body,
                method: Method, 
                requestContentType: RequestContentType,
                responseContentType: ResponseContentType,
                additionalHeaders: AdditionalHeaders.ToDictionary<string, string>(),
                apiVersion: ApiVersion,
                customServiceHost: UseHost);

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