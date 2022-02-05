---
title: Get-TfsGroupMember
breadcrumbs: [ "Identity", "Group" ]
parent: "Identity.Group"
description: "Gets the members of a Azure DevOps group "
remarks: 
parameterSets: 
  "_All_": [ Collection, Group, Member, Recurse, Server ] 
  "__AllParameterSets":  
    Group: 
      type: "object"  
      position: "0"  
      required: true  
    Member: 
      type: "string"  
      position: "1"  
    Collection: 
      type: "object"  
    Recurse: 
      type: "SwitchParameter"  
    Server: 
      type: "object" 
parameters: 
  - name: "Group" 
    description: "Specifies the group fom which to get its members. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Member" 
    description: "Specifies the member (user or group) to get from the given group. Wildcards are supported. When omitted, all group members are returned. " 
    globbing: false 
    position: 1 
    type: "string" 
    defaultValue: "*" 
  - name: "Recurse" 
    description: "Recursively expands all member groups, returning the users and/or groups contained in them " 
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
    description: "Specifies the group fom which to get its members. " 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Identity.Identity" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/Group/Get-TfsGroupMember"
aliases: 
examples: 
---
