---
title: New-TfsWiki
breadcrumbs: [ "Wiki" ]
parent: "Wiki"
description: "Creates a new Wiki repository in a team project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Passthru, Project, ProjectWiki, Repository, Wiki ] 
  "Create Code Wiki":  
    Wiki: 
      type: "string"  
      position: "0"  
      required: true  
    Repository: 
      type: "object"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
  "Provision Project Wiki":  
    ProjectWiki: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Wiki" 
    description: "Specifies the name of the new Wiki " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Id ] 
  - name: "Name" 
    description: "Specifies the name of the new Wiki This is an alias of the Wiki parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Id ] 
  - name: "Id" 
    description: "Specifies the name of the new Wiki This is an alias of the Wiki parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Id ] 
  - name: "Repository" 
    description: "Specifies the name or ID of the Git repository to publish as a Wiki " 
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
  - name: "ProjectWiki" 
    description: "Creates a provisioned (\"project\") Wiki in the specified Team Project. " 
    required: true 
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
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
outputs: 
  - type: "Microsoft.TeamFoundation.Wiki.WebApi.WikiV2" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Wiki/New-TfsWiki"
aliases: 
examples: 
---
