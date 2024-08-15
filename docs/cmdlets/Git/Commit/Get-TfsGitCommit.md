---
title: Get-TfsGitCommit
breadcrumbs: [ "Git", "Commit" ]
parent: "Git.Commit"
description: "Gets information from one or more Git commits in a remote repository. "
remarks: 
parameterSets: 
  "_All_": [ Author, Branch, Collection, Commit, Committer, CompareVersion, ExcludeDeletes, FromCommit, FromDate, IncludeLinks, IncludePushData, IncludeUserImageUrl, IncludeWorkItems, ItemPath, Project, Repository, Server, ShowOldestCommitsFirst, Skip, Tag, ToCommit, ToDate, Top ] 
  "Get by commit SHA":  
    Commit: 
      type: "object"  
      position: "0"  
      required: true  
    Repository: 
      type: "object"  
      required: true  
    Collection: 
      type: "object"  
    IncludeWorkItems: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
  "Get by tag":  
    Repository: 
      type: "object"  
      required: true  
    Tag: 
      type: "string"  
      required: true  
    Author: 
      type: "string"  
    Collection: 
      type: "object"  
    Committer: 
      type: "string"  
    CompareVersion: 
      type: "GitVersionDescriptor"  
    ExcludeDeletes: 
      type: "SwitchParameter"  
    FromCommit: 
      type: "string"  
    FromDate: 
      type: "DateTime"  
    IncludeLinks: 
      type: "SwitchParameter"  
    IncludePushData: 
      type: "SwitchParameter"  
    IncludeUserImageUrl: 
      type: "SwitchParameter"  
    IncludeWorkItems: 
      type: "SwitchParameter"  
    ItemPath: 
      type: "string"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
    ShowOldestCommitsFirst: 
      type: "SwitchParameter"  
    Skip: 
      type: "int"  
    ToCommit: 
      type: "string"  
    ToDate: 
      type: "DateTime"  
    Top: 
      type: "int"  
  "Get by branch":  
    Branch: 
      type: "string"  
      required: true  
    Repository: 
      type: "object"  
      required: true  
    Author: 
      type: "string"  
    Collection: 
      type: "object"  
    Committer: 
      type: "string"  
    CompareVersion: 
      type: "GitVersionDescriptor"  
    ExcludeDeletes: 
      type: "SwitchParameter"  
    FromCommit: 
      type: "string"  
    FromDate: 
      type: "DateTime"  
    IncludeLinks: 
      type: "SwitchParameter"  
    IncludePushData: 
      type: "SwitchParameter"  
    IncludeUserImageUrl: 
      type: "SwitchParameter"  
    IncludeWorkItems: 
      type: "SwitchParameter"  
    ItemPath: 
      type: "string"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
    ShowOldestCommitsFirst: 
      type: "SwitchParameter"  
    Skip: 
      type: "int"  
    ToCommit: 
      type: "string"  
    ToDate: 
      type: "DateTime"  
    Top: 
      type: "int"  
  "Search commits":  
    Repository: 
      type: "object"  
      required: true  
    Author: 
      type: "string"  
    Collection: 
      type: "object"  
    Committer: 
      type: "string"  
    CompareVersion: 
      type: "GitVersionDescriptor"  
    ExcludeDeletes: 
      type: "SwitchParameter"  
    FromCommit: 
      type: "string"  
    FromDate: 
      type: "DateTime"  
    IncludeLinks: 
      type: "SwitchParameter"  
    IncludePushData: 
      type: "SwitchParameter"  
    IncludeUserImageUrl: 
      type: "SwitchParameter"  
    IncludeWorkItems: 
      type: "SwitchParameter"  
    ItemPath: 
      type: "string"  
    Project: 
      type: "object"  
    Server: 
      type: "object"  
    ShowOldestCommitsFirst: 
      type: "SwitchParameter"  
    Skip: 
      type: "int"  
    ToCommit: 
      type: "string"  
    ToDate: 
      type: "DateTime"  
    Top: 
      type: "int" 
parameters: 
  - name: "Commit" 
    description: "Specifies the hash (SHA) of the commit to return. " 
    required: true 
    globbing: false 
    position: 0 
    type: "object" 
  - name: "Tag" 
    description: "Specifies the tag name of the commit to return. " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Branch" 
    description: "Specifies the branch name of the commit to return. " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Author" 
    description: "Limits the search to commits authored by this user. " 
    globbing: false 
    type: "string" 
  - name: "Committer" 
    description: "Limits the search to commits committed by this user. " 
    globbing: false 
    type: "string" 
  - name: "CompareVersion" 
    description:  
    globbing: false 
    type: "GitVersionDescriptor" 
  - name: "FromCommit" 
    description: "Specifies the \"commit-ish\" to start the search from. " 
    globbing: false 
    type: "string" 
  - name: "FromDate" 
    description: "Specifies the date and time of the commit to start the search from. " 
    globbing: false 
    type: "DateTime" 
    defaultValue: "1/1/0001 12:00:00 AM" 
  - name: "ItemPath" 
    description: "Limits the search to commits that affect this path. " 
    globbing: false 
    type: "string" 
  - name: "ToCommit" 
    description: "Specifies the \"commit-ish\" to end the search at. " 
    globbing: false 
    type: "string" 
  - name: "ToDate" 
    description: "Specifies the date and time of the commit to end the search at. " 
    globbing: false 
    type: "DateTime" 
    defaultValue: "1/1/0001 12:00:00 AM" 
  - name: "ShowOldestCommitsFirst" 
    description: "Sorts the results from oldest to newest commit. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Skip" 
    description:  
    globbing: false 
    type: "int" 
    defaultValue: "0" 
  - name: "Top" 
    description: "Specifies the maximum number of commits to return. " 
    globbing: false 
    type: "int" 
    defaultValue: "0" 
  - name: "ExcludeDeletes" 
    description: "Prevents deleted items from being included in the results. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeLinks" 
    description: "Includes links to related resources in the results. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeWorkItems" 
    description: "Includes links to related work items in the results. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludePushData" 
    description: "Includes push data in the results. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "IncludeUserImageUrl" 
    description: "Includes the user's image URL in the results. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Repository" 
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, defaults to the team project name (i.e. the default repository). " 
    required: true 
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
    description: "Specifies the target Git repository. Valid values are the name of the repository, its ID (a GUID), or a Microsoft.TeamFoundation.SourceControl.WebApi.GitRepository object obtained by e.g. a call to Get-TfsGitRepository. When omitted, defaults to the team project name (i.e. the default repository). "
outputs: 
  - type: "Microsoft.TeamFoundation.SourceControl.WebApi.GitCommitRef" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Git/Commit/Get-TfsGitCommit"
aliases: 
examples: 
---
