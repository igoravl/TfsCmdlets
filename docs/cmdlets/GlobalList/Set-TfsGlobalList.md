---
title: Set-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Changes the contents of a Global List. "
remarks: 
parameterSets: 
  "_All_": [ Add, Collection, Force, GlobalList, Passthru, Project, Remove ] 
  "__AllParameterSets":  
    GlobalList: 
      type: "string"  
      required: true  
    Add: 
      type: "IEnumerable`1"  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Remove: 
      type: "IEnumerable`1" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of the global list to be changed. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the global list to be changed. This is an alias of the GlobalList parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    type: "string" 
    aliases: [ Name ] 
  - name: "Add" 
    description: "Specifies a list of items to be added to the global list. " 
    globbing: false 
    type: "IEnumerable`1" 
  - name: "Remove" 
    description: "Specifies a list of items to be removed from the global list. " 
    globbing: false 
    type: "IEnumerable`1" 
  - name: "Force" 
    description: "Creates a new list if the specified one does not exist. " 
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
  - type: "System.String" 
    description: "Specifies the name of the global list to be changed. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/GlobalList/Set-TfsGlobalList"
aliases: 
examples: 
---
