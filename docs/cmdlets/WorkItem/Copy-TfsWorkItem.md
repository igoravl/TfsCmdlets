---
title: Copy-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Creates a copy of a work item, optionally changing its type. "
remarks: "Use this cmdlet to create a copy of a work item (using its latest saved state/revision data) that is of the specified work item type. By default, the copy retains the same type of the original work item, unless the Type argument is specified "
parameterSets: 
  "_All_": [ Collection, DestinationProject, IncludeAttachments, IncludeLinks, NewType, Passthru, Project, WorkItem ] 
  "__AllParameterSets":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    DestinationProject: 
      type: "object"  
    IncludeAttachments: 
      type: "SwitchParameter"  
    IncludeLinks: 
      type: "SwitchParameter"  
    NewType: 
      type: "object"  
    Passthru: 
      type: "string"  
    Project: 
      type: "object" 
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
  - name: "NewType" 
    description: "Specifies the type of the new work item. When omitted, the type of the original work item is preserved. " 
    globbing: false 
    type: "object" 
  - name: "IncludeAttachments" 
    description: "Creates a duplicate of all attachments present in the source work item and adds them to the new work item. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeLinks" 
    description: "Creates a copy of all links present in the source work item and adds them to the new work item. Only the links are copied; linked artifacts themselves are not copied. In other words, both the original and the copy work items point to the same linked artifacts. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "DestinationProject" 
    description: "Specifies the team project where the work item will be copied into. When omitted, the copy will be created in the same team project of the source work item. " 
    globbing: false 
    type: "object" 
  - name: "Project" 
    description: "Specifies the source team project from where the work item will be copied. When omitted, it defaults to the team project of the piped work item (if any), or to the connection set by Connect-TfsTeamProject. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. It takes one of the following values: Original (returns the original work item), Copy (returns the newly created work item copy) or None. " 
    globbing: false 
    type: "string" 
    defaultValue: "Copy"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Copy-TfsWorkItem"
aliases: 
examples: 
---
