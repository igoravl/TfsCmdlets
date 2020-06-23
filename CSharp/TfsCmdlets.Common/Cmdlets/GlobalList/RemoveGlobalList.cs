using System;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Deletes one or more Global Lists.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGlobalList", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [DesktopOnly]
    public class RemoveGlobalList : CmdletBase
    {
        /// <summary>
        /// Specifies the name of global list to be deleted. Wildcards are supported.
        /// </summary>
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string GlobalList { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var list = GetItem<Models.GlobalList>();

            if (list == null)
            {
                throw new ArgumentException($"Invalid or non-existent global list '{GlobalList}'");
            }

            var tpc = GetCollection();

            if (!ShouldProcess($"Team Project Collection '{tpc.DisplayName}'", $"Delete global list '{list.Name}'")) return;

            GetService<IGlobalListService>().Remove(new[]{list.Name});
        }
    }
}