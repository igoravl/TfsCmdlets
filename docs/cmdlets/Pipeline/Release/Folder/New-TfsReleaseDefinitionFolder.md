---
title: New-TfsReleaseDefinitionFolder
parent: [ "Pipeline", "Release", "Folder" ]
description: "Creates a new release definition folder."
parameterSets: 
  "__AllParameterSets": [ Folder, Collection, Description, Passthru, Project ]
parameters: 
  "Folder": 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Path ] 
  "Path": 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned.This is an alias of the Folder parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Path ] 
  "Description": 
    description: "Specifies the description of the new build/pipeline folder." 
    globbing: false 
    type: "string" 
  "Project": 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  "Collection": 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  "Passthru": 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter"
inputs: 
  - "System.String": "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned."
outputs: 
  - "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder": 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Pipeline/Release/Folder/New-TfsReleaseDefinitionFolder"
aliases: 
examples: 
---
