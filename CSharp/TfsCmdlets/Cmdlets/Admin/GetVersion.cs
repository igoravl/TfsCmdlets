using System.Text.RegularExpressions;
using TfsCmdlets.Models;

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
    [TfsCmdlet(CmdletScope.Collection, OutputType = typeof(ServerVersion))]
    partial class GetVersion 
    {
    }

    [CmdletController(typeof(ServerVersion))]
    partial class GetVersionController
    {
        [Import]
        private ITfsVersionTable TfsVersionTable { get; }

        [Import]
        private IRestApiService RestApi { get; set; }

        protected override IEnumerable Run()
        {
           Logger.Log("Trying Azure DevOps Services detection logic");

           var result = RestApi.InvokeAsync(Collection, "/").GetResult("Error accessing Azure DevOps home page (/)");
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
                   FriendlyVersion = "Azure DevOps " + (Collection.IsHosted ? $"Services, Sprint {version.Minor}" : $"Server {TfsVersionTable.GetYear(version.Major)}"),
                   IsHosted = Collection.IsHosted,
                   YearVersion = TfsVersionTable.GetYear(version.Major)
               };

               yield break;
           }

           Logger.Log("Response does not contain 'serviceVersion' information. Trying TFS detection logic");

           result = RestApi.InvokeAsync(Collection, "/_home/About").GetResult("Error accessing About page (/_home/About) in TFS");
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
    }
}