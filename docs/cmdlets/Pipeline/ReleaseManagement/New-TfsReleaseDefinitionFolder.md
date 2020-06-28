---
title: New-TfsReleaseDefinitionFolder
breadcrumbs: [ "Pipeline", "ReleaseManagement" ]
parent: "Pipeline.ReleaseManagement"
description: "Creates a new release definition folder."
remarks: 
parameterSets: 
  "_All_": [ Collection, Description, Folder, Passthru, Project ] 
  "__AllParameterSets":  
    Folder: 
      type: "string"  
      position: "0"  
    Collection: 
      type: "object"  
    Description: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Folder" 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Path ] 
    defaultValue: "**" 
  - name: "Path" 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned.This is an alias of the Folder parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Path ] 
    defaultValue: "**" 
  - name: "Description" 
    description: "Specifies the description of the new build/pipeline folder." 
    globbing: false 
    type: "string" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.String" 
    description: "Specifies the folder path. Wildcards are supported. When omitted, all Release/pipeline folders in the supplied team project are returned."
outputs: 
  - type: "Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Folder" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Pipeline/ReleaseManagement/New-TfsReleaseDefinitionFolder"
aliases: 
examples: 
---
