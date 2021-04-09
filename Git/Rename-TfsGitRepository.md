---
title: Rename-TfsGitRepository
breadcrumbs: [ "Git" ]
parent: "Git"
description: "Renames a Git repository in a team project. "
remarks: 
parameterSets: 
  "_All_": [ Collection, NewName, Passthru, Project, Repository ] 
  "__AllParameterSets":  
    Repository: 
      type: "object"  
      position: "0"  
      required: true  
    NewName: 
      type: "string"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object" 
parameters: 
  - name: "Repository" 
    description: "Specifies the repository to be renamed. Value can be the name or ID of a Git repository, as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git repository. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "NewName" 
    description: "Specifies the new name of the item. Enter only a name - i.e., for items that support paths, do not enter a path and name. " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Project" 
    description: "Specifies the name of the Team Project, its ID (a GUID), or a Microsoft.TeamFoundation.Core.WebApi.TeamProject object to connect to. When omitted, it defaults to the connection set by Connect-TfsTeamProject (if any). For more details, see the Get-TfsTeamProject cmdlet. " 
    globbing: false 
    type: "object" 
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
  - type: "System.Object" 
    description: "Specifies the repository to be renamed. Value can be the name or ID of a Git repository, as well as a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object representing a Git repository. "
outputs: 
  - type: "Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Git/Rename-TfsGitRepository"
aliases: 
examples: 
---
