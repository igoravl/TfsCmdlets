---
title: Get-TfsWiki
breadcrumbs: [ "Wiki" ]
parent: "Wiki"
description: "Gets information from one or more Wiki repositories in a team project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Project, ProjectWiki, Wiki ] 
  "Get all wikis":  
    Wiki: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
  "Get Project Wiki":  
    ProjectWiki: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Wiki" 
    description: "Specifies the name or ID of a Wiki repository. Wildcards are supported. When omitted, all Wiki repositories in the supplied team project are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name or ID of a Wiki repository. Wildcards are supported. When omitted, all Wiki repositories in the supplied team project are returned. This is an alias of the Wiki parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "Id" 
    description: "Specifies the name or ID of a Wiki repository. Wildcards are supported. When omitted, all Wiki repositories in the supplied team project are returned. This is an alias of the Wiki parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Id ] 
    defaultValue: "*" 
  - name: "ProjectWiki" 
    description: "Returns only provisioned (\"project\") Wikis. When omitted, returns all Wikis (both Project wikis and Code wikis). " 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.TeamFoundation.Wiki.WebApi.WikiV2" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Wiki/Get-TfsWiki"
aliases: 
examples: 
---
