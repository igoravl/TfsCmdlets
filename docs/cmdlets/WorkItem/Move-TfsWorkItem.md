---
title: Move-TfsWorkItem
breadcrumbs: [ "WorkItem" ]
parent: "WorkItem"
description: "Moves a work item to a different team project in the same collection."
remarks: 
parameterSets: 
  "_All_": [ Area, Collection, Destination, History, Iteration, Passthru, State, WorkItem ] 
  "__AllParameterSets":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    Destination: 
      type: "object"  
      position: "1"  
      required: true  
    Area: 
      type: "object"  
    Collection: 
      type: "object"  
    History: 
      type: "object"  
    Iteration: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    State: 
      type: "object" 
parameters: 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "id" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem.This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ id ] 
  - name: "Destination" 
    description:  
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
  - name: "Area" 
    description:  
    globbing: false 
    type: "object" 
  - name: "Iteration" 
    description:  
    globbing: false 
    type: "object" 
  - name: "State" 
    description:  
    globbing: false 
    type: "object" 
  - name: "History" 
    description:  
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem."
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/Move-TfsWorkItem"
aliases: 
examples: 
---
