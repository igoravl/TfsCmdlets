---
title: Export-TfsWorkItemType
breadcrumbs: [ "WorkItem", "WorkItemType" ]
parent: "WorkItem.WorkItemType"
description: "Exports an XML work item type definition from a team project."
remarks: 
parameterSets: 
  "_All_": [ AsXml, Collection, Destination, Force, IncludeGlobalLists, Project, Type ] 
  "Export to output stream":  
    Type: 
      type: "string"  
      position: "0"  
    AsXml: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    IncludeGlobalLists: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
  "Export to file":  
    Destination: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    IncludeGlobalLists: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Type" 
    description: "Specifies one or more work item types to export. Wildcards are supported. When omitted, all work item types in the given project are exported" 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies one or more work item types to export. Wildcards are supported. When omitted, all work item types in the given project are exportedThis is an alias of the Type parameter." 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "IncludeGlobalLists" 
    description: "Exports the definitions of referenced global lists. When omitted, global list definitions are not included in the exported XML document." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Destination" 
    description: "Specifies the path to the folder where exported types are saved." 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Force" 
    description: "Allows the cmdlet to overwrite an existing file in the destination folder." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "AsXml" 
    description: "Exports the saved query to the standard output stream as a string-encoded XML document." 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
  - type: "System.String" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/WorkItemType/Export-TfsWorkItemType"
aliases: 
examples: 
---
