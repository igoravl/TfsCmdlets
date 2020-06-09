using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System.Xml.Linq;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    partial class GetGlobalList
    {
    }

    [Exports(typeof(TfsGlobalList))]
    internal class GlobalListDataService : BaseDataService<TfsGlobalList>
    {
        protected override IEnumerable<TfsGlobalList> DoGetItems(object userState)
        {
            var tpc = GetCollection();
            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();
            var doc = store.ExportGlobalLists().ToXDocument();

            var lists = doc.Descendants("GLOBALLIST")
                .Where(e => e.Attribute("name").Value.IsLike(GetParameter<string>("GlobalList")))
                .Select(TfsGlobalList.FromXml)
                .ToList();

            return lists;
       }
    }
}