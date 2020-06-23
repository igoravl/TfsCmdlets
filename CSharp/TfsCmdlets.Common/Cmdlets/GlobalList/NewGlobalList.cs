using System;
using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Creates a new Global List.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "TfsGlobalList", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    [OutputType(typeof(PSCustomObject))]
    [DesktopOnly]
    public class NewGlobalList : CmdletBase
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

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            var newList = new Models.GlobalList(GlobalList, Items).ToXml();
            var existingList = GetItem<Models.GlobalList>();
            var tpc = GetCollection();

            if (!ShouldProcess($"Team Project Collection [{tpc.DisplayName}]", 
                $"{(existingList != null? "Overwrite": "Create")} global list [{GlobalList}]")) return;

            if (!Force && existingList != null)
            {
                throw new Exception($"Global List '{GlobalList}' already exist. To overwrite an existing list, use the -Force switch.");
            }

            GetService<IGlobalListService>().Import(newList);

            if(Passthru)
            {
                WriteObject(newList);
            }
        }
    }
}