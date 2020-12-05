---
title: Get-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Gets the contents of one or more Global Lists. "
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
    description: "Specifies the name of the global list. Wildcards are supported. When omitted, defaults to all global lists in the supplied team project collection. " 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of the global list. Wildcards are supported. When omitted, defaults to all global lists in the supplied team project collection. This is an alias of the GlobalList parameter." 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "TfsCmdlets.Models.GlobalList" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/GlobalList/Get-TfsGlobalList"
aliases: 
examples: 
---
