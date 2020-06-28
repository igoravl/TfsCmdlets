---
title: Set-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Sets the contents of one or more work items."
remarks: 
parameterSets: 
  "_All_": [ Board, BypassRules, Collection, Column, ColumnStage, Fields, Lane, SkipSave, Team, WorkItem ] 
  "Set work item":  
    WorkItem: 
      type: "object"  
      position: "0"  
    Fields: 
      type: "Hashtable"  
      position: "1"  
    BypassRules: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    SkipSave: 
      type: "SwitchParameter"  
  "Set board status":  
    WorkItem: 
      type: "object"  
      position: "0"  
    Board: 
      type: "object"  
    Collection: 
      type: "object"  
    Column: 
      type: "object"  
    ColumnStage: 
      type: "string"  
    Lane: 
      type: "object"  
    Team: 
      type: "object" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.This is an alias of the WorkItem parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "Fields" 
    description: "Specifies the names and the corresponding values for the fields to be set in the work item." 
    globbing: false 
    position: 1 
    type: "Hashtable" 
  - name: "BypassRules" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "SkipSave" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Board" 
    description:  
    globbing: false 
    type: "object" 
  - name: "Column" 
    description:  
    globbing: false 
    type: "object" 
  - name: "Lane" 
    description:  
    globbing: false 
    type: "object" 
  - name: "ColumnStage" 
    description:  
    globbing: false 
    type: "string" 
  - name: "Team" 
    description:  
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/Set-TfsWorkItem"
aliases: 
examples: 
---
