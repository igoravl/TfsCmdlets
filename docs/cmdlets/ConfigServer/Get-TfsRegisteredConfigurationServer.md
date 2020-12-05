---
title: Get-TfsRegisteredConfigurationServer
breadcrumbs: [ "ConfigServer" ]
parent: "ConfigServer"
description: "Gets one or more Team Foundation Server addresses registered in the current computer. "
remarks: 
parameterSets: 
  "_All_": [ Server ] 
  "__AllParameterSets":  
    Server: 
      type: "string"  
      position: "0" 
parameters: 
  - name: "Server" 
    description: "Specifies the name of a registered server. Wildcards are supported. When omitted, all registered servers are returned. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of a registered server. Wildcards are supported. When omitted, all registered servers are returned. This is an alias of the Server parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
    defaultValue: "*"
inputs: 
  - type: "System.String" 
    description: "Specifies the name of a registered server. Wildcards are supported. When omitted, all registered servers are returned. "
outputs: 
  - type: "Microsoft.TeamFoundation.Client.RegisteredConfigurationServer" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ConfigServer/Get-TfsRegisteredConfigurationServer"
aliases: 
examples: 
---
