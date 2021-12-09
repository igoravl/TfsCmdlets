using TfsCmdlets.Cmdlets.ProcessTemplate;
using System.IO.Compression;
using System.Xml.Linq;

#if NET471_OR_GREATER
using Microsoft.TeamFoundation.Server;
#endif

namespace TfsCmdlets.Controllers.ProcessTemplate
{
    [CmdletController]
    partial class ExportProcessTemplateController
    {
        public override object InvokeCommand()
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