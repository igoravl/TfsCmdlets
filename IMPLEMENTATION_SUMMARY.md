# Install-TfsShell & Uninstall-TfsShell Implementation Summary

## Overview
This implementation adds two new cmdlets to TfsCmdlets that provide shell installation management with Windows Terminal support.

## Files Added/Modified

### New C# Cmdlets
- `CSharp/TfsCmdlets/Cmdlets/Shell/InstallShell.cs` - Install-TfsShell cmdlet implementation
- `CSharp/TfsCmdlets/Cmdlets/Shell/UninstallShell.cs` - Uninstall-TfsShell cmdlet implementation

### Windows Terminal Profile Fragments
- `PS/AzureDevOpsShell-WinPS.json` - Windows PowerShell profile for Windows Terminal
- `PS/AzureDevOpsShell-PSCore.json` - PowerShell Core profile for Windows Terminal

### Enhanced WiX Installer
- `Setup/Product.wxs` - Updated with Windows Terminal detection and conditional shortcut creation

### Documentation
- `CSharp/TfsCmdlets/Cmdlets/Shell/Install-TfsShell.md` - Cmdlet documentation
- `CSharp/TfsCmdlets/Cmdlets/Shell/Uninstall-TfsShell.md` - Cmdlet documentation
- `CSharp/TfsCmdlets/Cmdlets/Shell/index.md` - Updated index with new cmdlets

### Tests
- `PS/_Tests/Shell/InstallUninstallTfsShell.Tests.ps1` - Pester tests for new cmdlets
- `PS/_Tests/Shell/ManualValidationTest.ps1` - Manual validation script

## Key Features

### Install-TfsShell Cmdlet
- **Windows Terminal Detection**: Automatically detects if Windows Terminal is installed
- **Conditional Shortcut Creation**: Creates WT shortcuts if detected, PowerShell shortcuts otherwise
- **Profile Installation**: Deploys WT profile fragments to the user's settings
- **Target Support**: Supports StartMenu, Desktop, and WindowsTerminal targets
- **Force Parameter**: Allows forcing PowerShell shortcuts even when WT is detected

### Uninstall-TfsShell Cmdlet
- **Shortcut Removal**: Removes shortcuts from all known locations
- **Profile Cleanup**: Removes Windows Terminal profile fragments
- **Target Support**: Supports selective removal by target

### Windows Terminal Integration
- **Profile Fragments**: JSON files for both Windows PowerShell and PowerShell Core
- **Automatic Detection**: Registry and file system checks for wt.exe
- **Settings Deployment**: Copies profiles to WT settings directory

### Enhanced WiX Installer
- **Registry Search**: Detects Windows Terminal via App Paths registry
- **Conditional Components**: Two mutually exclusive shortcut components
- **Backward Compatibility**: Falls back to PowerShell shortcuts when WT not detected

## Usage Examples

```powershell
# Install with default targets (StartMenu + Desktop)
Install-TfsShell

# Install only to Start Menu
Install-TfsShell -Target StartMenu

# Force PowerShell shortcuts even if Windows Terminal is detected
Install-TfsShell -Force

# Remove from all locations
Uninstall-TfsShell

# Remove only Windows Terminal profiles
Uninstall-TfsShell -Target WindowsTerminal
```

## Implementation Notes

1. **C# Architecture**: Uses the existing TfsCmdlets pattern with ControllerBase inheritance
2. **Cross-Platform Aware**: Handles non-Windows environments gracefully
3. **Error Handling**: Comprehensive try-catch blocks with logging
4. **PowerShell Integration**: Uses PowerShell service for shortcut creation via COM
5. **Registry Safe**: Handles registry access failures gracefully
6. **Path Validation**: Validates all paths before operations

## Compatibility

- **Existing Shortcuts**: Compatible with existing chocolatey and WiX installer shortcuts
- **Chocolatey**: Existing chocolatey scripts remain unchanged for compatibility
- **Windows Terminal**: Supports both Windows PowerShell and PowerShell Core profiles
- **Legacy Systems**: Falls back to PowerShell shortcuts when WT is not available

## Testing

- **JSON Validation**: Verified JSON profile syntax and content
- **Cross-Platform Logic**: Tested detection logic simulation on Linux
- **Pester Tests**: Basic cmdlet availability and parameter validation
- **Manual Validation**: Comprehensive logic testing script

## Future Enhancements

1. **Chocolatey Integration**: Could update chocolatey scripts to use new cmdlets
2. **Taskbar Support**: Could add Windows Taskbar shortcut support
3. **Profile Customization**: Could allow custom profile names/settings
4. **Multiple Profiles**: Could support installing multiple profile variants