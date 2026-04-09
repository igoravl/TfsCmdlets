---
title: Install-TfsShell
breadcrumbs: [ "Shell" ]
parent: "Shell"
description: "Installs Azure DevOps Shell shortcuts and Windows Terminal profile fragments. "
remarks: "Install-TfsShell creates shell shortcuts that launch a PowerShell session with the TfsCmdlets module pre-loaded and Enter-TfsShell invoked. When Windows Terminal is detected, it creates shortcuts that launch via wt.exe and deploys JSON profile fragments. Use -Force to create traditional PowerShell shortcuts even when Windows Terminal is available. Windows Terminal detection uses registry lookups and file system checks. If detection fails (e.g. due to insufficient permissions), the cmdlet falls back to PowerShell shortcuts automatically. "
parameterSets: 
  "_All_": [ Force, Target ] 
  "__AllParameterSets":  
    Target: 
      type: "ShellTarget"  
      position: "0"  
    Force: 
      type: "SwitchParameter" 
parameters: 
  - name: "Target" 
    description: "Specifies the target locations for shortcut installation. Defaults to StartMenu and Desktop. Possible values: StartMenu, Desktop, WindowsTerminal, All" 
    globbing: false 
    position: 0 
    type: "ShellTarget" 
    defaultValue: "StartMenu, Desktop" 
  - name: "Force" 
    description: "Forces the creation of traditional PowerShell shortcuts even when Windows Terminal is detected. Does not affect the WindowsTerminal target. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Shell/Install-TfsShell"
aliases: 
examples: 
---
