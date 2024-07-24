using System.Management.Automation;
using Microsoft.TeamFoundation.Wiki.WebApi;

namespace TfsCmdlets.Cmdlets.Wiki
{
    /// <summary>
    /// Gets information from one or more Wiki repositories in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WikiV2), DefaultParameterSetName = "Get all wikis")]
    partial class GetWiki
    {
        /// <summary>
        /// Specifies the name or ID of a Wiki repository. Wildcards are supported. 
        /// When omitted, all Wiki repositories in the supplied team project are returned.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get all wikis")]
        [SupportsWildcards()]
        [Alias("Name", "Id")]
        public object Wiki { get; set; } = "*";

        /// <summary>
        /// Returns only provisioned ("project") Wikis. When omitted, returns all Wikis 
        /// (both Project wikis and Code wikis).
        /// </summary>
        [Parameter(ParameterSetName = "Get Project Wiki", Mandatory = true)]
        public SwitchParameter ProjectWiki { get; set; }
    }

    [CmdletController(typeof(WikiV2))]
    partial class GetWikiController
    {
       protected override IEnumerable Run()
       {
           var client = GetClient<WikiHttpClient>();

           foreach(var input in Wiki)
           {
               var wiki = input switch {
                   string s when s.IsGuid() => new Guid(s),
                   _ => input
               };

               switch (wiki)
               {
                   case WikiV2 w:
                       {
                           yield return w;
                           break;
                       }
                   case null when ProjectWiki:
                   case {} when ProjectWiki:
                       {
                           foreach (var w in client.GetAllWikisAsync(Project.Name)
                                                .GetResult($"Error getting project wiki")
                                                .Where(r => r.Type == WikiType.ProjectWiki))
                           {
                               yield return w;
                           }
                           break;
                       }
                   case Guid guid:
                       {
                           yield return client.GetWikiAsync(Project.Name, guid)
                                            .GetResult($"Error getting Wiki with ID {guid}");
                           break;
                       }
                   case string s when !s.IsWildcard():
                       {
                           yield return client.GetWikiAsync(Project.Name, s)
                                            .GetResult($"Error getting Wiki '{s}'");
                           break;
                       }
                   case string s:
                       {
                           foreach (var w in client.GetAllWikisAsync(Project.Name)
                                                .GetResult($"Error getting wiki(s) '{s}'")
                                                .Where(r => r.Name.IsLike(s)))
                           {
                               yield return w;
                           }
                           break;
                       }
                   default:
                       {
                           Logger.LogError(new ArgumentException(nameof(GetWiki.Wiki)));
                           break;
                       }
               }
           }
       }
    }
}