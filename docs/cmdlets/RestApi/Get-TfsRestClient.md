---
title: Get-TfsRestClient
breadcrumbs: [ "RestApi" ]
parent: "RestApi"
description: "Gets an Azure DevOps HTTP Client object instance. "
remarks: "Connection objects (Microsoft.VisualStudio.Services.Client.VssConnection in PowerShell Core, Microsoft.TeamFoundation.Client.TfsTeamProjectCollection in Windows PowerShell) provide access to many HTTP client objects such as Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient that wrap many of the REST APIs exposed by Azure DevOps. Those clients inherit the authentication information supplied by their parent connection object and can be used as a more convenient mechanism to issue API calls. "
parameterSets: 
  "_All_": [ Collection, Server, TypeName ] 
  "__AllParameterSets":  
    TypeName: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "TypeName" 
    description: "Specifies the full type name (optionally including its assembly name) of the HTTP Client class to return. " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Type ] 
  - name: "Type" 
    description: "Specifies the full type name (optionally including its assembly name) of the HTTP Client class to return. This is an alias of the TypeName parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Type ] 
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
  - type: "Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/RestApi/Get-TfsRestClient"
aliases: 
examples: 
---
