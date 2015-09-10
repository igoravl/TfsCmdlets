What's new in 1.0.0-alpha5 (_10/Sep/2015_)
------------------------------------------

### Improvements
  - Add cascade disconnection: When calling a "higher" Disconnect cmdlet (e.g. Disconnect-TfsConfigurationServer), the "lower" ones (e.g. TeamProjectCollection, TeamProject) are cascade-invoked, in other to prevent inconsistent connection information.

### Bug fixes
  - Fix conditional use of -Title in New-TfsWorkItem ([96a8a60](https://github.com/igoravl/tfscmdlets/commit/818af6e9d6ba3f30e976f3ef20d6070ac50fa3e7))
  - Fix argument naming and pipelined return in Get-TfsWorkItem ([0a118125](https://github.com/igoravl/tfscmdlets/commit/0a11812554b447f4418e11454911ca5f53f34924))
  - Fix return when passing -Current in Get-TfsConfigurationServer ([58647d2f](https://github.com/igoravl/tfscmdlets/commit/58647d2f29d84d6f5db4e0022062c2dd30cfaba1))

### Known Issues
  - N/A

Previous Versions
-----------------

### 1.0.0-alpha4 (_03/Sep/2015_)

#### Improvements
  - Remove dependency on .NET 3.5

#### Bug fixes
  - N/A

#### Known Issues
  - N/A

### 1.0.0-alpha3 (_03/Sep/2015_)

#### Improvements
  - Add help comments to the Areas & Iterations functions

#### Bug fixes
  - Fix an issue in the AssemblyResolver implementation. Previously it was implemented as a scriptblock and was running into some race conditions that would crash PowerShell. Switched to a pure .NET implementation in order to avoid the race condition.

#### Known Issues
  - TfsCmdlets has currently a dependency on .NET 3.5 and won't load in computers with only .NET 4.x installed. Workaround is to install .NET 3.5. Next version will no longer depend on .NET 3.5.
