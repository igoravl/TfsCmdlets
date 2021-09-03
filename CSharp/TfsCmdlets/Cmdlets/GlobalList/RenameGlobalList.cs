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
    public class RenameGlobalList : CmdletBase
    {
        /// <summary>
        /// Specifies the name of the global lsit to be renamed.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }

        /// <summary>
        /// HELP_PARAM_NEWNAME
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public string NewName { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void DoProcessRecord()
        {
            var list = GetItem<Models.GlobalList>();

            if(list == null)
            {
                throw new ArgumentException($"Invalid or non-existent global list [{GlobalList}]");
            }

            var tpc = GetCollection();

            if(!ShouldProcess($"Team Project Collection [{tpc.DisplayName}]", $"Rename global list [{list.Name}] to [{NewName}]")) return;

            var svc = GetService<IGlobalListService>();

            try
            {

                // Import new (renamed) list
                svc.Import(new Models.GlobalList(NewName, list.Items));

                // Remove old list
                svc.Remove(new[]{list.Name});
            }
            catch
            {
                if((GetItem<Models.GlobalList>(new {GlobalList = NewName}) != null) &&
                    (GetItem<Models.GlobalList>() != null))
                {
                    svc.Remove(new[]{NewName});
                }

                throw;
            }

            if(Passthru)
            {
                WriteObject(list);
            }
        }
    }
}