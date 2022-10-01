---
title: Undo-TfsWorkItemQueryFolderRemoval
breadcrumbs: [ "WorkItem", "Query" ]
parent: "WorkItem.Query"
description: "Restores a deleted work item query folder. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Folder, Project, Recursive, Scope, Server ] 
  "__AllParameterSets":  
    Folder: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Recursive: 
      type: "SwitchParameter"  
    Scope: 
      type: "QueryItemScope"  
    Server: 
      type: "object" 
parameters: 
  - name: "Folder" 
    description: "Specifies one or more query folders to restore. Wildcards supported. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  - name: "Path" 
    description: "Specifies one or more query folders to restore. Wildcards supported. This is an alias of the Folder parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  - name: "Scope" 
    description: "Specifies the scope of the item to restore. Personal refers to the \"My Queries\" folder\", whereas Shared refers to the \"Shared Queries\" folder. When omitted defaults to \"Both\", effectively searching for items in both scopes. Possible values: Personal, Shared, Both" 
    globbing: false 
    type: "QueryItemScope" 
    defaultValue: "Both" 
  - name: "Recursive" 
    description: "Restores the specified query folder and all its descendants. When omitted, the specified folder is restored but not its contents (queries and/or sub-folders). " 
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
    description: "Specifies one or more query folders to restore. Wildcards supported. "
outputs: 
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.QueryHierarchyItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Query/Undo-TfsWorkItemQueryFolderRemoval"
aliases: 
examples: 
---
