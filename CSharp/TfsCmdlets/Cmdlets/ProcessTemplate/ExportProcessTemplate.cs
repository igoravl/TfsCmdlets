using System.Management.Automation;
#if NET471_OR_GREATER
using TfsCmdlets.Cmdlets.ProcessTemplate;
using Microsoft.TeamFoundation.Server;
using System.Xml;
using System.Xml.Linq;
using System.IO.Compression;
#endif

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
    [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true)]
    partial class ExportProcessTemplate 
    {
        /// <summary>
        /// Specifies the name of the process template(s) to be exported. Wildcards are supported. 
        /// When omitted, all process templates in the given project collection are exported.
        /// </summary>
        [Parameter(Position = 0)]
        [SupportsWildcards()]
        [Alias("Name", "Process")]
        public object ProcessTemplate { get; set; } = "*";

        /// <summary>
        /// Path to the target directory where the exported process template (and related files) will be saved. 
        /// A folder with the process template name will be created under this path. When omitted, templates  
        /// are exported in the current directory.
        /// </summary>
        [Parameter]
        public string DestinationPath { get; set; }

        /// <summary>
        /// Saves the exported process template with a new name. Useful when exporting a base template 
        /// which will be used as a basis for a new process template. When omitted, the original name is used.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty()]
        public string NewName { get; set; }

        /// <summary>
        /// Saves the exported process template with a new description. Useful when exporting a base template 
        /// which will be used as a basis for a new process template.  When omitted, the original description is used.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty()]
        public string NewDescription { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing destination folder.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    [CmdletController]
    partial class ExportProcessTemplateController
    {
        protected override IEnumerable Run()
        {
#if NET471_OR_GREATER
            var processTemplate = Parameters.Get<object>(nameof(ExportProcessTemplate.ProcessTemplate));
            var newName = Parameters.Get<string>(nameof(ExportProcessTemplate.NewName));
            var newDescription = Parameters.Get<string>(nameof(ExportProcessTemplate.NewDescription));
            var destinationPath = Parameters.Get<string>(nameof(ExportProcessTemplate.DestinationPath));
            var force = Parameters.Get<bool>(nameof(ExportProcessTemplate.Force));

           var tpc = Data.GetCollection();
           var processTemplateSvc = Data.GetService<Microsoft.TeamFoundation.Server.IProcessTemplates>();
           IList<TemplateHeader> templates = Data.GetItems<TemplateHeader>().ToList();

           if (templates.Count == 0) return null;

           if ((!string.IsNullOrEmpty(newName) || !string.IsNullOrEmpty(newDescription)) && templates.Count > 1)
           {
               PowerShell.WriteError(new ArgumentException($"Cannot specify a new name or a new description when " +
                   $"exporting multiple processes. The search criteria '{processTemplate}' matched the " +
                   $"following processes: {string.Join(", ", templates.Select(th => $"'{th.Name}'"))}"));
                return null;
           }
           else
           {
               templates[0].Name = newName ?? templates[0].Name;
               templates[0].Description = newDescription ?? templates[0].Description;
           }

           foreach (var template in templates)
           {
               if (!PowerShell.ShouldProcess($"Team Project Collection '{tpc.DisplayName}'",
                   $"Export process template '{template.Name}'"))
               {
                   continue;
               }

               var outDir = Path.Combine(destinationPath ?? PowerShell.GetCurrentDirectory(), template.Name);

               if (Directory.Exists(outDir) && Directory.GetFileSystemEntries(outDir).Length > 0)
               {
                   if (!force && !PowerShell.ShouldContinue($"Overwrite destination directory {outDir}? All its content will be lost.", "Delete directory"))
                   {
                       continue;
                   }

                   Directory.Delete(outDir, true);
               }

               Directory.CreateDirectory(outDir);

               var tempFile = processTemplateSvc.GetTemplateData(template.TemplateId);
               var zipFile = $"{tempFile}.zip";

               File.Move(tempFile, zipFile);

               ZipFile.ExtractToDirectory(zipFile, outDir);

               var ptFile = Path.Combine(outDir, "ProcessTemplate.xml");
               var ptXml = XDocument.Load(ptFile);

               ptXml.Element("ProcessTemplate").Element("metadata").Element("name").Value = template.Name;
               ptXml.Element("ProcessTemplate").Element("metadata").Element("description").Value = template.Description;
               ptXml.Save(ptFile);

               File.Delete(zipFile);
         }
#endif
            return null;
        }
    }
}