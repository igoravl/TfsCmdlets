---
title: Remove-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Deletes one or more Global Lists."
remarks: 
parameterSets: 
  "_All_": [ Collection, GlobalList ] 
  "__AllParameterSets":  
    GlobalList: 
      type: "string"  
      position: "0"  
    Collection: 
      type: "object" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of global list to be deleted. Wildcards are supported." 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of global list to be deleted. Wildcards are supported.This is an alias of the GlobalList parameter." 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.String" 
    description: "Specifies the name of global list to be deleted. Wildcards are supported."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/GlobalList/Remove-TfsGlobalList"
aliases: 
examples: 
---
