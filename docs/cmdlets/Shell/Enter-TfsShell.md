---
title: Enter-TfsShell
breadcrumbs: [ "Shell" ]
parent: "Shell"
description: "Activates the Azure DevOps Shell. "
remarks: "Enter-TfsShell sets up an interactive Azure DevOps Shell session. It customizes the PowerShell prompt, optionally integrates with Oh-My-Posh for an enhanced prompt experience, clears the host screen, displays a version banner, and loads an optional user profile script. If Oh-My-Posh is installed and not disabled, it is automatically initialized using the bundled Azure DevOps theme (azuredevops.omp.json). A custom theme path can be provided via the TFSCMDLETS_OMP_THEME environment variable. To fall back to the classic TfsCmdlets prompt regardless of Oh-My-Posh availability, set the TFSCMDLETS_OMP_DISABLE environment variable to 1 or true. When not running in debug mode, the user profile script is loaded from TfsCmdlets_Profile.ps1 (in the same directory as the PowerShell profile). In debug mode, the script name is TfsCmdlets_Debug_Profile.ps1. Environment variables: * TFSCMDLETS_OMP_THEME â€” Full path to a custom Oh-My-Posh theme file (.omp.json). When set, this theme is used instead of the bundled azuredevops.omp.json theme. * TFSCMDLETS_OMP_DISABLE â€” Set to 1 or true (case-insensitive) to disable Oh-My-Posh integration entirely. When disabled, the classic TfsCmdlets prompt is used regardless of whether Oh-My-Posh is installed. To end the shell session and restore the previous prompt and window title, call Exit-TfsShell. "
parameterSets: 
  "_All_": [ DoNotClearHost, NoLogo, NoProfile, WindowTitle ] 
  "__AllParameterSets":  
    DoNotClearHost: 
      type: "SwitchParameter"  
    NoLogo: 
      type: "SwitchParameter"  
    NoProfile: 
      type: "SwitchParameter"  
    WindowTitle: 
      type: "string" 
parameters: 
  - name: "WindowTitle" 
    description: "Specifies the shell window title. Defaults to \"Azure DevOps Shell\". " 
    globbing: false 
    type: "string" 
    defaultValue: "Azure DevOps Shell" 
  - name: "DoNotClearHost" 
    description: "Does not clear the host screen when activating the Azure DevOps Shell. When set, the prompt is updated without clearing the screen. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "NoLogo" 
    description: "Does not display the version banner (module description, version, and client library version) when activating the Azure DevOps Shell. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False" 
  - name: "NoProfile" 
    description: "Does not load the user profile script (TfsCmdlets_Profile.ps1, located in the same directory as the PowerShell profile) when activating the Azure DevOps Shell. " 
    globbing: false 
    type: "SwitchParameter" 
    defaultValue: "False"
inputs: 
outputs: 
notes: 
relatedLinks: 
  - text: "Online Version:" 
    uri: "https://tfscmdlets.dev/docs/cmdlets/Shell/Enter-TfsShell"
aliases: 
examples: 
---
