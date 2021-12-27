using System;
using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Cmdlets.GlobalList;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.GlobalList
{
    [CmdletController]
    partial class ExportGlobalListController
    {
        private IWorkItemStore Store { get; set; }

        public override object InvokeCommand()
        {
            var lists = Data.GetItems<Models.GlobalList>();
            var encoding = Parameters.Get<string>(nameof(ExportGlobalList.Encoding));
            var asXml = Parameters.Get<bool>(nameof(ExportGlobalList.AsXml));
            var force = Parameters.Get<bool>(nameof(ExportGlobalList.Force));
            var destination = Parameters.Get<string>(nameof(ExportGlobalList.Destination));

            var tpc = Data.GetCollection();

            var result = new List<string>();

            foreach (var list in lists)
            {
                if (!PowerShell.ShouldProcess(tpc, $"Export global list '{list.Name}'")) continue;

                if (asXml)
                {
                    result.Add(list.ToString());
                    continue;
                }

                var relativePath = $"{list.Name}.xml";
                var outputPath = PowerShell.ResolvePath(destination, relativePath);
                var destDir = Path.GetDirectoryName(outputPath);

                if (!Directory.Exists(destDir))
                {
                    Logger.Log($"Destination path '{destination}' not found.");

                    if (!PowerShell.ShouldProcess(destination, "Create output directory")) continue;

                    Directory.CreateDirectory(destDir);
                }

                if (File.Exists(outputPath) && !(force || PowerShell.ShouldContinue($"Are you sure you want to overwrite existing file '{outputPath}'", "Confirm")))
                {
                    Logger.LogWarn($"Cannot overwrite existing file '{outputPath}'");
                    continue;
                }

                try
                {
                    File.WriteAllText(outputPath, list.ToString(), Encoding.GetEncoding(encoding));
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }

            return result;
        }
    }
}