/*
.SYNOPSIS
Sets the contents of one or more work items.

.PARAMETER SkipSave
Leaves the work item in a "dirty" (unsaved) state, by not calling its Save() method. It is useful for when subsequents changes need to be made to the work item object before saving it. In that case, it is up to the user to later invoke the Save() method on the work item object to persist the changes.

.PARAMETER Project
Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem
System.Int32
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    [Cmdlet(VerbsCommon.Set, "TfsWorkItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    public class SetWorkItem : BaseCmdlet
    {
        /*
                [Parameter(ValueFromPipeline=true, Position=0)]
                [Alias("id")]
                [ValidateNotNull()]
                public object WorkItem { get; set; }

                [Parameter(Position=1)]
                public hashtable Fields { get; set; }

                [Parameter()]
                public SwitchParameter BypassRules { get; set; }

                [Parameter()]
                [SwitchParameter] 
                SkipSave,

                [Parameter()]
                public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if (WorkItem is Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem)
                {
                    tpc = WorkItem.Store.TeamProjectCollection
                    id = WorkItem.Id
                }
                else
                {
                    tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})
                    id = (Get-TfsWorkItem -WorkItem WorkItem -Collection Collection).Id
                }

                if (BypassRules)
                {
                    store = new Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore(tpc, Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStoreFlags.BypassRules)
                }
                else
                {
                    store = tpc.GetService([type]"Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore")
                }

                wi = store.GetWorkItem(id)

                Fields = _FixAreaIterationValues -Fields Fields -ProjectName wi.Project.Name

                if(ShouldProcess($"Set work item fields {{Fields}.Keys -join ", "} to $(Fields.Values -join ", "), respectively"))
                {
                    foreach(fldName in Fields.Keys)
                    {
                        wi.Fields[fldName].Value = Fields[fldName]
                    }

                    if(! SkipSave)
                    {
                        wi.Save()
                    }
                }

                WriteObject(wi); return;
            }
        }
        */
    }
}
