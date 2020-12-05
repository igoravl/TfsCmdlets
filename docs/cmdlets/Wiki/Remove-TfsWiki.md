---
title: Remove-TfsWiki
breadcrumbs: [ "Wiki" ]
parent: "Wiki"
description: "Deletes one or more Git repositories from a team project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Project, ProjectWiki, Wiki ] 
  "Remove code wiki":  
    Wiki: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
  "Remove Project Wiki":  
    ProjectWiki: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object" 
parameters: 
  - name: "Wiki" 
    description: "Specifies the Wiki to be deleted. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the Wiki to be deleted. This is an alias of the Wiki parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "ProjectWiki" 
    description: "Deletes the provisioned (\"project\") Wiki of the specified Team Project. " 
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
inputs: 
  - type: "System.Object" 
    description: "Specifies the Wiki to be deleted. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Wiki/Remove-TfsWiki"
aliases: 
examples: 
---
