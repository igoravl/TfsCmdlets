---
title: Remove-TfsReleaseDefinitionFolder
parent: [ "Pipeline", "Release", "Folder" ]
description: "Deletes one or more release definition folders."
parameterSets: 
  "__AllParameterSets": [ Folder, Collection, Force, Project, Recurse ]
parameters: 
  "Folder": 
    description: "Specifies the path of the release folder to delete. Wildcards are supported." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  "Path": 
    description: "Specifies the path of the release folder to delete. Wildcards are supported.This is an alias of the Folder parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue, ByPropertyName)" 
    position: 0 
    type: "object" 
    aliases: [ Path ] 
  "Recurse": 
    description: "Removes folders recursively. When omitted, folders with subfolders cannot be deleted." 
    globbing: false 
    type: "SwitchParameter" 
  "Force": 
    description: "Forces the exclusion of folders containing release definitions definitions. When omitted, only empty folders can be deleted." 
    globbing: false 
    type: "SwitchParameter" 
  "Project": 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  "Collection": 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - "System.Object": "Specifies the path of the release folder to delete. Wildcards are supported."
outputs: 
  - "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder": 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Pipeline/Release/Folder/Remove-TfsReleaseDefinitionFolder"
aliases: 
examples: 
---