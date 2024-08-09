using System.Management.Automation;
using Microsoft.VisualStudio.Services.ExtensionManagement.WebApi;

namespace TfsCmdlets.Cmdlets.ExtensionManagement
{
    /// <summary>
    /// Gets one or more installed extensions in the specified collection.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(InstalledExtension))]
    partial class GetExtension
    {
        /// <summary>
        /// Specifies the ID or the name of the extensions. Wildcards are supported. 
        /// When omitted, returns all extensions installed in the specified organization/collection.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards]
        public object Extension { get; set; } = "*";

        /// <summary>
        /// Specifies the ID or the name of the publisher. Wildcards are supported. 
        /// When omitted, returns all extensions installed in the specified organization/collection.
        /// </summary>
        [Parameter(Position = 1)]
        [SupportsWildcards]
        public string Publisher { get; set; } = "*";

        /// <summary>
        /// Includes disabled extensions in the result. When omitted, disabled extensions are not included in the result.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeDisabledExtensions { get; set; }

        /// <summary>
        /// Specifies whether to include errors in the result.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeErrors { get; set; }

        /// <summary>
        /// Specifies whether to include installation issues in the result.
        /// </summary>
        [Parameter]
        public SwitchParameter IncludeInstallationIssues { get; set; }
    }

    [CmdletController(typeof(InstalledExtension), Client=typeof(IExtensionManagementHttpClient))]
    partial class GetExtensionController
    {
        protected override IEnumerable Run()
        {
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

                            foreach (var result in Client.GetInstalledExtensionsAsync(
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
                            yield return Client.GetInstalledExtensionByNameAsync(publisher, s)
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