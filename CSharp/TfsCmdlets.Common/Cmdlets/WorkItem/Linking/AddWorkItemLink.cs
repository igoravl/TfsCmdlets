/*
.SYNOPSIS
Add a link between two work items

.DESCRIPTION
Add-TfsWorkItemLink permits the creation of a link between two work items. By linking work items and other objects, you can track related work, dependencies, and changes made over time. All links are defined with a specific link type. For example, you can use Parent/Child links to link work items to support a hierarchical tree structure. 

.PARAMETER SourceWorkItem
The first work item (the "leftmost") in a link relationship. For instance, in a Parent-Child relationship, that would be the Parent.

.PARAMETER TargetWorkItem
The second work item (the "rightmost") in a link relationship. For instance, in a Parent-Child relationship, that would be the Child.

.PARAMETER EndLinkType
Determines the kind of link to be created. This argument always refer to the Target (or "end") work item, in order to define directionality. In other words, to create a Parent-Child between a Source (parent) and a Target (child) work items, EndLinkType would be "Child". To learn more about the supported link types, see Get-TfsWorkItemLinkEndType.

.PARAMETER Comment
Add a comment to a link. Link comments can be seen in a work item form"s "Links" tab.

.PARAMETER SkipSave
Leaves the source work item in a "dirty" (unsaved) state, by not calling its Save() method. It is useful for when subsequents changes need to be made to the work item object before saving it. In that case, it is up to the user to later invoke the Save() method on the source work item object to persist the newly created link.

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
Microsoft.TeamFoundation.WorkItemTracking.Client.Project
System.String

.EXAMPLE
Add-TfsWorkItemLink -SourceWorkItem 1 -TargetWorkItem 2 -EndLinkType Child
Creates a parent-child relationship between work items with IDs #1 (source, parent) and #2 (target, child)

.LINK
https://www.visualstudio.com/en-us/docs/work/track/link-work-items-support-traceability

.LINK
Get-TfsWorkItemLinkEndType
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.Linking
{
    [Cmdlet(VerbsCommon.Add, "WorkItemLink")]
    public class AddWorkItemLink: BaseCmdlet
    {
/*
        [Parameter(Position=0, Mandatory=true, ValueFromPipeline=true)]
        [Alias("Id")]
        [Alias("From")]
        [ValidateNotNull()]
        public object SourceWorkItem { get; set; }

        [Parameter(Position=1, Mandatory=true)]
        [Alias("To")]
        [ValidateNotNull()]
        public object TargetWorkItem { get; set; }

        [Parameter(Position=2, Mandatory=true)]
        [Alias("LinkType")]
        [Alias("Type")]
        public object EndLinkType { get; set; }

        [Parameter()]
        public string Comment { get; set; }

        public SwitchParameter SkipSave { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        sourceWi = Get-TfsWorkItem -WorkItem SourceWorkItem -Collection Collection -Project Project
        targetWi = Get-TfsWorkItem -WorkItem TargetWorkItem -Collection Collection -Project Project

        
        if (EndLinkType -isnot [Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLinkTypeEnd])
        {
            try
            {
                EndLinkType = sourceWi.Store.WorkItemLinkTypes.LinkTypeEnds[EndLinkType]
            }
            catch
            {
                throw new Exception($"Error retrieving work item link type {EndLinkType}`: _")
            }
        }

        link = new Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemLink(EndLinkType, targetWi.Id)
        link.Comment = Comment

        i = sourceWi.WorkItemLinks.Add(link)

        if (! SkipSave)
        {
            sourceWi.Save()
        }

        WriteObject(sourceWi.WorkItemLinks[i]        ); return;
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
