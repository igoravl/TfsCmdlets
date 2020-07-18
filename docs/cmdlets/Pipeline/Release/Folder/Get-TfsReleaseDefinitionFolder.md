---
title: Get-TfsReleaseDefinitionFolder
parent: [ "Pipeline", "Release", "Folder" ]
description: "Gets one or more Release/pipeline definition folders in a team project."
parameterSets: 
  "__AllParameterSets": [ Folder, Collection, Project, QueryOrder ]
parameters: 
  "Folder": 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  "Path": 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned.This is an alias of the Folder parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  "QueryOrder": 
    description: "Specifies the query order. When omitted, defaults to None.Possible values: None, Ascending, Descending" 
    globbing: false 
    type: "FolderPathQueryOrder" 
  "Project": 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  "Collection": 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - "System.Object": "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet."
outputs: 
  - "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder": 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Pipeline/Release/Folder/Get-TfsReleaseDefinitionFolder"
aliases: 
examples: 
---
