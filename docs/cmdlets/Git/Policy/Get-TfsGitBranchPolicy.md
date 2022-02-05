---
title: Get-TfsGitBranchPolicy
breadcrumbs: [ "Git", "Policy" ]
parent: "Git.Policy"
description: "Gets the Git branch policy configuration of the given Git branches. "
remarks: 
parameterSets: 
  "_All_": [ Branch, Collection, PolicyType, Project, Repository, Server ] 
  "__AllParameterSets":  
    PolicyType: 
      type: "object"  
      position: "0"  
    Branch: 
      type: "object"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Repository: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "PolicyType" 
    description: "Specifies the policy type of the branch policy to return. Wildcards are supported. When omitted, all branch policies defined for the given branch are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    defaultValue: "*" 
  - name: "Branch" 
    description: "Specifies the name of the branch to query for branch policies. When omitted, the default branch in the given repository is queried. " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ RefName ] 
  - name: "RefName" 
    description: "Specifies the name of the branch to query for branch policies. When omitted, the default branch in the given repository is queried. This is an alias of the Branch parameter." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
    aliases: [ RefName ] 
  - name: "Repository" 
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, defaults to the team project name (i.e. the default repository). " 
    globbing: false 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
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
    description: "Specifies the name of the branch to query for branch policies. When omitted, the default branch in the given repository is queried. "
outputs: 
  - type: "Microsoft.TeamFoundation.Policy.WebApi.PolicyConfiguration" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Git/Policy/Get-TfsGitBranchPolicy"
aliases: 
examples: 
---
