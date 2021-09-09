using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Deletes one or more Global Lists.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGlobalList", SupportsShouldProcess = true)]
    [DesktopOnly]
    public class RemoveGlobalList : RemoveCmdletBase<Models.GlobalList>
    {
        /// <summary>
        /// Specifies the name of global list to be deleted. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string GlobalList { get; set; }

        /// <summary>
        /// HELP_PARAM_FORCE_REMOVE
        /// </summary>
        [Parameter()]
        public SwitchParameter Force { get; set; }
    }

    partial class GlobalListDataService
    {
        protected override void DoRemoveItem()
        {
            var tpc = GetCollection();
            var force = GetParameter<bool>(nameof(RemoveGlobalList.Force));

            var listsToRemove = new List<string>();

            foreach (var l in GetItems<Models.GlobalList>())
            {
                if (!ShouldProcess($"Team Project Collection '{tpc.DisplayName}'", $"Delete global list '{l.Name}'")) continue;
                if (!force && !ShouldContinue($"Are you sure you want to delete global list '{l.Name}'?")) continue;

                listsToRemove.Add(l.Name);
            }

            if (listsToRemove.Count == 0) return;

            var svc = GetService<IGlobalListService>();
            svc.Remove(listsToRemove);
        }
    }
}