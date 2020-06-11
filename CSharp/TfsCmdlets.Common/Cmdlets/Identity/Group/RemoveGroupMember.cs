using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    /// <summary>
    /// Removes a member from an Azure DevOps group.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "TfsGroupMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveGroupMember : BaseCmdlet
    {
        /// <summary>
        /// Specifies the member (user or group) to add to the given group.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public object Member { get; set; }

        /// <summary>
        /// Specifies the group to which the member is added.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true)]
        public object Group { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            /*
            var group = GetInstanceOf
            tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

            gi = Get-TfsIdentity -Identity Group -Collection tpc
            ui = Get-TfsIdentity -Identity Identity -Collection tpc

            if(! gi)
            {
                throw new Exception($"Invalid or non-existent group "{Group}"")
            }

            if(! ui)
            {
                throw new Exception($"Invalid or non-existent identity "{Identity}"")
            }

            var client = GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>();

            this.Log($"Removing {{ui}.IdentityType} "$(ui.DisplayName) ($(ui.Properties["Account"]))" from group "$(gi.DisplayName)"");

            if(! ShouldProcess(gi.DisplayName, $"Remove member "{{ui}.DisplayName} ($(ui.Properties["Account"]))""))
            {
                return
            }

            task = client.RemoveMemberFromGroupAsync(gi.Descriptor, ui.Descriptor); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error removing member "{{ui}.DisplayName}" from group "$(gi.DisplayName)"" task.Exception.InnerExceptions })
        */
        }
    }
}