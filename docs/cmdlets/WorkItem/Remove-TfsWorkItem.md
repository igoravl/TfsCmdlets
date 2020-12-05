---
title: Remove-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Deletes a work item from a team project collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Destroy, Force, Project, WorkItem ] 
  "__AllParameterSets":  
    WorkItem: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Destroy: 
      type: "SwitchParameter"  
    Force: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies the work item to remove. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies the work item to remove. This is an alias of the WorkItem parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "Destroy" 
    description: "Permanently deletes the work item, without sending it to the recycle bin. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Force" 
    description: "Forces the exclusion of the item. When omitted, the command prompts for confirmation prior to deleting the item. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the work item to remove. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Remove-TfsWorkItem"
aliases: 
examples: 
---
