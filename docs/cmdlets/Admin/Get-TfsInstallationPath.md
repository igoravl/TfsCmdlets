---
title: Get-TfsInstallationPath
breadcrumbs: [ "Admin" ]
parent: "Admin"
description: "Gets the installation path of a given Team Foundation Server component."
remarks: "Many times a Team Foundation Server admin needs to retrieve the location where TFS is actually installed. That can be useful, for instance, to locate tools like TfsSecurity or TfsServiceControl. That information is recorded at setup time, in a well-known location in the Windows Registry of the server where TFS is installed."
parameterSets: 
  "_All_": [ Component, ComputerName, Credential, Session, Version ] 
  "Use computer name":  
    Component: 
      type: "TfsComponent"  
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
    Component: 
      type: "TfsComponent"  
    Credential: 
      type: "PSCredential"  
    Version: 
      type: "int" 
parameters: 
  - name: "ComputerName" 
    description: "The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a previously opened PowerShell Remote session can be provided instead. When omitted, defaults to the local machine where the script is being run" 
    globbing: false 
    type: "string" 
    defaultValue: "localhost" 
  - name: "Session" 
    description: "The machine name of the server where the TFS component is installed. It must be properly configured for PowerShell Remoting in case it's a remote machine. Optionally, a System.Management.Automation.Runspaces.PSSession object pointing to a previously opened PowerShell Remote session can be provided instead. When omitted, defaults to the local machine where the script is being run" 
    required: true 
    globbing: false 
    type: "PSSession" 
  - name: "Component" 
    description: "Indicates the TFS component whose installation path is being searched for. For the main TFS installation directory, use BaseInstallation. When omitted, defaults to BaseInstallation.Possible values: BaseInstallation, ApplicationTier, SharePointExtensions, TeamBuild, Tools, VersionControlProxy" 
    globbing: false 
    type: "TfsComponent" 
    defaultValue: "BaseInstallation" 
  - name: "Version" 
    description: "The TFS version number, represented by the year in its name. For e.g. TFS 2015, use \"2015\". When omitted, will default to the newest installed version of TFS / Azure DevOps Server" 
    globbing: false 
    type: "int" 
    defaultValue: "0" 
  - name: "Credential" 
    description: "The user credentials to be used to access a remote machine. Those credentials must have the required permission to execute a PowerShell Remote session on that computer and also the permission to access the Windows Registry." 
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
    uri: "https://tfscmdlets.dev/Cmdlets/Admin/Get-TfsInstallationPath"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Get-TfsInstallationPath -Version 2017" 
    remarks: "Gets the root folder (the BaseInstallationPath) of TFS in the local server where the cmdlet is being run" 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Get-TfsInstallationPath -Computer SPTFSSRV -Version 2015 -Component SharepointExtensions -Credentials (Get-Credentials)" 
    remarks: "Gets the location where the SharePoint Extensions have been installed in the remote server SPTFSSRV, prompting for admin credentials to be used for establishing a PS Remoting session to the server"
---
