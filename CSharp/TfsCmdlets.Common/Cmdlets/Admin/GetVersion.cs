using System;
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
    ///   The Get-TfsVersion cmdlet retrieves version information from the supplied team project collection 
    ///   or Azure DevOps organization. Currently supported platforms are Team Foundation Server 2015+, 
    ///   Azure DevOps Server 2019+ and Azure DevOps Services. When available/applicable, detailed 
    ///   information about installed updates, deployed sprints and so on are also provided.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsVersion")]
    [OutputType(typeof(ServerVersion))]
    public class GetVersion : CmdletBase
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var tpc = this.GetCollection();
            ServerVersion serverVersion = null;

            if (tpc.IsHosted)
            {
                this.Log("Detected Azure DevOps Services organization");
                var result = this.GetService<IRestApiService>().InvokeAsync(tpc, "/").SyncResult();
                var html = result.Content.ReadAsStringAsync().GetResult();
                var matches = (new Regex(@"""serviceVersion"":""(.+?) \((.+?)\)""")).Matches(html);

                if (matches.Count == 0)
                {
                    this.Log("Response does not contain 'serviceVersion' information");
                    throw new Exception("Azure DevOps Services version not found in response.");
                }

                var version = new Version(Regex.Replace(matches[0].Groups[1].Value, "[a-zA-Z]", "") + ".0");

                serverVersion = new ServerVersion {
                    Version = version,
                    LongVersion = $"{matches[0].Groups[1].Value} ({matches[0].Groups[2].Value})",
                    Sprint = version.Minor.ToString(),
                    FriendlyVersion = $"Azure DevOps Services, Sprint {version.Minor} ({matches[0].Groups[1].Value})",
                    IsHosted = true
                };
            }
            else
            {
                this.Log("Detected Azure DevOps Server / TFS collection");
                var result = this.GetService<IRestApiService>().InvokeAsync(tpc, "/_home/About").GetResult("Error accessing About page (/_home/About) in TFS");
                var html = result.Content.ReadAsStringAsync().GetResult("Error accessing About page (/_home/About) in TFS");
                var matches = (new Regex(@"\>Version (.+?)\<")).Matches(html);

                if (matches.Count == 0)
                {
                    this.Log("Response does not contain 'Version' information");
                    throw new Exception("Team Foundation Server version not found in response.");
                }

                var version = new Version(matches[0].Groups[1].Value);

                if(!TfsVersionTable.TryGetServerVersion(version.Major, out serverVersion))
                {
                    serverVersion = new ServerVersion {
                        Version = version,
                        FriendlyVersion = (version.Major >= 17? $"Azure DevOps": "Team Foundation") + $" Server {TfsVersionTable.GetYear(version.Major)}",
                        IsHosted = false,
                        LongVersion = version.ToString(),
                        Sprint = "N/A",
                        Update = "N/A"
                    };
                }

            }

            WriteObject(serverVersion);
        }
    }
}