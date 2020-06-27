---
title: Remove-TfsWorkItemTag
breadcrumbs: [ "WorkItem", "Tagging" ]
parent: "WorkItem.Tagging"
description: "Deletes one or more work item tags."
remarks: 
parameterSets: 
  "_All_": [ Collection, Force, Project, Tag ] 
  "__AllParameterSets":  
    Tag: 
      type: "object"  
      position: "0"  
      required: true  
    Collection: 
      type: "object"  
    Force: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Tag" 
    description: "Specifies one or more tags to delete. Wildcards are supported." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies one or more tags to delete. Wildcards are supported.This is an alias of the Tag parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Force" 
    description: "Allows the cmdlet to delete active tags (currently associated with work items)." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet." 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any)." 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies one or more tags to delete. Wildcards are supported."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/WorkItem/Tagging/Remove-TfsWorkItemTag"
aliases: 
examples: 
---
