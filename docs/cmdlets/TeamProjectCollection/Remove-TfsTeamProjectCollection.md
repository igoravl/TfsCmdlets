---
title: Remove-TfsTeamProjectCollection
breadcrumbs: [ "TeamProjectCollection" ]
parent: "TeamProjectCollection"
description: "Deletes a team project collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Server, Timeout ] 
  "__AllParameterSets":  
    Collection: 
      type: "object"  
      position: "0"  
    Server: 
      type: "object"  
    Timeout: 
      type: "TimeSpan" 
parameters: 
  - name: "Collection" 
    description: "Specifies the collection to be removed. Wildcards are supported. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Timeout" 
    description: "Sets the timeout for the operation to complete. When omitted, will wait indefinitely until the operation completes. " 
    globbing: false 
    type: "TimeSpan" 
    defaultValue: "10675199.02:48:05.4775807" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the collection to be removed. Wildcards are supported. "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProjectCollection/Remove-TfsTeamProjectCollection"
aliases: 
examples: 
---
