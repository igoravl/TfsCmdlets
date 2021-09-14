using System;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Changes either the name or the contents of a Global List.
    /// </summary>
    [Cmdlet(VerbsCommon.Rename, "TfsGlobalList", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    [DesktopOnly]
    public class RenameGlobalList : RenameCmdletBase<Models.GlobalList>
    {
        /// <summary>
        /// Specifies the name of the global lsit to be renamed.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }
    }

    partial class GlobalListDataService
    {
        protected override Models.GlobalList DoRenameItem()
        {
            var list = GetItem<Models.GlobalList>();
            var newName = GetParameter<string>(nameof(RenameGlobalList.NewName));
            var tpc = Collection;

            if (!ShouldProcess(tpc, $"Rename global list [{list.Name}] to [{newName}]")) return null;

            var svc = GetService<IGlobalListService>();

            try
            {
                // Import new (renamed) list
                svc.Import(new Models.GlobalList(newName, list.Items));

                // Remove old list
                svc.Remove(new[] { list.Name });
            }
            catch
            {
                if ((GetItem<Models.GlobalList>(new { GlobalList = newName }) != null) &&
                    (GetItem<Models.GlobalList>() != null))
                {
                    svc.Remove(new[] { newName });
                }

                throw;
            }
            
            return list;
        }
    }
}