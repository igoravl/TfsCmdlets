using System;
using System.Linq;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    partial class NewGlobalList
    {
        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            var doc = CreateDocument(
                new GlobalList(GlobalList, Items).ToXml()
            );

            var tpc = GetCollection();

            if (!ShouldProcess($"Team Project Collection [{tpc.DisplayName}]", $"Create global list [{GlobalList}]")) return;

            var store = tpc.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();

            if(!Force && GetItems<TfsGlobalList>().Any())
            {
                throw new Exception($"Global List '{GlobalList}' already exists. To overwrite an existing list, use the -Force switch.");
            }

            store.ImportGlobalLists(doc.ToString());

            if(Passthru)
            {
                WriteObject(GetItem<TfsGlobalList>());
            }
        }
    }
}