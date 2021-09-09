using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Creates a new Global List.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsGlobalList", SupportsShouldProcess = true)]
    [OutputType(typeof(PSCustomObject))]
    [DesktopOnly]
    public class NewGlobalList : NewCmdletBase<Models.GlobalList>
    {
        /// <summary>
        /// Specifies the name of the new global list.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }

        /// <summary>
        /// Specifies the contents (items) of the new global list.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string[] Items { get; set; }

        /// <summary>
        /// Allows the cmdlet to overwrite an existing global list.
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class GlobalListDataService
{
        protected override Models.GlobalList DoNewItem()
        {
            var name = GetParameter<string>(nameof(NewGlobalList.GlobalList));
            var items = GetParameter<IEnumerable<string>>(nameof(NewGlobalList.Items));
            var force = GetParameter<bool>(nameof(NewGlobalList.Force));

            var newList = new Models.GlobalList(name, items).ToXml();
            var hasList = TestItem<Models.GlobalList>();

            var tpc = GetCollection();

            if (!ShouldProcess(tpc, $"{(hasList? "Overwrite": "Create")} global list [{name}]")) return null;

            if (!force && hasList) throw new Exception($"Global List '{name}' already exists. To overwrite an existing list, use the -Force switch.");

            GetService<IGlobalListService>().Import(newList);

            return newList;
        }
    }
}