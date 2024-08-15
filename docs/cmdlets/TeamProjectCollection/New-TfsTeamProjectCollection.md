---
title: New-TfsTeamProjectCollection
breadcrumbs: [ "TeamProjectCollection" ]
parent: "TeamProjectCollection"
description: "Creates a new team project collection. "
remarks: 
parameterSets: 
  "_All_": [ Collection, ConnectionString, DatabaseName, DatabaseServer, Default, Description, InitialState, Passthru, PollingInterval, Server, Timeout, UseExistingDatabase ] 
  "Use database server":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    DatabaseServer: 
      type: "string"  
      required: true  
    DatabaseName: 
      type: "string"  
    Default: 
      type: "SwitchParameter"  
    Description: 
      type: "string"  
    InitialState: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    PollingInterval: 
      type: "int"  
    Server: 
      type: "object"  
    Timeout: 
      type: "TimeSpan"  
    UseExistingDatabase: 
      type: "SwitchParameter"  
  "Use connection string":  
    Collection: 
      type: "object"  
      position: "0"  
      required: true  
    ConnectionString: 
      type: "string"  
      required: true  
    Default: 
      type: "SwitchParameter"  
    Description: 
      type: "string"  
    InitialState: 
      type: "string"  
    Passthru: 
      type: "SwitchParameter"  
    PollingInterval: 
      type: "int"  
    Server: 
      type: "object"  
    Timeout: 
      type: "TimeSpan"  
    UseExistingDatabase: 
      type: "SwitchParameter" 
parameters: 
  - name: "Collection" 
    description: "Specifies the name of the team project collection to create. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the team project collection to create. This is an alias of the Collection parameter." 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
    aliases: [ Name ] 
  - name: "Description" 
    description: "Specifies the description of the team project collection. " 
    globbing: false 
    type: "string" 
  - name: "DatabaseServer" 
    description: "Specifies the name of the SQL Server for hosting the team project collection database. " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "DatabaseName" 
    description: "Specifies the name of the team project collection database. " 
    globbing: false 
    type: "string" 
  - name: "ConnectionString" 
    description: "Specifies the connection string for the team project collection. " 
    required: true 
    globbing: false 
    type: "string" 
  - name: "Default" 
    description: "Specifies whether new the team project collection should become the default collection. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "UseExistingDatabase" 
    description: "Specifies whether to use an existing database for the team project collection. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "InitialState" 
    description: "Specifies the initial state of the team project collection. This allows you to create a collection in a stopped state when needed. " 
    globbing: false 
    type: "string" 
    defaultValue: "Started" 
  - name: "PollingInterval" 
    description: "Specifies the frequency (in seconds) at which the command will check whether the creation is completed. " 
    globbing: false 
    type: "int" 
    defaultValue: "5" 
  - name: "Timeout" 
    description: "Specifies the timeout to wait for the operation to complete. When omitted, will wait indefinitely until the operation completes. " 
    globbing: false 
    type: "TimeSpan" 
    defaultValue: "10675199.02:48:05.4775807" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet. " 
    globbing: false 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the name of the team project collection to create. "
outputs: 
  - type: "TfsCmdlets.Models.Connection" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/TeamProjectCollection/New-TfsTeamProjectCollection"
aliases: 
examples: 
---
