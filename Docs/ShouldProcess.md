# ShouldProcess and ShouldContinue Guidelines

This document describes the mandatory patterns for implementing confirmation support in TfsCmdlets cmdlets.

## Rule: All mutation cmdlets must support ShouldProcess

Every cmdlet that modifies state (verbs like `New`, `Set`, `Remove`, `Enable`, `Disable`, `Update`, `Rename`, `Import`, `Move`, etc.) **must** declare `SupportsShouldProcess = true` in its `[TfsCmdlet]` attribute. This enables `-WhatIf` and `-Confirm` support automatically.

```csharp
[TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
partial class SetSomething { /* ... */ }
```

## Confirmation levels

All TfsCmdlets mutation cmdlets use the default `ConfirmImpact` of `Medium`. The user gets a confirmation prompt only when `$ConfirmPreference` is `Medium` or `Low`, or when they explicitly pass `-Confirm`.

In the controller, call `ShouldProcess` before executing the mutation:

```csharp
if (!PowerShell.ShouldProcess(target, "Create something"))
    yield break;
```

### Irreversible operations (ShouldContinue + Force)

Destructive or irreversible cmdlets (e.g. `Remove-*`, hard-delete operations, permanent revocations) must use a two-barrier approach:

1. **`ShouldProcess`** — standard PowerShell infrastructure barrier (respects `-WhatIf`, `-Confirm`, and `$ConfirmPreference`).
2. **`ShouldContinue`** — operator-level safety barrier that requires explicit acknowledgment. Bypassed only by the `-Force` switch.

This prevents accidental destructive operations when a script or caller sets `$ConfirmPreference = 'None'` or passes `-Confirm:$false`.

#### Implementation pattern

In the cmdlet class, add a `Force` switch parameter:

```csharp
[TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
partial class RemoveSomething
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public object Item { get; set; }

    /// <summary>
    /// Suppresses the confirmation prompt for destructive operations.
    /// </summary>
    [Parameter]
    public SwitchParameter Force { get; set; }
}
```

In the controller, implement the gated two-barrier check:

```csharp
protected override IEnumerable Run()
{
    // 1st barrier: ShouldProcess (handles -WhatIf and -Confirm)
    if (!PowerShell.ShouldProcess(target, "Delete item"))
        yield break;

    // 2nd barrier: ShouldContinue (operator safety, only when -Force is not specified)
    if (!Force && !PowerShell.ShouldContinue(
        $"Are you sure you want to delete '{target}'? This operation is irreversible."))
        yield break;

    // Perform the destructive operation
    Client.DeleteAsync(itemId).Wait("Error deleting item");
}
```

## Behavior summary

| Scenario | ShouldProcess | ShouldContinue | Operation runs? |
|---|---|---|---|
| Interactive, no switches | Prompts (if ConfirmImpact ≥ $ConfirmPreference) | Prompts | Only if both confirmed |
| `-WhatIf` | Returns `false` (shows "What if" message) | Never reached | No |
| `-Confirm:$false` | Returns `true` silently | **Still prompts** | Only if ShouldContinue confirmed |
| `-Force` | Prompts normally | Bypassed | If ShouldProcess confirmed |
| `-Confirm:$false -Force` | Returns `true` silently | Bypassed | Yes (fully automated) |
| Pipeline / `$ConfirmPreference = 'None'` | Returns `true` silently | **Still prompts** | Only if ShouldContinue confirmed |
| Pipeline / `$ConfirmPreference = 'None'` + `-Force` | Returns `true` silently | Bypassed | Yes (fully automated) |

## Key points

- **`ShouldProcess`** is for the PowerShell engine. It respects `$ConfirmPreference`, `-WhatIf`, and `-Confirm`. It can be silenced by the caller or session state.
- **`ShouldContinue`** is for the operator. It always prompts interactively unless bypassed by `-Force`. It cannot be silenced by `$ConfirmPreference` or `-Confirm:$false`.
- The `-Force` parameter is the **only** way to bypass `ShouldContinue` in automation scripts. This ensures destructive operations in pipelines require explicit intent in the calling code.
- Never call `ShouldContinue` without gating it behind `!Force` — otherwise you create an unconditional interactive prompt that makes automation impossible.
- In interactive scenarios, `ShouldProcess` prompts when `$ConfirmPreference` is `Medium` or lower. `ShouldContinue` then adds a second, distinct confirmation for the destructive nature of the operation. This is acceptable for truly irreversible operations.

## When NOT to use ShouldContinue

- If the operation is a standard workflow step (e.g. creating a work item, setting a property), `ShouldProcess` alone is sufficient.
- Use the two-barrier pattern only when:
  - The operation is destructive and irreversible (e.g. permanent deletion, revoking tokens, destroying resources).
  - You need the user to read a specific warning that the generic `-Confirm` prompt does not convey.
  - You want to require `-Force` in automation scripts to guarantee explicit intent.

## Reversible vs. irreversible operations

The `-Force` + `ShouldContinue` barrier is **only required for irreversible operations**. Many Azure DevOps resources support a "recycle bin" concept — deleting them is a reversible (soft-delete) operation that can be undone. In those cases, `ShouldProcess` alone is sufficient; `-Force` is not needed.

However, when a cmdlet offers a **hard-delete** path (permanent, irreversible destruction), that specific path must require `-Force` and use `ShouldContinue`.

### Decision guide

| Operation | Reversible? | Requires `-Force`? | Example |
|---|---|---|---|
| Delete team project (soft) | Yes (recycle bin) | No | `Remove-TfsTeamProject` |
| Delete team project (hard) | **No** | **Yes** | `Remove-TfsTeamProject -Hard` |
| Delete work item | Yes (recycle bin) | No | `Remove-TfsWorkItem` |
| Destroy work item | **No** | **Yes** | `Remove-TfsWorkItem -Destroy` |
| Delete Git repository (empty) | Yes (recycle bin) | No | `Remove-TfsGitRepository` |
| Delete Git repository (non-empty) | Potentially destructive | **Yes** | `Remove-TfsGitRepository` (with content) |
| Revoke PAT | **No** | **Yes** | `Remove-TfsPersonalAccessToken` |

### Implementation example: reversible delete with irreversible hard-delete

```csharp
protected override IEnumerable Run()
{
    // ShouldProcess always required for any mutation
    if (!PowerShell.ShouldProcess(target, "Delete item"))
        yield break;

    if (Hard)
    {
        // Hard delete is irreversible — require Force + ShouldContinue
        if (!Force && !PowerShell.ShouldContinue(
            $"The item '{target}' will be permanently destroyed. " +
            "This operation is IRREVERSIBLE and may cause DATA LOSS. Continue?"))
            yield break;

        Client.HardDeleteAsync(itemId).Wait("Error destroying item");
    }
    else
    {
        // Soft delete goes to recycle bin — ShouldProcess is sufficient
        Client.SoftDeleteAsync(itemId).Wait("Error deleting item");
    }
}
```

The key principle: **`-Force` signals explicit intent for irreversible actions**. If the user (or a script) can undo the operation, the standard `ShouldProcess` confirmation is sufficient protection.

## Examples in this codebase

- `RemoveTeamProject` — uses `ShouldProcess` + `ShouldContinue` with `-Force` for soft-delete, and an additional `ShouldContinue` for hard-delete.
- `RemoveWorkItem` — uses `ShouldProcess` for standard delete, `ShouldContinue` gated by `-Force` for the `-Destroy` (permanent) path.
- `RemoveGitRepository` — uses `ShouldProcess` + `ShouldContinue` gated by `-Force` when the repository has a default branch (i.e. is not empty).
