using Microsoft.TeamFoundation.Build.WebApi;

namespace TfsCmdlets.Controllers.Pipeline.Build
{
    [CmdletController(typeof(WebApiBuild))]
    partial class StartBuildController
    {
        protected override IEnumerable Run()
        {
            var definition = GetItem<BuildDefinitionReference>();

            var client = GetClient<BuildHttpClient>();

            yield return client.QueueBuildAsync(project: Project.Name, build: new WebApiBuild
            {
                Definition = definition
            }).GetResult($"Error queuing build '{definition}'");
        }
    }
}