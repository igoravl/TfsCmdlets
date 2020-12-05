---
title: New-TfsGlobalList
breadcrumbs: [ "GlobalList" ]
parent: "GlobalList"
description: "Creates a new Global List. "
remarks: 
parameterSets: 
  "_All_": [ Collection, Force, GlobalList, Items, Passthru ] 
  "__AllParameterSets":  
    GlobalList: 
      type: "string"  
      position: "0"  
      required: true  
    Items: 
      type: "string[]"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter" 
parameters: 
  - name: "GlobalList" 
    description: "Specifies the name of the new global list. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the new global list. This is an alias of the GlobalList parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Items" 
    description: "Specifies the contents (items) of the new global list. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByPropertyName)" 
    type: "string[]" 
  - name: "Force" 
    description: "Allows the cmdlet to overwrite an existing global list. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.String" 
    description: "Specifies the name of the new global list. " 
  - type: "System.String[]" 
    description: "Specifies the contents (items) of the new global list. "
outputs: 
  - type: "System.Management.Automation.PSCustomObject" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/GlobalList/New-TfsGlobalList"
aliases: 
examples: 
---
