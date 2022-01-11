using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class GetExtensionController
    {
        protected override IEnumerable Run()
        {
            foreach (var input in Extension)
            {
                switch (input)
                {
                    case InstalledExtension ext:
                        {
                            yield return ext;
                            break;
                        }
                    case string s when s.IsWildcard() || Publisher.IsWildcard():
                        {
                            var client = GetClient<ExtensionManagementHttpClient>();

                            Logger.Log($"Getting extensions matching name '{s}' with publisher '{Publisher}'");

                            foreach (var result in client.GetInstalledExtensionsAsync(
                                includeDisabledExtensions: IncludeDisabledExtensions,
                                includeErrors: IncludeErrors,
                                includeInstallationIssues: IncludeInstallationIssues)
                                .GetResult("Error getting installed extensions")
                                .Where(ext => (ext.ExtensionName.IsLike(s) || ext.ExtensionDisplayName.IsLike(s)) &&
                                    (ext.PublisherName.IsLike(Publisher) || ext.PublisherDisplayName.IsLike(Publisher))))
                            {
                                yield return result;
                            }
                            break;
                        }
                        case string s: {
                            var client = GetClient<ExtensionManagementHttpClient>();

                            yield return client.GetInstalledExtensionByNameAsync(Publisher, s)
                                .GetResult("Error getting installed extension.");

                            break;
                        }
                        default: {
                            Logger.LogError(new ArgumentException($"Invalid or unknown extension '{input}'"));
                            break;
                        }
                }
            }
        }
    }
}