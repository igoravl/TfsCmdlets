// using System.Management.Automation;

// namespace TfsCmdlets.Cmdlets.GlobalList
// {
//     /// <summary>
//     /// Imports one or more Global Lists from an XML document
//     /// </summary>
//     /// <remarks>
//     /// This cmdletsimports an XML containing one or more global lists and their respective items, 
//     /// in the same format used by witadmin. It is functionally equivalent to "witadmin importgloballist"
//     /// </remarks>
//     /// <example>
//     ///   <code>Get-Content gl.xml | Import-GlobalList</code>
//     ///   <para>Imports the contents of an XML document called gl.xml to the current project collection</para>
//     /// </example>
//     /// <notes>
//     /// To import global lists, you must be a member of the Project Collection Administrators security group.
//     /// </notes>
//     [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true, SupportsShouldProcess = true)]
//     partial class ImportGlobalList 
//     {
//         /// <summary>
//         /// XML document object containing one or more global list definitions.
//         /// </summary>
//         [Parameter(Mandatory = true, ValueFromPipeline = true)]
//         [Alias("Xml")]
//         public object InputObject { get; set; }

//         /// <summary>
//         /// Allows the cmdlet to overwrite a global list that already exists.
//         /// </summary>
//         [Parameter]
//         public SwitchParameter Force { get; set; }

//         // TODO

//         ///// <summary>
//         ///// Performs execution of the command
//         ///// </summary>
//         //protected override void DoProcessRecord()
//         //{
//         //    bool done = false;
//         //    XDocument doc = null;

//         //    while (!done) switch (InputObject)
//         //        {
//         //            case PSObject pso:
//         //                {
//         //                    InputObject = pso.BaseObject;
//         //                    continue;
//         //                }
//         //            case string s:
//         //                {
//         //                    doc = XDocument.Parse(s);
//         //                    done = true;
//         //                    break;
//         //                }
//         //            case XmlDocument xmlDoc:
//         //                {
//         //                    doc = xmlDoc.ToXDocument();
//         //                    done = true;
//         //                    break;
//         //                }
//         //            case XDocument xDoc:
//         //                {
//         //                    doc = new XDocument(xDoc);
//         //                    done = true;
//         //                    break;
//         //                }
//         //            default:
//         //                {
//         //                    throw new ArgumentException("Supplied input object is not a valid XML document");
//         //                }
//         //        }

//         //    var importList = new Models.GlobalListCollection(doc);
//         //    var existingLists = Data.GetItems<Models.GlobalList>(new { GlobalList = "*" });
//         //    var operations = new Dictionary<string, string>();

//         //    foreach (var list in importList)
//         //    {
//         //        operations.Add(list.Name, existingLists.Any(l => l.Name.Equals(list.Name, StringComparison.OrdinalIgnoreCase)) ?
//         //            "Overwrite" : "Import");
//         //    }

//         //    var tpc = Collection;

//         //    foreach (var kvp in operations)
//         //    {
//         //        if (ShouldProcess($"Team Project Collection [{tpc.DisplayName}]",
//         //            $"{kvp.Value} global list [{kvp.Key}]")) continue;

//         //        // Remove skipped list from import list

//         //        importList.RemoveAll(l => l.Name.Equals(kvp.Key, StringComparison.OrdinalIgnoreCase));
//         //    }

//         //    if (importList.Count == 0) return;

//         //    if (!Force && operations.ContainsValue("Overwrite"))
//         //    {
//         //        var listNames = string.Join(", ", operations.Where(kvp => kvp.Value.Equals("Overwrite")).Select(kvp => $"'{kvp.Key}'"));
//         //        throw new Exception($"Global List(s) {listNames} already exist. To overwrite an existing list, use the -Force switch.");
//         //    }

//         //    GetService<IGlobalListService>().Import(importList);
//         //}
//     }
// }