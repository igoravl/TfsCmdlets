using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using System.Xml.Linq;
using TfsCmdlets.Extensions;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    partial class ImportGlobalList
    {
        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            bool done = false;
            XDocument doc = null;

            while (!done) switch (InputObject)
                {
                    case PSObject pso:
                        {
                            InputObject = pso.BaseObject;
                            continue;
                        }
                    case string s:
                        {
                            doc = XDocument.Parse(s);
                            done = true;
                            break;
                        }
                    case XmlDocument xmlDoc:
                        {
                            doc = xmlDoc.ToXDocument();
                            done = true;
                            break;
                        }
                    case XDocument xDoc:
                        {
                            doc = new XDocument(xDoc);
                            done = true;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Supplied input object is not a valid XML document");
                        }
                }

            var existingLists = GetCollectionOf<TfsGlobalList>(new ParameterDictionary(){["GlobalList"]="*"}).ToList();
            var operations = new Dictionary<string, string>();

            foreach (XElement list in doc.Root.Descendants("GLOBALLIST"))
            {
                var name = list.Attribute("name").Value;
                operations.Add(name, existingLists.Any(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) ?
                    "Overwrite" : "Import");
            }

            var tpc = GetCollection();

            foreach (var kvp in operations)
            {
                if (ShouldProcess($"Team Project Collection [{tpc.DisplayName}]",
                    $"{kvp.Value} global list [{kvp.Key}]")) continue;

                // Remove skipped list from import list

                doc.Descendants("GLOBALLIST")
                    .Where(e => e.Attribute("name").Value.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase))
                    .Remove();
            }

            if(doc.Elements().Count() == 0) return;

            if(!Force && operations.ContainsValue("Overwrite"))
            {
                var listNames = string.Join(", ", operations.Where(kvp=>kvp.Value.Equals("Overwrite")).Select(kvp=>$"'{kvp.Key}'"));
                throw new Exception($"Global List(s) {listNames} already exist. To overwrite an existing list, use the -Force switch.");
            }

            Import(doc);
        }
    }
}