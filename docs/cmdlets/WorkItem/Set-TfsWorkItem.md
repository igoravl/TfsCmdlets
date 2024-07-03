﻿---
title: Set-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Sets the contents of one or more work items. "
remarks: 
parameterSets: 
  "_All_": [ AreaPath, AssignedTo, BoardColumn, BoardColumnDone, BoardLane, BypassRules, Collection, Description, Fields, IterationPath, Passthru, Priority, Project, Reason, Server, State, SuppressNotifications, Tags, Title, ValueArea, WorkItem ] 
  "__AllParameterSets":  
    WorkItem: 
      type: "object"  
      position: "0"  
    AreaPath: 
      type: "string"  
    AssignedTo: 
      type: "object"  
    BoardColumn: 
      type: "string"  
    BoardColumnDone: 
      type: "bool"  
    BoardLane: 
      type: "string"  
    BypassRules: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    Description: 
      type: "string"  
    Fields: 
      type: "Hashtable"  
    IterationPath: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    Priority: 
      type: "int"  
    Project: 
      type: "object"  
    Reason: 
      type: "string"  
    Server: 
      type: "object"  
    State: 
      type: "string"  
    SuppressNotifications: 
      type: "SwitchParameter"  
    Tags: 
      type: "string[]"  
    Title: 
      type: "string"  
    ValueArea: 
      type: "string" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. This is an alias of the WorkItem parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "Title" 
    description: "Specifies the title of the work item. " 
    globbing: false 
    type: "string" 
  - name: "Description" 
    description: "Specifies the description of the work item. " 
    globbing: false 
    type: "string" 
  - name: "AreaPath" 
    description: "Specifies the area path of the work item. " 
    globbing: false 
    type: "string" 
  - name: "IterationPath" 
    description: "Specifies the iteration path of the work item. " 
    globbing: false 
    type: "string" 
  - name: "AssignedTo" 
    description: "Specifies the user this work item is assigned to. " 
    globbing: false 
    type: "object" 
  - name: "State" 
    description: "Specifies the state of the work item. " 
    globbing: false 
    type: "string" 
  - name: "Reason" 
    description: "Specifies the reason (field 'System.Reason') of the work item. " 
    globbing: false 
    type: "string" 
  - name: "ValueArea" 
    description: "Specifies the Value Area of the work item. " 
    globbing: false 
    type: "string" 
  - name: "BoardColumn" 
    description: "Specifies the board column of the work item. " 
    globbing: false 
    type: "string" 
  - name: "BoardColumnDone" 
    description: "Specifies whether the work item is in the sub-column Doing or Done in a board. " 
    globbing: false 
    type: "bool" 
    defaultValue: "False" 
  - name: "BoardLane" 
    description: "Specifies the board lane of the work item " 
    globbing: false 
    type: "string" 
  - name: "Priority" 
    description: "Specifies the priority of the work item. " 
    globbing: false 
    type: "int" 
    defaultValue: "0" 
  - name: "Tags" 
    description: "Specifies the tags of the work item. " 
    globbing: false 
    type: "string[]" 
  - name: "Fields" 
    description: "Specifies the names and the corresponding values for the fields to be set in the work item and whose values were not supplied in the other arguments to this cmdlet. " 
    globbing: false 
    type: "Hashtable" 
  - name: "BypassRules" 
    description: "Bypasses any rule validation when saving the work item. Use it with caution, as this may leave the work item in an invalid state. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "SuppressNotifications" 
    description: "Do not fire any notifications for this change. Useful for bulk operations and automated processes. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
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
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Set-TfsWorkItem"
aliases: 
examples: 
---
