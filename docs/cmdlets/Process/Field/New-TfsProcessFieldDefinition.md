---
title: New-TfsProcessFieldDefinition
breadcrumbs: [ "Process", "Field" ]
parent: "Process.Field"
description: "Gets information from one or more process templates. "
remarks: 
parameterSets: 
  "_All_": [ CanSortBy, Collection, Description, Field, IsIdentity, IsQueryable, Passthru, PicklistItems, PicklistSuggested, ReadOnly, ReferenceName, Server, Type ] 
  "__AllParameterSets":  
    Field: 
      type: "string"  
      position: "0"  
      required: true  
    ReferenceName: 
      type: "string"  
      position: "1"  
      required: true  
    CanSortBy: 
      type: "bool"  
    Collection: 
      type: "object"  
    Description: 
      type: "string"  
    IsIdentity: 
      type: "SwitchParameter"  
    IsQueryable: 
      type: "bool"  
    Passthru: 
      type: "SwitchParameter"  
    PicklistItems: 
      type: "object[]"  
    PicklistSuggested: 
      type: "SwitchParameter"  
    ReadOnly: 
      type: "bool"  
    Server: 
      type: "object"  
    Type: 
      type: "FieldType" 
parameters: 
  - name: "Field" 
    description: "Specifies the name of the field. " 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the field. This is an alias of the Field parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "ReferenceName" 
    description: "Specifies the reference name of the field. It should contain only letters, numbers, dots and underscores. " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Description" 
    description: "Specifies the description of the field. " 
    globbing: false 
    type: "string" 
  - name: "Type" 
    description: "Specifies the type of the field. Possible values: String, Integer, DateTime, PlainText, Html, TreePath, History, Double, Guid, Boolean, Identity, PicklistString, PicklistInteger, PicklistDouble" 
    globbing: false 
    type: "FieldType" 
    defaultValue: "String" 
  - name: "ReadOnly" 
    description: "Specifies whether the field is read-only. " 
    globbing: false 
    type: "bool" 
    defaultValue: "False" 
  - name: "CanSortBy" 
    description: "Specifies whether the field is sortable in server queries. " 
    globbing: false 
    type: "bool" 
    defaultValue: "False" 
  - name: "IsQueryable" 
    description: "Specifies whether the field can be queried in the server. " 
    globbing: false 
    type: "bool" 
    defaultValue: "False" 
  - name: "IsIdentity" 
    description: "Specifies whether the field is an identity field. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "PicklistItems" 
    description: "Specifies the contents of the picklist. Supplying values to this parameter will automatically turn the field into a picklist. " 
    globbing: false 
    type: "object[]" 
  - name: "PicklistSuggested" 
    description: "Specifies whether the user can enter a custom value in the picklist, making it a list of suggested values instead of allowed values. " 
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
  - type: "Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemField" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Process/Field/New-TfsProcessFieldDefinition"
aliases: 
examples: 
---
