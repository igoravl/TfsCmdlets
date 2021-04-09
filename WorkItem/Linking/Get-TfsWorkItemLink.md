---
title: Get-TfsWorkItemLink
breadcrumbs: [ "WorkItem", "Linking" ]
parent: "WorkItem.Linking"
description: "Gets the links in a work item. "
remarks: 
parameterSets: 
  "_All_": [ Collection, IncludeAttachments, LinkType, WorkItem ] 
  "__AllParameterSets":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    IncludeAttachments: 
      type: "SwitchParameter"  
    LinkType: 
      type: "WorkItemLinkType" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "LinkType" 
    description: "Returns only the specified link types. When omitted, returns all link types. Possible values: All, Parent, Child, Related, Predecessor, Successor, Duplicate, DuplicateOf, Tests, TestedBy" 
    globbing: false 
    type: "WorkItemLinkType" 
    defaultValue: "All" 
  - name: "IncludeAttachments" 
    description: "Includes attachment information alongside links. When omitted, only links are retrieved. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemRelation" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Linking/Get-TfsWorkItemLink"
aliases: 
examples: 
---
