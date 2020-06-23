using System;
using System.IO;
using IOPath = System.IO.Path;
using System.Management.Automation;
using Microsoft.TeamFoundation.Server;
using System.IO.Compression;
using System.Xml.Linq;

namespace TfsCmdlets.Cmdlets.ProcessTemplate
{
    partial class ImportProcessTemplate
    {
        /// <inheritdoc/>
        protected override void DoProcessRecord()
        {
            var dir = ResolvePath(this.Path, ".");
            var processXmlFile = ResolvePath(dir, "ProcessTemplate.xml");

            if(!File.Exists(processXmlFile))
            {
                throw new ArgumentException($"Invalid path. Source path '{Path}' must be a directory and must contain a file named ProcessTemplate.xml.");
            }

            var tpc = GetCollection();

            var doc = XDocument.Load(processXmlFile);
            var name = doc.Element("ProcessTemplate").Element("metadata").Element("name").Value;
            var desc = doc.Element("ProcessTemplate").Element("metadata").Element("description").Value;
            var metadata = doc.Element("ProcessTemplate").Element("metadata").ToString();

            if(!ShouldProcess(tpc.DisplayName, $"Import process template '{name}' from '{Path}'")) return;

            var processTemplateSvc = tpc.GetService<IProcessTemplates>();

            var tempFile = IOPath.GetTempFileName();
            var zipFile = $"{tempFile}.zip";
            File.Move(tempFile, IOPath.GetFileName(zipFile));

            ZipFile.CreateFromDirectory(dir, zipFile);

            processTemplateSvc.AddUpdateTemplate(name, desc, metadata, State, zipFile);

            File.Delete(zipFile);
        }
    }
}