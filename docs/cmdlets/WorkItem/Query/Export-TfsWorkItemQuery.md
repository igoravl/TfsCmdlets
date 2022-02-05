---
title: Export-TfsWorkItemQuery
breadcrumbs: [ "WorkItem", "Query" ]
parent: "WorkItem.Query"
description: "Exports a saved work item query to XML. "
remarks: "Work item queries can be exported to XML files (.WIQ extension) in order to be shared and reused. Visual Studio Team Explorer has the ability to open and save WIQ files. Use this cmdlet to generate WIQ files compatible with the format supported by Team Explorer. "
parameterSets: 
  "_All_": [ AsXml, Collection, Destination, Encoding, FlattenFolders, Force, Project, Query, Scope, Server ] 
  "Export to file":  
    Query: 
      type: "object"  
      position: "0"  
      required: true  
    Destination: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    Encoding: 
      type: "string"  
    FlattenFolders: 
      type: "SwitchParameter"  
    Force: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Scope: 
      type: "string"  
    Server: 
      type: "object"  
  "Export to output stream":  
    Query: 
      type: "object"  
      position: "0"  
      required: true  
    AsXml: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Scope: 
      type: "string"  
    Server: 
      type: "object" 
parameters: 
  - name: "Query" 
    description: "Specifies one or more saved queries to export. Wildcards supported. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  - name: "Path" 
    description: "Specifies one or more saved queries to export. Wildcards supported. This is an alias of the Query parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  - name: "Scope" 
    description: "Specifies the scope of the returned item. Personal refers to the \"My Queries\" folder\", whereas Shared refers to the \"Shared Queries\" folder. When omitted defaults to \"Both\", effectively searching for items in both scopes. " 
    globbing: false 
    type: "string" 
    defaultValue: "Both" 
  - name: "Destination" 
    description: "Specifies the path to the folder where exported queries are saved. " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Encoding" 
    description: "Specifies the encoding for the exported XML files. When omitted, defaults to UTF-8. " 
    globbing: false 
    type: "string" 
    defaultValue: "UTF-8" 
  - name: "FlattenFolders" 
    description: "Flattens the query folder structure. When omitted, the original query folder structure is recreated in the destination folder. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Force" 
    description: "Allows the cmdlet to overwrite an existing file in the destination folder. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "AsXml" 
    description: "Exports the saved query to the standard output stream as a string-encoded XML document. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
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
    description: "Specifies one or more saved queries to export. Wildcards supported. " 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "System.String" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Query/Export-TfsWorkItemQuery"
aliases: 
examples: 
---
