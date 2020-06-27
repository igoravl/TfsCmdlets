---
title: Get-TfsConfigurationServerConnectionString
breadcrumbs: [ "Admin" ]
parent: "Admin"
description: "Gets the configuration server database connection string."
remarks: 
parameterSets: 
  "_All_": [ ComputerName, Credential, Session, Version ] 
  "Use computer name":  
    ComputerName: 
      type: "string"  
    Credential: 
      type: "PSCredential"  
    Version: 
      type: "int"  
  "Use session":  
    Session: 
      type: "PSSession"  
      required: true  
    Credential: 
      type: "PSCredential"  
    Version: 
      type: "int" 
parameters: 
  - name: "ComputerName" 
    description: "Specifies the name of a Team Foundation Server application tier from which to retrieve the connection string." 
    globbing: false 
    type: "string" 
    defaultValue: "localhost" 
  - name: "Session" 
    description: "The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a previously opened PowerShell Remote session can be provided instead. When omitted, defaults to the local machine where the script is being run" 
    required: true 
    globbing: false 
    type: "PSSession" 
  - name: "Version" 
    description: "The TFS version number, represented by the year in its name. For e.g. TFS 2015, use \"2015\". When omitted, will default to the newest installed version of TFS / Azure DevOps Server" 
    globbing: false 
    type: "int" 
    defaultValue: "0" 
  - name: "Credential" 
    description: "The user credentials to be used to access a remote machine. Those credentials must have the required permission to execute a PowerShell Remote session on that computer." 
    globbing: false 
    type: "PSCredential" 
    defaultValue: "System.Management.Automation.PSCredential"
inputs: 
outputs: 
  - type: "System.String" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/Cmdlets/Admin/Get-TfsConfigurationServerConnectionString" 
  - text: "Online version:" 
    uri: "https://tfscmdlets.dev/admin/get-tfsconfigurationserverconnectionstring/" 
  - text: "Get-TfsInstallationPath" 
    uri: 
aliases: 
examples: 
---
