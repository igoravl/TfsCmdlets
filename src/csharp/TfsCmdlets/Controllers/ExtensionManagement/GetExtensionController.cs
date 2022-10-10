using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Controllers.ExtensionManagement
{
    [CmdletController(typeof(InstalledExtension))]
    partial class GetExtensionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ExtensionManagementHttpClient>();

            foreach (var input in Extension)
            {
                object extension = input;
                string publisher = Publisher;

                if(extension is string s0 && s0.Contains("."))
                {
                    var (pub, ext, _) = s0.Split('.');

                    publisher = pub;
                    extension = ext;
                }

                switch (extension)
                {
                    case InstalledExtension ext:
                        {
                            yield return ext;
                            break;
                        }
                    case string s when s.IsWildcard() || publisher.IsWildcard():
                        {
                            Logger.Log($"Getting extensions matching name '{s}' with publisher '{publisher}'");

                            foreach (var result in client.GetInstalledExtensionsAsync(
                                includeDisabledExtensions: IncludeDisabledExtensions,
                                includeErrors: IncludeErrors,
                                includeInstallationIssues: IncludeInstallationIssues)
                                .GetResult("Error getting installed extensions")
                                .Where(ext => (ext.ExtensionName.IsLike(s) || ext.ExtensionDisplayName.IsLike(s)) &&
                                    (ext.PublisherName.IsLike(publisher) || ext.PublisherDisplayName.IsLike(publisher))))
                            {
                                yield return result;
                            }
                            break;
                        }
                    case string s:
                        {
                            yield return client.GetInstalledExtensionByNameAsync(publisher, s)
                                .GetResult("Error getting installed extension.");
                            break;
                        }
                    default:
                        {
                            Logger.LogError(new ArgumentException($"Invalid or unknown extension '{input}'"));
                            break;
                        }
                }
            }
        }
    }
}