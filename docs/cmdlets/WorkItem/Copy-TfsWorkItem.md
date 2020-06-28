---
title: Copy-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Creates a copy of a work item, optionally changing its type."
remarks: "Use this cmdlet to create a copy of a work item (using its latest saved state/revision data) that is of the specified work item type. By default, the copy retains the same type of the original work item, unless the Type argument is specified"
parameterSets: 
  "_All_": [ Collection, DestinationProject, IncludeAttachments, IncludeLinks, Passthru, SkipSave, Type, WorkItem ] 
  "__AllParameterSets":  
    Collection: 
      type: "object"  
    DestinationProject: 
      type: "object"  
    IncludeAttachments: 
      type: "SwitchParameter"  
    IncludeLinks: 
      type: "SwitchParameter"  
    Passthru: 
      type: "string"  
    SkipSave: 
      type: "SwitchParameter"  
    Type: 
      type: "object"  
    WorkItem: 
      type: "object" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.This is an alias of the WorkItem parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ id ] 
  - name: "Type" 
    description:  
    globbing: false 
    type: "object" 
  - name: "IncludeAttachments" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeLinks" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "SkipSave" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "DestinationProject" 
    description: "Specifies the team project where the work item will be copied into. When omitted, the copy will be created in the same team project of the source work item. The value provided to this argument takes precedence over both the source team project and the team project of an instance of WorkItemType provided to the Type argument." 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. It takes one of the following values: Original (returns the original work item), Copy (returns the newly created work item copy) or None." 
    globbing: false 
    type: "string" 
    defaultValue: "Copy"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/Copy-TfsWorkItem"
aliases: 
examples: 
---
