using System;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using WebApiFolder = Microsoft.TeamFoundation.Build.WebApi.Folder;

namespace TfsCmdlets.Cmdlets.Pipeline.Build.Folder
{
    /// <summary>
    /// Creates a new build/pipeline definition folder
    /// </summary>
    /// <remarks>
    /// Folders are created recursively - i.e. when specifying a path like '\foo\bar\baz', if any of 
    /// the parent folders (foo, foo\bar) does not exist, it is automatically created before creating any
    /// child folders.
    /// </remarks>
    [Cmdlet(VerbsCommon.New, "TfsBuildDefinitionFolder", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [OutputType(typeof(WebApiFolder))]
    public class NewBuildDefinitionFolder : CmdletBase
    {
        /// <summary>
        /// Specifies the path of the new pipeline/build folder, including its name, 
        /// separated by backslashes (\).
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Path")]
        public object Folder { get; set; }

        /// <summary>
        /// Specifies the description of the new build/pipeline folder.
        /// </summary>
        [Parameter()]
        public string Description { get; set; }

        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter()]
        public object Project { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }
        
        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if(string.IsNullOrEmpty(Folder as string))
            {
                throw new ArgumentException($"Invalid folder name '{Folder}'");
            }

            var (_, tp) = GetCollectionAndProject();

            if(!ShouldProcess($"Team Project '{tp.Name}'", $"Create build folder '{Folder}'")) 
            {
                return;
            }
            
            var client = GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

            var newFolder = new WebApiFolder() {
                Description = Description
            };

            var folder = ((string) Folder).Trim('\\');
            var result = client.CreateFolderAsync(newFolder, tp.Name, $@"\{Folder}")
                .GetResult($"Error creating folder '{Folder}'");

            if(Passthru)
            {
                WriteObject(result);
            }
        }
    }
}