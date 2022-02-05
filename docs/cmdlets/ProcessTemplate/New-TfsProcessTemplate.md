---
title: New-TfsProcessTemplate
breadcrumbs: [ "ProcessTemplate" ]
parent: "ProcessTemplate"
description: "Creates a new inherited process. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Description, Force, Parent, Passthru, ProcessTemplate, ReferenceName, Server ] 
  "__AllParameterSets":  
    ProcessTemplate: 
      type: "string"  
      position: "0"  
      required: true  
    Parent: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    Description: 
      type: "string"  
    Force: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter"  
    ReferenceName: 
      type: "string"  
    Server: 
      type: "object" 
parameters: 
  - name: "ProcessTemplate" 
    description: "Specifies the name of the process to create. " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Process ] 
  - name: "Name" 
    description: "Specifies the name of the process to create. This is an alias of the ProcessTemplate parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Process ] 
  - name: "Process" 
    description: "Specifies the name of the process to create. This is an alias of the ProcessTemplate parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name,Process ] 
  - name: "Description" 
    description: "Specifies the description of the new process. " 
    globbing: false 
    type: "string" 
  - name: "ReferenceName" 
    description: "Specifies the reference name of the new process. When omitted, a random name will be automatically generated and assigned by the server. " 
    globbing: false 
    type: "string" 
  - name: "Parent" 
    description: "Specifies the name of the parent process from which the new process will inherit. " 
    required: true 
    globbing: false 
    type: "object" 
  - name: "Force" 
    description: "Allows the cmdlet to overwrite an existing process. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
outputs: 
  - type: "Microsoft.TeamFoundation.Core.WebApi.Process" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/ProcessTemplate/New-TfsProcessTemplate"
aliases: 
examples: 
---
