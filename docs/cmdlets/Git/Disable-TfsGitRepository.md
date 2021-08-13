---
title: Disable-TfsGitRepository
breadcrumbs: [ "Git" ]
parent: "Git"
description: "Disables one or more Git repositories. "
remarks: "Disables access to the repository. When a repository is disabled it cannot be accessed (including clones, pulls, pushes, builds, pull requests etc) but remains discoverable, with a warning message stating it is disabled. "
parameterSets: 
  "_All_": [ Project, Repository ] 
  "__AllParameterSets":  
    Repository: 
      type: "object"  
      position: "0"  
      required: true  
    Project: 
      type: "object" 
parameters: 
  - name: "Repository" 
    description: "Specifies the name or ID of a Git repository. Wildcards are supported. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name or ID of a Git repository. Wildcards are supported. This is an alias of the Repository parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name or ID of a Git repository. Wildcards are supported. "
outputs: 
  - type: "Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Git/Disable-TfsGitRepository"
aliases: 
examples: 
---
