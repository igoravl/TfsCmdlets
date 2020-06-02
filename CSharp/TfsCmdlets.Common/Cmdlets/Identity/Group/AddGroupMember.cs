using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet(VerbsCommon.Add, "GroupMember", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class AddGroupMember : BaseCmdlet
    {
        /*
                # Specifies the board name(s). Wildcards accepted
                [Parameter(Position=0)]
                [Alias("Name")]
                [Alias("Member")]
                [Alias("User")]
                public object Identity { get; set; }

                [Parameter(ValueFromPipeline=true)]
                public object Group { get; set; }

                [Parameter()]
                public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
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

                this.Log($"Adding {{ui}.IdentityType} "$(ui.DisplayName) ($(ui.Properties["Account"]))" to group "$(gi.DisplayName)"");

                if(! ShouldProcess(gi.DisplayName, $"Add member "{{ui}.DisplayName} ($(ui.Properties["Account"]))""))
                {
                    return
                }

                task = client.AddMemberToGroupAsync(gi.Descriptor, ui.Descriptor); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error adding member "{{ui}.DisplayName}" to group "$(gi.DisplayName)"" task.Exception.InnerExceptions })
            }
        }
        */
    }
}
