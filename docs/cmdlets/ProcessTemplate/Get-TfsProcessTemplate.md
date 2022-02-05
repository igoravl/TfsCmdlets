---
title: Get-TfsProcessTemplate
breadcrumbs: [ "ProcessTemplate" ]
parent: "ProcessTemplate"
description: "Gets information from one or more process templates. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Default, ProcessTemplate, Server ] 
  "Get by name":  
    ProcessTemplate: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Server: 
      type: "object"  
  "Get default process":  
    Default: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "ProcessTemplate" 
    description: "Specifies the name of the process template(s) to be returned. Wildcards are supported. When omitted, all process templates in the given project collection are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Process ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of the process template(s) to be returned. Wildcards are supported. When omitted, all process templates in the given project collection are returned. This is an alias of the ProcessTemplate parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Process ] 
    defaultValue: "*" 
  - name: "Process" 
    description: "Specifies the name of the process template(s) to be returned. Wildcards are supported. When omitted, all process templates in the given project collection are returned. This is an alias of the ProcessTemplate parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name,Process ] 
    defaultValue: "*" 
  - name: "Default" 
    description: "Returns the default process template in the given orgnization / project collection. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.Process" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ProcessTemplate/Get-TfsProcessTemplate"
aliases: 
examples: 
---
