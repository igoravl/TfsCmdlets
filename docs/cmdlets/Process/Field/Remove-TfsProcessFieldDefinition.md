---
title: Remove-TfsProcessFieldDefinition
breadcrumbs: [ "Process", "Field" ]
parent: "Process.Field"
description: "Deletes one or more work item field definitions from a collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Field, Force, ReferenceName, Server ] 
  "By Name":  
    Field: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
  "By Reference Name":  
    ReferenceName: 
      type: "string[]"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "Field" 
    description: "Specifies the name of the field(s) to be removed. Wildcards are supported. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "Name" 
    description: "Specifies the name of the field(s) to be removed. Wildcards are supported. This is an alias of the Field parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
    defaultValue: "*" 
  - name: "ReferenceName" 
    description: "Specifies the reference name of the field(s) to be removed. Wildcards are supported. " 
    required: true 
    globbing: false 
    type: "string[]" 
  - name: "Force" 
    description: "Forces the exclusion of the item. When omitted, the command prompts for confirmation prior to deleting the item. " 
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
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Process/Field/Remove-TfsProcessFieldDefinition"
aliases: 
examples: 
---
