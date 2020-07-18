---
title: Get-TfsWorkItemType
breadcrumbs: [ "WorkItem", "WorkItemType" ]
parent: "WorkItem.WorkItemType"
description: "Gets one or more Work Item Type definitions from a team project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Project, Type, WorkItem ] 
  "Get by type":  
    Type: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
  "Get by work item":  
    WorkItem: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Type" 
    description: "Specifies one or more work item type names to return. Wildcards are supported. When omitted, returns all work item types in the given team project." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies one or more work item type names to return. Wildcards are supported. When omitted, returns all work item types in the given team project.This is an alias of the Type parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "WorkItem" 
    description: "Speficies a work item whose corresponding type should be returned." 
    required: true 
    globbing: false 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/WorkItemType/Get-TfsWorkItemType"
aliases: 
examples: 
---
