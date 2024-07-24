---
title: Add-TfsWorkItemLink
breadcrumbs: [ "WorkItem", "Linking" ]
parent: "WorkItem.Linking"
description: "Adds a link between two work items. "
remarks: 
parameterSets: 
  "_All_": [ BypassRules, Collection, Comment, LinkType, Passthru, Server, SuppressNotifications, TargetWorkItem, WorkItem ] 
  "Link to work item":  
    WorkItem: 
      type: "object"  
      position: "0"  
      required: true  
    TargetWorkItem: 
      type: "object"  
      position: "1"  
      required: true  
    LinkType: 
      type: "WorkItemLinkType"  
      position: "2"  
      required: true  
    BypassRules: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    Comment: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    Server: 
      type: "object"  
    SuppressNotifications: 
      type: "SwitchParameter" 
parameters: 
  - name: "WorkItem" 
    description:  
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id,From ] 
  - name: "Id" 
    description: "This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id,From ] 
  - name: "From" 
    description: "This is an alias of the WorkItem parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Id,From ] 
  - name: "TargetWorkItem" 
    description:  
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ To ] 
  - name: "To" 
    description: "This is an alias of the TargetWorkItem parameter." 
    required: true 
    globbing: false 
    position: 1 
    type: "object" 
    aliases: [ To ] 
  - name: "LinkType" 
    description: "Possible values: All, Parent, Child, Related, Predecessor, Successor, Duplicate, DuplicateOf, Tests, TestedBy, TestCase, SharedSteps, References, ReferencedBy, ProducesFor, ConsumesFrom, RemoteRelated, AttachedFile, Hyperlink, ArtifactLink" 
    required: true 
    globbing: false 
    position: 2 
    type: "WorkItemLinkType" 
    aliases: [ EndLinkType,Type ] 
    defaultValue: "All" 
  - name: "EndLinkType" 
    description: "Possible values: All, Parent, Child, Related, Predecessor, Successor, Duplicate, DuplicateOf, Tests, TestedBy, TestCase, SharedSteps, References, ReferencedBy, ProducesFor, ConsumesFrom, RemoteRelated, AttachedFile, Hyperlink, ArtifactLinkThis is an alias of the LinkType parameter." 
    required: true 
    globbing: false 
    position: 2 
    type: "WorkItemLinkType" 
    aliases: [ EndLinkType,Type ] 
    defaultValue: "All" 
  - name: "Type" 
    description: "Possible values: All, Parent, Child, Related, Predecessor, Successor, Duplicate, DuplicateOf, Tests, TestedBy, TestCase, SharedSteps, References, ReferencedBy, ProducesFor, ConsumesFrom, RemoteRelated, AttachedFile, Hyperlink, ArtifactLinkThis is an alias of the LinkType parameter." 
    required: true 
    globbing: false 
    position: 2 
    type: "WorkItemLinkType" 
    aliases: [ EndLinkType,Type ] 
    defaultValue: "All" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "BypassRules" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "SuppressNotifications" 
    description:  
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Comment" 
    description:  
    globbing: false 
    type: "string" 
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
  - type: "System.Object" 
    description: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/WorkItem/Linking/Add-TfsWorkItemLink"
aliases: 
examples: 
---
