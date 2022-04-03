---
title: Get-TfsArtifactFeed
breadcrumbs: [ "Artifact" ]
parent: "Artifact"
description: "Gets information from one or more artifact feeds. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Feed, Project, Role, Scope, Server ] 
  "__AllParameterSets":  
    Feed: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Role: 
      type: "FeedRole"  
    Scope: 
      type: "ProjectOrCollectionScope"  
    Server: 
      type: "object" 
parameters: 
  - name: "Feed" 
    description: "Specifies the feed name. Wildcards are supported. When omitted, returns all feeds. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "Scope" 
    description: "Returns only feeds from the given scope (collection or project). When omitted, returns all feeds. Possible values: Collection, Project, All" 
    globbing: false 
    type: "ProjectOrCollectionScope" 
    defaultValue: "All" 
  - name: "Role" 
    description: "Filters by role. Returns only those feeds where the currently logged on user has one of the specified roles: either Administrator, Contributor, or Reader level permissions. When omitted, filters by Administrator role. Possible values: Custom, None, Reader, Contributor, Administrator, Collaborator" 
    globbing: false 
    type: "FeedRole" 
    defaultValue: "Reader" 
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
    description: "Specifies the feed name. Wildcards are supported. When omitted, returns all feeds. " 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Feed.WebApi.Feed" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Artifact/Get-TfsArtifactFeed"
aliases: 
examples: 
---
