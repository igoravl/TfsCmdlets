using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Gets the version information about Team Foundation / Azure DevOps servers and 
    ///   Azure DevOps Services organizations.
    /// </summary>
    /// <remarks>
    /// The Get-TfsVersion cmdlet retrieves version information from the supplied team project collection or Azure DevOps organization. 
    /// When available/applicable, detailed information about installed updates, deployed sprints and so on are also provided.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsVersion")]
    [OutputType(typeof(ServerVersion))]
    public class GetVersion : GetCmdletBase<ServerVersion>
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public new object Collection { get; set; }
    }

    [Exports(typeof(ServerVersion))]
    internal class VersionDataService : BaseDataService<ServerVersion>
    {
        protected override IEnumerable<ServerVersion> DoGetItems()
        {
            var tpc = this.GetCollection();
            var svc = this.GetService<IRestApiService>();

            this.Log("Trying Azure DevOps Services detection logic");

            var result = svc.InvokeAsync(tpc, "/").SyncResult();
            var html = result.Content.ReadAsStringAsync().GetResult("Error accessing Azure DevOps home page (/)");
            var matches = (new Regex(@"""serviceVersion"":""(.+?)( \((.+?)\))?""")).Matches(html);

            Version version; string versionText = null, longVersion = null;

            if (matches.Count > 0)
            {
                versionText = Regex.Replace(matches[0].Groups[1].Value, "[a-zA-Z]", "") + ".0";

                this.Log($"Version text found: '{versionText}'");

                version = new Version(versionText);

                if(matches[0].Groups.Count == 4)
                {
                    longVersion = $"{matches[0].Groups[1].Value}" + (
                        !string.IsNullOrEmpty(matches[0].Groups[3].Value)? 
                            $" ({matches[0].Groups[3].Value})": 
                            ""
                        );
                }

                yield return new ServerVersion
                {
                    Version = version,
                    LongVersion = longVersion,
                    Update = version.Minor,
                    FriendlyVersion = "Azure DevOps " + (tpc.IsHosted ? $"Services, Sprint {version.Minor}" : $"Server {TfsVersionTable.GetYear(version.Major)}"),
                    IsHosted = tpc.IsHosted
                };

                yield break;
            }

            this.Log("Response does not contain 'serviceVersion' information. Trying TFS detection logic");

            result = svc.InvokeAsync(tpc, "/_home/About").GetResult("Error accessing About page (/_home/About) in TFS");
            html = result.Content.ReadAsStringAsync().GetResult("Error accessing About page (/_home/About)");
            matches = (new Regex(@"\>Version (.+?)\<")).Matches(html);

            if (matches.Count == 0)
            {
                this.Log("Response does not contain 'Version' information");
                this.Log($"Returned HTML: {html}");

                throw new Exception("Team Foundation Server version not found in response.");
            }

            versionText = matches[0].Groups[1].Value;

            this.Log($"Version text found: '{versionText}'");

            version = new Version(versionText);
            longVersion = $"{version} (TFS {TfsVersionTable.GetYear(version.Major)}))";

            yield return TfsVersionTable.GetServerVersion(version);
        }
    }
}