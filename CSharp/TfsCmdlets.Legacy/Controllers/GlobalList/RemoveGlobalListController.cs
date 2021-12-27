// using System.Management.Automation;

// namespace TfsCmdlets.Cmdlets.GlobalList
// {
//     /// <summary>
//     /// Deletes one or more Global Lists.
//     /// </summary>
//     [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true, SupportsShouldProcess = true)]
//     partial class RemoveGlobalList 
//     {
//         /// <summary>
//         /// Specifies the name of global list to be deleted. Wildcards are supported.
//         /// </summary>
//         [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
//         [Alias("Name")]
//         [SupportsWildcards()]
//         public string GlobalList { get; set; }

//         /// <summary>
//         /// HELP_PARAM_FORCE_REMOVE
//         /// </summary>
//         [Parameter]
//         public SwitchParameter Force { get; set; }
//     }

//     // TODO

//     //partial class GlobalListDataService
//     //{
//     //    protected override void DoRemoveItem()
//     //    {
//     //        var tpc = Collection;
//     //        var force = parameters.Get<bool>(nameof(RemoveGlobalList.Force));

//     //        var listsToRemove = new List<string>();

//     //        foreach (var l in GetItems<Models.GlobalList>())
//     //        {
//     //            if (!PowerShell.ShouldProcess($"Team Project Collection '{tpc.DisplayName}'", $"Delete global list '{l.Name}'")) continue;
//     //            if (!force && !PowerShell.ShouldContinue($"Are you sure you want to delete global list '{l.Name}'?")) continue;

//     //            listsToRemove.Add(l.Name);
//     //        }

//     //        if (listsToRemove.Count == 0) return;

//     //        var svc = GetService<IGlobalListService>();
//     //        svc.Remove(listsToRemove);
//     //    }
//     //}
// }