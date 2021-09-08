---
title: Export-TfsWorkItemAttachment
breadcrumbs: [ "WorkItem", "Linking" ]
parent: "WorkItem.Linking"
description: "Downloads one or more attachments from work items "
remarks: 
parameterSets: 
  "_All_": [ Attachment, Collection, Destination, Force, WorkItem ] 
  "__AllParameterSets":  
    Attachment: 
      type: "object"  
      position: "0"  
    WorkItem: 
      type: "object"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Destination: 
      type: "string"  
    Force: 
      type: "SwitchParameter" 
parameters: 
  - name: "Attachment" 
    description: "Specifies the attachment to download. Wildcards are supported. When omitted, all attachments in the specified work item are downloaded. " 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "WorkItem" 
    description: "Specifies a work item. Valid values are the work item ID or an instance of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 1 
    type: "object" 
  - name: "Destination" 
    description: "Specifies the directory to save the attachment to. When omitted, defaults to the current directory. " 
    globbing: false 
    type: "string" 
  - name: "Force" 
    description: "Allows the cmdlet to overwrite an existing file. " 
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
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Linking/Export-TfsWorkItemAttachment"
aliases: 
examples: 
---
