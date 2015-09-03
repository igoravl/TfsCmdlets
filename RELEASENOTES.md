
What's new in 1.0.0-alpha4 (_03/Sep/2015_)
------------------------------------------

### Improvements
  - Remove dependency on .NET 3.5

### Bug fixes
  - N/A

### Known Issues
  - N/A

Previous Versions
-----------------

### 1.0.0-alpha3 (_03/Sep/2015_)

#### Improvements
  - Add help comments to the Areas & Iterations functions

#### Bug fixes
  - Fix an issue in the AssemblyResolver implementation. Previously it was implemented as a scriptblock and was running into some race conditions that would crash PowerShell. Switched to a pure .NET implementation in order to avoid the race condition.

#### Known Issues
  - TfsCmdlets has currently a dependency on .NET 3.5 and won't load in computers with only .NET 4.x installed. Workaround is to install .NET 3.5. Next version will no longer depend on .NET 3.5.
