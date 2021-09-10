---
title: Rename-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Changes either the name or the contents of a Global List. "
remarks: 
parameterSets: 
  "_All_": [ Collection, GlobalList, NewName, Passthru, Project ] 
  "__AllParameterSets":  
    GlobalList: 
      type: "string"  
      position: "0"  
      required: true  
    NewName: 
      type: "string"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of the global lsit to be renamed. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the global lsit to be renamed. This is an alias of the GlobalList parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "NewName" 
    description: "Specifies the new name of the item. Enter only a name - i.e., for items that support paths, do not enter a path and name. " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
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
  - type: "System.String" 
    description: "Specifies the name of the global lsit to be renamed. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/GlobalList/Rename-TfsGlobalList"
aliases: 
examples: 
---
