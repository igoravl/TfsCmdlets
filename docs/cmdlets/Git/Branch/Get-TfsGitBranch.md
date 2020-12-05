---
title: Get-TfsGitBranch
breadcrumbs: [ "Git", "Branch" ]
parent: "Git.Branch"
description: "Gets information from one or more branches in a remote Git repository. "
remarks: 
parameterSets: 
  "_All_": [ Branch, Collection, Default, Project, Repository ] 
  "Get by name":  
    Branch: 
      type: "object"  
      position: "0"  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Repository: 
      type: "object"  
  "Get default":  
    Default: 
      type: "SwitchParameter"  
      required: true  
    Collection: 
      type: "object"  
    Project: 
      type: "object"  
    Repository: 
      type: "object" 
parameters: 
  - name: "Branch" 
    description: "Specifies the name of a branch in the supplied Git repository. Wildcards are supported. When omitted, all branches are returned. " 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ RefName ] 
    defaultValue: "*" 
  - name: "RefName" 
    description: "Specifies the name of a branch in the supplied Git repository. Wildcards are supported. When omitted, all branches are returned. This is an alias of the Branch parameter." 
    globbing: false 
    position: 0 
    type: "object" 
    aliases: [ RefName ] 
    defaultValue: "*" 
  - name: "Default" 
    description: "Returns the default branch in the given repository. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Repository" 
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, it default to the team project name (i.e. the default repository). " 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
  - name: "Collection" 
    description: "Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. You can also connect to an Azure DevOps Services organizations by simply providing its name instead of the full URL. For more details, see the Get-TfsTeamProjectCollection cmdlet. When omitted, it defaults to the connection set by Connect-TfsTeamProjectCollection (if any). " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, it default to the team project name (i.e. the default repository). "
outputs: 
  - type: "Microsoft.TeamFoundation.SourceControl.WebApi.GitBranchStats" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Git/Branch/Get-TfsGitBranch"
aliases: 
examples: 
---
