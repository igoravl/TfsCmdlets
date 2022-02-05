---
title: Import-TfsProcessTemplate
breadcrumbs: [ "ProcessTemplate" ]
parent: "ProcessTemplate"
description: "Imports a process template definition from disk. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Path, Server, State ] 
  "__AllParameterSets":  
    Path: 
      type: "string"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Server: 
      type: "object"  
    State: 
      type: "string" 
parameters: 
  - name: "Path" 
    description: "Specifies the folder containing the process template to be imported. This folder must contain the file ProcessTemplate.xml " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "string" 
  - name: "State" 
    description: "Specifies the state of the template after it is imported. When set to Invisible, the process template will not be listed in the server UI. " 
    globbing: false 
    type: "string" 
    defaultValue: "Visible" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Organization" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). This is an alias of the Collection parameter." 
    globbing: false 
    type: "object" 
    aliases: [ Organization ] 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.String" 
    description: "Specifies the folder containing the process template to be imported. This folder must contain the file ProcessTemplate.xml "
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ProcessTemplate/Import-TfsProcessTemplate"
aliases: 
examples: 
---
