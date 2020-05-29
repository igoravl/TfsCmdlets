using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.Admin
{
    [Cmdlet(VerbsCommon.Get, "Version")]
    [OutputType(typeof(ServerVersion))]
    public class GetVersion : BaseCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }

        protected override void ProcessRecord()
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

                if(_tfsVersionTable.ContainsKey(version.ToString()))
                {
                    serverVersion = _tfsVersionTable[version.ToString()];
                }
                else
                {
                    serverVersion = new ServerVersion {
                        Version = version,
                        FriendlyVersion = (version.Major >= 17? $"Azure DevOps": "Team Foundation") + $" Server {_tfsMajorVersionTable[version.Major]}",
                        IsHosted = false,
                        LongVersion = version.ToString(),
                        Sprint = "N/A",
                        Update = "N/A"
                    };
                }

            }

            WriteObject(serverVersion);
        }

        private static readonly Dictionary<int,int> _tfsMajorVersionTable = new Dictionary<int, int> {
            [8] = 2005,
            [9] = 2008,
            [10] = 2010,
            [11] = 2011,
            [12] = 2013,
            [14] = 2015,
            [15] = 2017,
            [16] = 2018,
            [17] = 2019,
            [18] = 2020
        };

        private static readonly Dictionary<string, ServerVersion> _tfsVersionTable = new Dictionary<string, ServerVersion>() {

        };
    }

    public class ServerVersion
    {
        public Version Version { get; set; }
        public string LongVersion { get; set; }
        public string FriendlyVersion { get; set; }
        public bool IsHosted { get; set; }
        public string Sprint { get; set; }
        public string Update { get; set; }

    }
}