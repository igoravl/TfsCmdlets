# Uninstall-TfsShell

Removes the Azure DevOps Shell shortcuts and Windows Terminal profiles.

## Syntax

```powershell
Uninstall-TfsShell [[-Target] <string[]>] [<CommonParameters>]
```

## Description

The `Uninstall-TfsShell` cmdlet removes Azure DevOps Shell shortcuts and Windows Terminal profile fragments that were installed by `Install-TfsShell`.

## Parameters

### -Target

Specifies the installation target(s) to remove. Valid options are:
- `StartMenu` (default): Removes shortcut from the Start Menu
- `Desktop` (default): Removes shortcut from the Desktop  
- `WindowsTerminal` (default): Removes Windows Terminal profile fragments

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
Required: False
Position: 0
Default value: @("StartMenu", "Desktop", "WindowsTerminal")
Accept pipeline input: False
Accept wildcard characters: False
```

## Examples

### Example 1

```powershell
Uninstall-TfsShell
```

Removes Azure DevOps Shell shortcuts from all locations and removes Windows Terminal profile fragments.

### Example 2

```powershell
Uninstall-TfsShell -Target StartMenu
```

Removes Azure DevOps Shell shortcut only from the Start Menu.

### Example 3

```powershell
Uninstall-TfsShell -Target WindowsTerminal
```

Removes only the Windows Terminal profile fragments.

## Related Links

- [Install-TfsShell](Install-TfsShell.md)
- [Enter-TfsShell](Enter-TfsShell.md)