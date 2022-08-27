using TfsCmdlets.Cmdlets.Pipeline.Build.Folder;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Controllers.Pipeline.Build.Folder
{
    [CmdletController(typeof(WebApiFolder))]
    partial class NewBuildDefinitionFolderController
    {
        protected override IEnumerable Run()
        {
            var folder = Parameters.Get<string>(nameof(NewBuildDefinitionFolder.Folder))?.Trim('\\');
            var description = Parameters.Get<string>(nameof(NewBuildDefinitionFolder.Description));

            if (string.IsNullOrEmpty(folder))
            {
                throw new ArgumentException($"Invalid folder name '{folder}'");
            }

            var tp = Data.GetProject();

            if (!PowerShell.ShouldProcess($"Team Project '{tp.Name}'", $"Create build folder '{folder}'"))
            {
                yield break;
            }

            var client = Data.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

            var newFolder = new WebApiFolder()
            {
                Description = description
            };

            var result = client.CreateFolderAsync(newFolder, tp.Name, $@"\{folder}")
                .GetResult($"Error creating folder '{folder}'");

            yield return result;
        }
    }
}