#if NET471_OR_GREATER
using TfsCmdlets.Cmdlets.ProcessTemplate;
using Microsoft.TeamFoundation.Server;
using System.Xml;
using System.Xml.Linq;
using System.IO.Compression;
#endif

namespace TfsCmdlets.Controllers.ProcessTemplate
{
    [CmdletController]
    partial class ImportProcessTemplateController
    {
        protected override IEnumerable Run()
        {
#if NET471_OR_GREATER
            var path = Parameters.Get<string>(nameof(ImportProcessTemplate.Path));
            var state = Parameters.Get<string>(nameof(ImportProcessTemplate.State));

            var dir = PowerShell.ResolvePath(path, ".");
            var processXmlFile = PowerShell.ResolvePath(dir, "ProcessTemplate.xml");

            if (!File.Exists(processXmlFile))
            {
                PowerShell.WriteError(new ArgumentException($"Invalid path. Source path '{path}' must be a directory and must contain a file named ProcessTemplate.xml."));
                return null;
            }

            var tpc = Data.GetCollection();

            var doc = XDocument.Load(processXmlFile);
            var name = doc.Element("ProcessTemplate").Element("metadata").Element("name").Value;
            var desc = doc.Element("ProcessTemplate").Element("metadata").Element("description").Value;
            var metadata = doc.Element("ProcessTemplate").Element("metadata").ToString();

            if (!PowerShell.ShouldProcess(tpc.DisplayName, $"Import process template '{name}' from '{path}'")) return null;

            var processTemplateSvc = Data.GetService<IProcessTemplates>();

            var tempFile = System.IO.Path.GetTempFileName();
            var zipFile = $"{tempFile}.zip";
            File.Move(tempFile, System.IO.Path.GetFileName(zipFile));

            ZipFile.CreateFromDirectory(dir, zipFile);

            processTemplateSvc.AddUpdateTemplate(name, desc, metadata, state, zipFile);

            File.Delete(zipFile);
#endif
            return null;
        }
    }
}