using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts;

namespace TfsCmdlets.Controllers.Pipeline.ReleaseManagement
{
    /// <summary>
    /// Gets information from one or more release definitions in a team project.
    /// </summary>
    [CmdletController(typeof(ReleaseDefinition))]
    partial class GetReleaseDefinitionController
    {
        protected override IEnumerable Run()
        {
            var client = GetClient<ReleaseHttpClient2>();

            foreach(var input in Definition)
            {
                var definition = input switch {
                    _ => input,
                };

                var expand = DoNotIncludeDetails? 
                    ReleaseDefinitionExpands.LastRelease :
                    ReleaseDefinitionExpands.Artifacts | ReleaseDefinitionExpands.Environments | ReleaseDefinitionExpands.Triggers | ReleaseDefinitionExpands.Variables | ReleaseDefinitionExpands.Tags | ReleaseDefinitionExpands.LastRelease;

                switch (definition)
                {
                    case ReleaseDefinition rd: 
                        yield return rd;
                        break;
                    case string s when s.IsWildcard():
                        yield return client.GetReleaseDefinitionsAsync(Project.Name, expand: expand)
                            .GetResult($"Error getting release definition(s) '{s}'")
                            .Where(r => r.Name.IsLike(s));
                        break;
                    case string s: 
                        yield return client.GetReleaseDefinitionsAsync(Project.Name, searchText: s, expand: expand)
                            .GetResult($"Error getting release definition '{s}'");
                        break;
                }
            }
        }
    }
}