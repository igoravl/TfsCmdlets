# The `Items` Pattern — Implicit Get Cmdlet Invocation

## Overview

In TfsCmdlets, mutation controllers (`Set-*`, `Remove-*`, `Rename-*`, `Update-*`, `Disable-*`, etc.) use a generated `Items` property that **automatically invokes the corresponding `Get-*` cmdlet** to resolve the input parameter into strongly-typed objects. This eliminates boilerplate for parameter resolution and ensures consistent lookup behavior across all cmdlets that operate on existing resources.

## How It Works

### Generation condition

The source generator (in `ControllerInfo.cs`) produces the `Items` property on a controller when **all** of the following are true:

1. The cmdlet verb is **not** `Get` (i.e., it is a mutation verb like `Set`, `Remove`, `Rename`, etc.).
2. The controller has **at least one declared parameter**.
3. The **first declared parameter** has type `object`.

The `object` type is key — it signals that the parameter accepts flexible input (a name/wildcard string, a Guid, or a typed object piped from the corresponding `Get-*` cmdlet).

### Generated code

The generator produces one of two variants depending on whether `DataType` is specified in the `[CmdletController]` attribute:

**Variant A — No DataType (untyped):**

```csharp
protected IEnumerable Items => Data.Invoke("Get", "{Noun}");
```

**Variant B — With DataType (typed, e.g., `[CmdletController(typeof(T))]`):**

```csharp
protected IEnumerable<T> Items => {FirstParamName} switch {
    T item => new[] { item },
    IEnumerable<T> items => items,
    _ => Data.GetItems<T>()
};
```

Where `T` is the DataType specified in `[CmdletController]` and `{FirstParamName}` is the name of the first declared parameter.

In Variant B, the `switch` expression provides a **short-circuit optimization**: if the user already piped a typed object (e.g., from `Get-TfsXxx | Remove-TfsXxx`), it is used directly without re-invoking the Get cmdlet. Only when the input is an unresolved value (string, Guid, wildcard) does `Data.GetItems<T>()` trigger the lookup.

### Invocation chain

```
User runs: Remove-TfsXxx -Xxx "MyItem*"
  │
  ├─ RemoveXxxController.Run()
  │    └─ foreach (var item in Items) { ... }
  │         │
  │         ├─ Items property evaluates: Xxx is string "MyItem*"
  │         │   → falls through to Data.GetItems<T>()
  │         │
  │         ├─ DataManagerImpl looks up GetXxxController
  │         │   → executes it with current parameters (Collection, Credential, etc.)
  │         │
  │         ├─ GetXxxController.Run() yields matching T objects
  │         │
  │         └─ Back in Remove loop → processes each resolved item
  │
  └─ Output: items matching "MyItem*" are processed
```

When the user pipes instead:

```
Get-TfsXxx "MyItem*" | Remove-TfsXxx
```

The `Items` property receives a `T` object directly (Variant B short-circuit) and skips the re-invocation entirely.

## Usage Pattern in Controllers

```csharp
[CmdletController(typeof(T), Client = typeof(ISomeHttpClient))]
partial class RemoveXxxController
{
    protected override IEnumerable Run()
    {
        // Items is generated — iterates over resolved objects
        foreach (var item in Items)
        {
            // item is already of type T — use it directly
            if (!PowerShell.ShouldProcess(item.Name, "Remove item"))
                yield break;

            Client.DeleteAsync(item.Id)
                .Wait("Error removing item");
        }
    }
}
```

## Rules

- **Do not** manually resolve the first parameter in mutation controllers — use `Items` instead.
- **Do not** call `Data.Invoke("Get", ...)` or `Data.GetItems<T>()` explicitly when `Items` is available.
- The first parameter in a mutation cmdlet **must** be typed as `object` for the generator to produce `Items`.
- The corresponding `Get-*` cmdlet **must** exist; otherwise `Items` will throw at runtime.
- Use `yield break` (not `continue`) inside the `foreach` when skipping an item due to `ShouldProcess` / `ShouldContinue`, unless you intend to process remaining items (in which case use `continue`).

## Source Generator Reference

- Generator logic: `CSharp/TfsCmdlets.SourceGenerators/Generators/Controllers/ControllerInfo.cs` — `GenerateItemsProperty()` method (line ~119).
- Condition check: same file, line ~231 — the tuple `(predicate, generator, label)` entry for "Items".
