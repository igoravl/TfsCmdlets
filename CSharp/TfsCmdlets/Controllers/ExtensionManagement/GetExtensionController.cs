using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class GetExtensionController
    {
        public override IEnumerable<InstalledExtension> Invoke()
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
                        default: {
                            Logger.LogError(new ArgumentException($"Invalid or unknown extension '{input}'"));
                            break;
                        }
                }
            }
        }
    }
}