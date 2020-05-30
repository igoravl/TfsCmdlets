/*

.SYNOPSIS
    Creates a new work item in a team project.

.PARAMETER Type
    Represents the name of the work item type to create.

.PARAMETER Title
    Specifies a Title field of new work item type that will be created.

.PARAMETER Fields
    Specifies the fields that are changed and the new values to give to them.
    FieldN The name of a field to update.
    ValueN The value to set on the fieldN.
    [field1=value1[;field2=value2;...]

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

.EXAMPLE
    New-TfsWorkItem -Type Task -Title "Task 1" -Project "MyTeamProject"
    This example creates a new Work Item on Team Project "MyTeamProject".

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemType
    System.String    
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem
{
    [Cmdlet(VerbsCommon.New, "WorkItem", ConfirmImpact = ConfirmImpact.Medium, SupportsShouldProcess = true)]
    //[OutputType(typeof(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem))]
    public class NewWorkItem : BaseCmdlet
    {
        /*
                [Parameter(ValueFromPipeline=true, Mandatory=true, Position=0)]
        public object Type,

                [Parameter(Position=1)]
                public string Title { get; set; }

                [Parameter()]
                public hashtable Fields { get; set; }

                [Parameter()]
                public object Project { get; set; }

                [Parameter()]
                public object Collection { get; set; }

                [Parameter()]
                public SwitchParameter SkipSave { get; set; }

                [Parameter()]
                public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
            {
                if(ShouldProcess(Type, "Create work item of specified type"))
                {
                    wit = Get-TfsWorkItemType -Type Type -Project Project -Collection Collection

                    wi = wit.NewWorkItem()

                    if (Title)
                    {
                        wi.Title = Title
                    }

                    foreach(field in Fields)
                    {
                        wi.Fields[field.Key] = field.Value
                    }

                    if (! SkipSave.IsPresent)
                    {
                        wi.Save()
                    }

                    if (Passthru)
                    {
                        WriteObject(wi); return;
                    }
                }
            }
        }
        */
    }
}
