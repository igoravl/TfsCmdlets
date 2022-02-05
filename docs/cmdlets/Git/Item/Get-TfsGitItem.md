---
title: Get-TfsGitItem
breadcrumbs: [ "Git", "Item" ]
parent: "Git.Item"
description: "Gets information from one or more items (folders and/or files) in a remote Git repository. "
remarks: 
parameterSets: 
  "_All_": [ Branch, Collection, Commit, IncludeContent, IncludeMetadata, Item, Project, Repository, Server, Tag ] 
  "Get by commit SHA":  
    Item: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Commit: 
      type: "object"  
    IncludeContent: 
      type: "SwitchParameter"  
    IncludeMetadata: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Repository: 
      type: "object"  
    Server: 
      type: "object"  
  "Get by tag":  
    Item: 
      type: "object"  
      position: "0"  
    Tag: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    IncludeContent: 
      type: "SwitchParameter"  
    IncludeMetadata: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Repository: 
      type: "object"  
    Server: 
      type: "object"  
  "Get by branch":  
    Item: 
      type: "object"  
      position: "0"  
    Branch: 
      type: "string"  
      required: true  
    Collection: 
      type: "object"  
    IncludeContent: 
      type: "SwitchParameter"  
    IncludeMetadata: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Repository: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Item" 
    description: "Specifies the path to items (folders and/or files) in the supplied Git repository. Wildcards are supported. When omitted, all items in the root of the Git repository are retrieved. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
    defaultValue: "/*" 
  - name: "Path" 
    description: "Specifies the path to items (folders and/or files) in the supplied Git repository. Wildcards are supported. When omitted, all items in the root of the Git repository are retrieved. This is an alias of the Item parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
    defaultValue: "/*" 
  - name: "Commit" 
    description:  
    globbing: false 
    type: "object" 
  - name: "Tag" 
    description:  
    required: true 
    globbing: false 
    type: "string" 
  - name: "Branch" 
    description:  
    required: true 
    globbing: false 
    type: "string" 
  - name: "IncludeContent" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeMetadata" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Repository" 
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, defaults to the team project name (i.e. the default repository). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
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
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, defaults to the team project name (i.e. the default repository). "
outputs: 
  - type: "Microsoft.TeamFoundation.SourceControl.WebApi.GitItem" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Git/Item/Get-TfsGitItem"
aliases: 
examples: 
---
