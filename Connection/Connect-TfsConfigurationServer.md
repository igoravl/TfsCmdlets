---
title: Connect-TfsConfigurationServer
breadcrumbs: [ "Connection" ]
parent: "Connection"
description: "Connects to a configuration server. "
remarks: "A TFS Configuration Server represents the server that is running Team Foundation Server. On a database level, it is represented by the Tfs_Configuration database. Operations that should be performed on a server level (such as setting server-level permissions) require a connection to a TFS configuration server. Internally, this connection is represented by an instance of the Microsoft.TeamFoundation.Client.TfsConfigurationServer. NOTE: Currently it is only supported in Windows PowerShell. "
parameterSets: 
  "_All_": [ Cached, Credential, Interactive, Passthru, Password, PersonalAccessToken, Server, UserName ] 
  "Cached credentials":  
    Server: 
      type: "object"  
      position: "0"  
      required: true  
    Cached: 
      type: "SwitchParameter"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
  "User name and password":  
    Server: 
      type: "object"  
      position: "0"  
      required: true  
    UserName: 
      type: "string"  
      position: "1"  
      required: true  
    Password: 
      type: "SecureString"  
      position: "2"  
    Passthru: 
      type: "SwitchParameter"  
  "Credential object":  
    Server: 
      type: "object"  
      position: "0"  
      required: true  
    Credential: 
      type: "object"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
  "Personal Access Token":  
    Server: 
      type: "object"  
      position: "0"  
      required: true  
    PersonalAccessToken: 
      type: "string"  
      required: true  
    Passthru: 
      type: "SwitchParameter"  
  "Prompt for credential":  
    Server: 
      type: "object"  
      position: "0"  
      required: true  
    Interactive: 
      type: "SwitchParameter"  
    Passthru: 
      type: "SwitchParameter" 
parameters: 
  - name: "Server" 
    description: "Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. When using a URL, it must be fully qualified. To connect to a Team Foundation Server instance by using its name, it must have been previously registered. " 
    required: true 
    globbing: false 
    pipelineInput: "true (ByValue)" 
    position: 0 
    type: "object" 
  - name: "Cached" 
    description: "Specifies that cached (default) credentials should be used when possible/available. " 
    required: true 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "UserName" 
    description: "Specifies a user name for authentication modes (such as Basic) that support username/password-based credentials. Must be used in conjunction with the -Password argument " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "Password" 
    description: "Specifies a password for authentication modes (such as Basic) that support username/password-based credentials. Must be used in conjunction with the -UserName argument " 
    globbing: false 
    position: 2 
    type: "SecureString" 
  - name: "Credential" 
    description: "Specifies a user account that has permission to perform this action. To provide a user name and password, a Personal Access Token, and/or to open a input dialog to enter your credentials, call Get-TfsCredential with the appropriate arguments and pass its return to this argument. " 
    required: true 
    globbing: false 
    type: "object" 
  - name: "PersonalAccessToken" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOps " 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat ] 
  - name: "Pat" 
    description: "Specifies a personal access token, used as an alternate credential, to authenticate to Azure DevOps This is an alias of the PersonalAccessToken parameter." 
    required: true 
    globbing: false 
    type: "string" 
    aliases: [ Pat ] 
  - name: "Interactive" 
    description: "Prompts for user credentials. Can be used for any Team Foundation Server or Azure DevOps account - the proper login dialog is automatically selected. Should only be used in an interactive PowerShell session (i.e., a PowerShell terminal window), never in an unattended script (such as those executed during an automated build). Currently it is only supported in Windows PowerShell. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
  - type: "System.Object" 
    description: "Specifies either a URL/name of the Team Foundation Server to connect to, or a previously initialized TfsConfigurationServer object. When using a URL, it must be fully qualified. To connect to a Team Foundation Server instance by using its name, it must have been previously registered. "
outputs: 
  - type: "Microsoft.TeamFoundation.Client.TfsConfigurationServer" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Connection/Connect-TfsConfigurationServer"
aliases: 
examples: 
  - title: "----------  EXAMPLE 1  ----------" 
    code: "PS> Connect-TfsConfigurationServer -Server http://vsalm:8080/tfs" 
    remarks: "Connects to the TFS server specified by the URL in the Server argument" 
  - title: "----------  EXAMPLE 2  ----------" 
    code: "PS> Connect-TfsConfigurationServer -Server vsalm" 
    remarks: "Connects to a previously registered TFS server by its user-defined name \"vsalm\". For more information, see Get-TfsRegisteredConfigurationServer"
---
