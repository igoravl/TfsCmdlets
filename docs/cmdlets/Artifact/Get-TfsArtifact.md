---
title: Get-TfsArtifact
breadcrumbs: [ "Artifact" ]
parent: "Artifact"
description: "Gets information from one or more artifact feeds. "
remarks: 
parameterSets: 
  "_All_": [ Artifact, Collection, Feed, IncludeDeleted, IncludeDelisted, IncludeDescription, IncludePrerelease, Project, ProtocolType, Server ] 
  "__AllParameterSets":  
    Artifact: 
      type: "object"  
      position: "0"  
    Feed: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    IncludeDeleted: 
      type: "SwitchParameter"  
    IncludeDelisted: 
      type: "SwitchParameter"  
    IncludeDescription: 
      type: "SwitchParameter"  
    IncludePrerelease: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    ProtocolType: 
      type: "string"  
    Server: 
      type: "object" 
parameters: 
  - name: "Artifact" 
    description: "Specifies the package (artifact) name. Wildcards are supported. When omitted, returns all packages in the specified feed. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Package ] 
    defaultValue: "*" 
  - name: "Package" 
    description: "Specifies the package (artifact) name. Wildcards are supported. When omitted, returns all packages in the specified feed. This is an alias of the Artifact parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Package ] 
    defaultValue: "*" 
  - name: "Feed" 
    description: "Specifies the feed name. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "IncludeDeleted" 
    description: "Includes deletes packages in the result. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeDescription" 
    description: "Includes the package description in the results. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludePrerelease" 
    description: "Includes prerelease packages in the results. Applies only to Nuget packages. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeDelisted" 
    description: "Includes delisted packages in the results. Applies only to Nuget packages. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "ProtocolType" 
    description: "Returns only packages of the specified protocol type. " 
    globbing: false 
    type: "string" 
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
    description: "Specifies the feed name. " 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Feed.WebApi.Package" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Artifact/Get-TfsArtifact"
aliases: 
examples: 
---
