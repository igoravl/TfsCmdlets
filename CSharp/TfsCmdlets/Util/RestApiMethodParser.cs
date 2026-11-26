namespace TfsCmdlets.Util
{
    internal static class RestApiMethodParser
    {
        internal static bool TryParseMethodPrefix(string apiTemplate, out string method, out string remainingTemplate)
        {
            method = null;
            remainingTemplate = apiTemplate;

            if (apiTemplate == null || !apiTemplate.Contains(" "))
            {
                return false;
            }

            var tokens = apiTemplate.Split(' ');

            if (!IsHttpMethod(tokens[0]))
            {
                return false;
            }

            method = tokens[0];
            remainingTemplate = apiTemplate.Substring(tokens[0].Length + 1).Trim();
            return true;
        }

        private static bool IsHttpMethod(string method)
        {
            const string methods = "|GET|POST|PUT|PATCH|DELETE|";
            return methods.Contains($"|{method.ToUpperInvariant()}|");
        }
    }
}
