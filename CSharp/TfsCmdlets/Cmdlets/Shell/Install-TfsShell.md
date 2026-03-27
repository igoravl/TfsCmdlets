# Install-TfsShell

Installs the Azure DevOps Shell shortcut with Windows Terminal support.

## Syntax

```powershell
Install-TfsShell [[-Target] <string[]>] [-Force] [<CommonParameters>]
```

## Description

The `Install-TfsShell` cmdlet installs shortcuts for the Azure DevOps Shell. It automatically detects if Windows Terminal is installed and creates appropriate shortcuts. If Windows Terminal is detected, it also installs Windows Terminal profile fragments for both Windows PowerShell and PowerShell Core.

## Parameters

### -Target

Specifies the installation target(s). Valid options are:
- `StartMenu` (default): Creates a shortcut in the Start Menu
- `Desktop` (default): Creates a shortcut on the Desktop
- `WindowsTerminal`: Installs Windows Terminal profiles (automatic when WT is detected)

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
Required: False
Position: 0
Default value: @("StartMenu", "Desktop")
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Forces installation even if Windows Terminal is not detected. When set, creates PowerShell shortcuts instead of Windows Terminal shortcuts.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## Examples

### Example 1

```powershell
Install-TfsShell
```

Installs Azure DevOps Shell shortcuts to the Start Menu and Desktop. If Windows Terminal is detected, creates Windows Terminal shortcuts and installs profile fragments.

### Example 2

```powershell
Install-TfsShell -Target StartMenu
```

Installs Azure DevOps Shell shortcut only to the Start Menu.

### Example 3

```powershell
Install-TfsShell -Force
```

Forces installation with PowerShell shortcuts even if Windows Terminal is detected.

## Related Links

- [Uninstall-TfsShell](Uninstall-TfsShell.md)
- [Enter-TfsShell](Enter-TfsShell.md)