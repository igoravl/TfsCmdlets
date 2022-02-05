
/*

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    [Exports(typeof(IGlobalListService))]
    internal class GlobalListServiceImpl: BaseService, IGlobalListService
    {
        public GlobalListCollection Export()
        {
            var tpc = GetCollection();

#pragma warning disable CS0618
            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
#pragma warning restore CS0618

            var doc = store.ExportGlobalLists().ToXDocument();

            doc.Descendants("GLOBALLIST")
                .Where(e => !e.Attribute("name").Value.IsLike(GetParameter<string>("GlobalList")))
                .Remove();

            return doc;
        }

        public void Import(GlobalListCollection lists)
        {
            var tpc = GetCollection();

#pragma warning disable CS0618
            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
#pragma warning restore CS0618

            store.ImportGlobalLists(lists.ToString());
        }

        public void Import(Models.GlobalList list)
        {
            ((IGlobalListService)this).Import(new GlobalListCollection(list));
        }

        public void Remove(IEnumerable<string> listNames)
        {
            var doc = new XDocument(
                new XElement("Package",
                    listNames.Select(s => new XElement("DestroyGlobalList",
                        new XAttribute("ListName", $"*{s}"),
                        new XAttribute("ForceDelete", $"true")))
                )
            );

            var tpc = GetCollection();

#pragma warning disable CS0618
            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
#pragma warning restore CS0618

            store.SendUpdatePackage(doc.ToXmlDocument().DocumentElement, out var _, false);
        }
    }
}


*/