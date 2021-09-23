using System;
using System.Collections.Generic;
using System.Composition;
using System.Text.RegularExpressions;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Admin
{
    [Command]
    internal class GetVersion : CommandBase<ServerVersion>
    {
        private ITfsVersionTable TfsVersionTable { get; }
        private IRestApiService RestApi { get; set; }

        public override IEnumerable<ServerVersion> Invoke(ParameterDictionary parameters)
        {

           var tpc = Data.GetCollection(parameters);

           Logger.Log("Trying Azure DevOps Services detection logic");

           var result = RestApi.InvokeAsync(tpc, "/").GetResult("Error accessing Azure DevOps home page (/)");
           var html = result.Content.ReadAsStringAsync().GetResult("Error accessing Azure DevOps home page (/)");
           var matches = (new Regex(@"""serviceVersion"":""(.+?)( \((.+?)\))?""")).Matches(html);

           Version version;
           string versionText;
           string longVersion = null;

           if (matches.Count > 0)
           {
               versionText = Regex.Replace(matches[0].Groups[1].Value, "[a-zA-Z]", "") + ".0";

               Logger.Log($"Version text found: '{versionText}'");

               version = new Version(versionText);

               if(matches[0].Groups.Count == 4)
               {
                   longVersion = $"{matches[0].Groups[1].Value}" + (
                       !string.IsNullOrEmpty(matches[0].Groups[3].Value)? 
                           $" ({matches[0].Groups[3].Value})": 
                           ""
                       );
               }

               yield return new ServerVersion(version)
               {
                   LongVersion = longVersion,
                   Update = version.Minor,
                   FriendlyVersion = "Azure DevOps " + (tpc.IsHosted ? $"Services, Sprint {version.Minor}" : $"Server {TfsVersionTable.GetYear(version.Major)}"),
                   IsHosted = tpc.IsHosted,
                   YearVersion = TfsVersionTable.GetYear(version.Major)
               };

               yield break;
           }

           Logger.Log("Response does not contain 'serviceVersion' information. Trying TFS detection logic");

           result = RestApi.InvokeAsync(tpc, "/_home/About").GetResult("Error accessing About page (/_home/About) in TFS");
           html = result.Content.ReadAsStringAsync().GetResult("Error accessing About page (/_home/About) in TFS");
           matches = (new Regex(@"\>Version (.+?)\<")).Matches(html);

           if (matches.Count == 0)
           {
               Logger.Log("Response does not contain 'Version' information");
               Logger.Log($"Returned HTML: {html}");

               throw new Exception("Team Foundation Server version not found in response.");
           }

           versionText = matches[0].Groups[1].Value;

           Logger.Log($"Version text found: '{versionText}'");

           version = new Version(versionText);

           yield return TfsVersionTable.GetServerVersion(version);
        }
 
        [ImportingConstructor]
        public GetVersion(ITfsVersionTable tfsVersionTable, IRestApiService restApi, IPowerShellService powerShell, IDataManager data, ILogger logger) 
        : base(powerShell, data, logger)
        {
            TfsVersionTable = tfsVersionTable;
            RestApi = restApi;
        }
    }
}