using System.Management.Automation;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace TfsCmdlets.Cmdlets.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets information from one or more release definitions in a team project.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(ReleaseDefinition))]
    partial class GetReleaseDefinition
    {
        /// <summary>
        /// Specifies the release definition to get information from.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name")]
        public object Definition { get; set; } = "*";
    }

    [CmdletController(typeof(ReleaseDefinition), Client = typeof(IReleaseHttpClient2))]
    partial class GetReleaseDefinitionController
    {
        protected override IEnumerable Run()
        {
            foreach(var input in Definition)
            {
                var definition = input switch {
                    _ => input,
                };

                switch (definition)
                {
                    case ReleaseDefinition rd: 
                        yield return rd;
                        break;
                    case string s when s.IsWildcard():
                        yield return Client.GetReleaseDefinitionsAsync(Project.Name)
                            .GetResult($"Error getting release definition(s) '{s}'")
                            .Where(r => r.Name.IsLike(s));
                        break;
                    case string s: 
                        yield return Client.GetReleaseDefinitionsAsync(Project.Name, searchText: s)
                            .GetResult($"Error getting release definition '{s}'");
                        break;
                }
            }
        }
    }
}