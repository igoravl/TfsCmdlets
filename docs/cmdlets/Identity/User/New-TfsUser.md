---
title: New-TfsUser
breadcrumbs: [ "Identity", "User" ]
parent: "Identity.User"
description: "Creates a new user in the organization and optionally adds them to projects. "
remarks: 
parameterSets: 
  "_All_": [ Collection, DefaultGroup, DisplayName, License, Passthru, Project, Server, User ] 
  "__AllParameterSets":  
    User: 
      type: "string"  
      position: "0"  
    DisplayName: 
      type: "string"  
      position: "1"  
      required: true  
    Collection: 
      type: "object"  
    DefaultGroup: 
      type: "GroupEntitlementType"  
    License: 
      type: "AccountLicenseType"  
    Passthru: 
      type: "SwitchParameter"  
    Project: 
      type: "object"  
    Server: 
      type: "object" 
parameters: 
  - name: "User" 
    description: "Specifies the ID of the user to be created. For Azure DevOps Services, use the user's email address. For TFS, use the user's domain alias. " 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ UserId ] 
  - name: "UserId" 
    description: "Specifies the ID of the user to be created. For Azure DevOps Services, use the user's email address. For TFS, use the user's domain alias. This is an alias of the User parameter." 
    globbing: false 
    position: 0 
    type: "string" 
    aliases: [ UserId ] 
  - name: "DisplayName" 
    description: "Specifies the friendly (display) name of the user to be created. " 
    required: true 
    globbing: false 
    position: 1 
    type: "string" 
  - name: "License" 
    description: "Specifies the license type for the user to be created. When omitted, defaults to Stakeholder. Possible values: Basic, BasicTestPlans, Stakeholder, VisualStudio" 
    globbing: false 
    type: "AccountLicenseType" 
    defaultValue: "Stakeholder" 
  - name: "Project" 
    description: "Specifies the projects to which the user should be added. Can be supplied as an array of project names or a hashtable/dictionary with project names as keys and group names as values. When provided as an array, the user is added to the group specified in the DefaultGroup parameter. When omitted, the user is not added to any projects. " 
    globbing: false 
    type: "object" 
    aliases: [ Projects ] 
  - name: "Projects" 
    description: "Specifies the projects to which the user should be added. Can be supplied as an array of project names or a hashtable/dictionary with project names as keys and group names as values. When provided as an array, the user is added to the group specified in the DefaultGroup parameter. When omitted, the user is not added to any projects. This is an alias of the Project parameter." 
    globbing: false 
    type: "object" 
    aliases: [ Projects ] 
  - name: "DefaultGroup" 
    description: "Specifies the default group to which the user should be added, when applicable. When omitted, defaults to Contributor. Possible values: Administrator, Contributor, Reader, Stakeholder" 
    globbing: false 
    type: "GroupEntitlementType" 
    defaultValue: "Contributor" 
  - name: "Passthru" 
    description: "Returns the results of the command. By default, this cmdlet does not generate any output. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
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
outputs: 
  - type: "Microsoft.VisualStudio.Services.Licensing.AccountEntitlement" 
    description: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Identity/User/New-TfsUser"
aliases: 
examples: 
---
