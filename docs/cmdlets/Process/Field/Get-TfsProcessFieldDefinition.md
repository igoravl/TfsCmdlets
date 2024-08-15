﻿---
title: Get-TfsProcessFieldDefinition
breadcrumbs: [ "Process", "Field" ]
parent: "Process.Field"
description: "Gets information from one or more organization-wide work item fields. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Field, IncludeDeleted, IncludeExtensionFields, Project, ReferenceName, Server ] 
  "By Name":  
    Field: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    IncludeDeleted: 
      type: "SwitchParameter"  
    IncludeExtensionFields: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "By Reference Name":  
    ReferenceName: 
      type: "string[]"  
      required: true  
    Collection: 
      type: "object"  
    IncludeDeleted: 
      type: "SwitchParameter"  
    IncludeExtensionFields: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Field" 
    description: "Specifies the name of the field(s) to be returned. Wildcards are supported. When omitted, all fields in the given organization are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of the field(s) to be returned. Wildcards are supported. When omitted, all fields in the given organization are returned. This is an alias of the Field parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "ReferenceName" 
    description: "Specifies the reference name of the field(s) to be returned. Wildcards are supported. " 
    required: true 
    globbing: false 
    type: "string[]" 
  - name: "Project" 
    description: "Limits the search to the specified project. " 
    globbing: false 
    type: "object" 
  - name: "IncludeExtensionFields" 
    description: "Specifies whether to include extension fields in the result. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeDeleted" 
    description: "Specifies whether to include deleted fields in the result. " 
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
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Process/Field/Get-TfsProcessFieldDefinition"
aliases: 
examples: 
---
