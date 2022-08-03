---
title: Get-TfsTeamProjectMember
breadcrumbs: [ "TeamProject", "Member" ]
parent: "TeamProject.Member"
description: "Gets the members of a team project. "
remarks: 
parameterSets: 
  "_All_": [ AsIdentity, Collection, Member, Project, Server ] 
  "__AllParameterSets":  
    AsIdentity: 
      type: "SwitchParameter"  
    Collection: 
      type: "object"  
    Member: 
      type: "object"  
    Project: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "Member" 
    description: "Specifies the name of a team project member. Wildcards are supported. When omitted, all team project members are returned. " 
    globbing: false 
    type: "object" 
    defaultValue: "*" 
  - name: "AsIdentity" 
    description: "Returns the members as fully resolved Identity objects. When omitted, it returns only the name, ID and email of the users. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
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
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. "
outputs: 
  - type: "Microsoft.VisualStudio.Services.Identity.Identity" 
    description:  
  - type: "TfsCmdlets.Models.TeamProjectMember" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProject/Member/Get-TfsTeamProjectMember"
aliases: 
examples: 
---
