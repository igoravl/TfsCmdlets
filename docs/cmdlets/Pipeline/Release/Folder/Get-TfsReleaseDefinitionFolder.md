---
title: Get-TfsReleaseDefinitionFolder
breadcrumbs: [ "Pipeline", "Release", "Folder" ]
parent: "Pipeline.Release.Folder"
description: "Gets one or more Release/pipeline definition folders in a team project."
remarks: 
parameterSets: 
  "_All_": [ Collection, Folder, Project, QueryOrder ] 
  "__AllParameterSets":  
    Folder: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    QueryOrder: 
      type: "FolderPathQueryOrder" 
parameters: 
  - name: "Folder" 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
    defaultValue: "**" 
  - name: "Path" 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned.This is an alias of the Folder parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
    defaultValue: "**" 
  - name: "QueryOrder" 
    description: "Specifies the query order. When omitted, defaults to None.Possible values: None, Ascending, Descending" 
    globbing: false 
    type: "FolderPathQueryOrder" 
    defaultValue: "None" 
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
  - type: "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Pipeline/Release/Folder/Get-TfsReleaseDefinitionFolder"
aliases: 
examples: 
---
