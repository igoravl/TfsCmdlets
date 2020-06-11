using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.TeamFoundation.Server;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    partial class ExportProcessTemplate
    {
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var tpc = GetCollection();
            var processTemplateSvc = tpc.GetService<Microsoft.TeamFoundation.Server.IProcessTemplates>();
            IList<TemplateHeader> templates = GetItems<TemplateHeader>().ToList();

            if (templates.Count == 0) return;

            if ((!string.IsNullOrEmpty(NewName) || !string.IsNullOrEmpty(NewDescription)) && templates.Count > 1)
            {
                throw new ArgumentException($"Cannot specify a new name or a new description when " +
                    $"exporting multiple processes. The search criteria '{ProcessTemplate}' matched the " +
                    $"following processes: {string.Join(", ", templates.Select(th => $"'{th.Name}'"))}");
            }
            else
            {
                templates[0].Name = NewName ?? templates[0].Name;
                templates[0].Description = NewDescription ?? templates[0].Description;
            }

            foreach (var template in templates)
            {
                if (!ShouldProcess($"Team Project Collection '{tpc.DisplayName}'",
                    $"Export process template '{template.Name}'"))
                {
                    continue;
                }

                var outDir = Path.Combine(DestinationPath ?? GetCurrentDirectory(), template.Name);
                

                if(Directory.Exists(outDir) && Directory.GetFileSystemEntries(outDir).Length > 0)
                {
                    if(!Force && !ShouldContinue($"Overwrite destination directory {outDir}? All its content will be lost.", "Delete directory"))
                    {
                        return;
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
        }
    }
}