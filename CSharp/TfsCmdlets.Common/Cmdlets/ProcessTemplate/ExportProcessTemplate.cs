using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    /// <summary>
    /// Exports a XML-based process template definition to disk.
    /// </summary>
    /// <remarks>
    /// This cmdlet offers a functional replacement to the "Export Process Template" feature found 
    /// in Team Explorer. All files pertaining to the specified process template (work item defininitons, 
    /// reports, saved queries, process configuration and so on) are downloaded from the given 
    /// Team Project Collection and saved in a local directory, preserving the directory structure 
    /// required to later re-import it. This is specially handy to do small changes to a process template 
    /// or to create a new process template based on an existing one.
    /// </remarks>
    /// <example>
    ///   <code>Export-TfsProcessTemplate -Process "Scrum" -DestinationPath C:\PT -Collection http://vsalm:8080/tfs/DefaultCollection</code>
    ///   <para>Exports the Scrum process template from the DefaultCollection project collection in the VSALM server, saving the template files to the C:\PT\Scrum directory in the local computer.</para>
    /// </example>
    /// <example>
    ///   <code>Export-TfsProcessTemplate -Process "Scrum" -DestinationPath C:\PT -Collection http://vsalm:8080/tfs/DefaultCollection -NewName "MyScrum" -NewDescription "A customized version of the Scrum process template"</code>
    ///   <para>Exports the Scrum process template from the DefaultCollection project collection in the VSALM server, saving the template files to the C:\PT\MyScrum directory in the local computer. Notice that the process template is being renamed from Scrum to MyScrum, so that it can be later reimported as a new process template instead of overwriting the original one.</para>
    /// </example>
    [Cmdlet(VerbsData.Export, "TfsProcessTemplate")]
    [DesktopOnly]
    public partial class ExportProcessTemplate : BaseCmdlet
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be exported. Wildcards supported. 
        /// When omitted, all process templates in the given project collection are exported.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        public object ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// Path to the target directory where the exported process template (and related files) will be saved. 
        /// A folder with the process template name will be created under this path. When omitted, templates  
        /// are exported in the current directory.
        /// </summary>
        [Parameter()]
        public string DestinationPath { get; set; }

        /// <summary>
        /// Saves the exported process template with a new name. Useful when exporting a base template 
        /// which will be used as a basis for a new process template. When omitted, the original name is used.
        /// </summary>
        [Parameter()]
        [ValidateNotNullOrEmpty()]
        public string NewName { get; set; }

        /// <summary>
        /// Saves the exported process template with a new description. Useful when exporting a base template 
        /// which will be used as a basis for a new process template.  When omitted, the original description is used.
        /// </summary>
        [Parameter()]
        [ValidateNotNullOrEmpty()]
        public string NewDescription { get; set; }


        /// <summary>
        /// Allows the cmdlet to overwrite an existing destination folder.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }
    }
}