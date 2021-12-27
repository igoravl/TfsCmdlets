using System;
using System.Collections.Generic;
using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Controllers.GlobalList
{
    [CmdletController(typeof(Models.GlobalList))]
    partial class GetGlobalList 
    {
        [Import]
        private IWorkItemStore Store { get; set; }

        public override IEnumerable<Models.GlobalList> Invoke()
        {
            var globalList = Parameters.Get<string>("GlobalList");
            var tpc = Data.GetCollection();

            return Store
                .ExportGlobalLists()
                .Descendants("GLOBALLIST")
                .Where(el => el.Attribute("name").Value.IsLike(globalList))
                .Select(el => new Models.GlobalList(el));
        }
    }
}
