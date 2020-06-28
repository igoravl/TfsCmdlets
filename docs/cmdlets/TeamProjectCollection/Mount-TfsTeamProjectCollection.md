---
title: Mount-TfsTeamProjectCollection
breadcrumbs: [ "TeamProjectCollection" ]
parent: "TeamProjectCollection"
description: "Attaches a team project collection database to a Team Foundation Server installation."
remarks: 
parameterSets: 
  "_All_": [ Clone, Collection, ConnectionString, DatabaseName, DatabaseServer, Description, InitialState, PollingInterval, Server, Timeout ] 
  "Use database server":  
    Collection: 
      type: "string"  
      position: "0"  
      required: true  
    DatabaseName: 
      type: "string"  
      required: true  
    DatabaseServer: 
      type: "string"  
      required: true  
    Clone: 
      type: "SwitchParameter"  
    Description: 
      type: "string"  
    InitialState: 
      type: "string"  
    PollingInterval: 
      type: "int"  
    Server: 
      type: "object"  
    Timeout: 
      type: "TimeSpan"  
  "Use connection string":  
    Collection: 
      type: "string"  
      position: "0"  
      required: true  
    ConnectionString: 
      type: "string"  
      required: true  
    Clone: 
      type: "SwitchParameter"  
    Description: 
      type: "string"  
    InitialState: 
      type: "string"  
    PollingInterval: 
      type: "int"  
    Server: 
      type: "object"  
    Timeout: 
      type: "TimeSpan" 
parameters: 
  - name: "Collection" 
    description: "Specifies the name of the collection to attach. It can be different from the original name - in that case, it is attached under a new name." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Name" 
    description: "Specifies the name of the collection to attach. It can be different from the original name - in that case, it is attached under a new name.This is an alias of the Collection parameter." 
    required: true 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ Name ] 
  - name: "Description" 
    description: "Specifies a new description for the collection. When omitted, it retains the original description." 
    globbing: false 
    type: "string" 
  - name: "DatabaseServer" 
    description: "Specifies the name of the SQL Server instance where the database is stored." 
    required: true 
    globbing: false 
    type: "string" 
  - name: "DatabaseName" 
    description: "Specifies the name of the collection database." 
    required: true 
    globbing: false 
    type: "string" 
  - name: "ConnectionString" 
    description: "Specifies the connection string of the collection database." 
    required: true 
    globbing: false 
    type: "string" 
  - name: "InitialState" 
    description: "Specifies whether the collection will be started ou stopped after being attached. When omitted, the collection is automatically started and goes online after being attached." 
    globbing: false 
    type: "string" 
    defaultValue: "Started" 
  - name: "Clone" 
    description: "Changes the internal collection IDs upon attaching to that a \"clone\" of the original collection can be attached to the same server." 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "PollingInterval" 
    description: "Specifies the polling interval (in seconds) to get an updated status from the server. When omitted, defaults to 5 seconds." 
    globbing: false 
    type: "int" 
    defaultValue: "5" 
  - name: "Timeout" 
    description: "Specifies the maximum period of time this cmdlet should wait for the attach procedure to complete. By default, it waits indefinitely until the collection servicing completes." 
    globbing: false 
    type: "TimeSpan" 
    defaultValue: "10675199.02:48:05.4775807" 
  - name: "Server" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet." 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    type: "object"
inputs: 
  - type: "System.Object" 
    description: "Specifies the URL to the Team Foundation Server to connect to, a TfsConfigurationServer object (Windows PowerShell only), or a VssConnection object. When omitted, it defaults to the connection set by Connect-TfsConfiguration (if any). For more details, see the Get-TfsConfigurationServer cmdlet."
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/TeamProjectCollection/Mount-TfsTeamProjectCollection"
aliases: 
examples: 
---
