What's new in 1.0.0.13-alpha8 (_04/Jun/2016_)
------------------------------------------

Whoa, that really took longer than expected! :-)

Right now we're in the middle of a migration from AppVeyor to VSTS. With that, we plan on leveraging VSTS's Release Management feature to simplify managing versions and releases.

By the end of it, we expect our releases to be easier, faster and thus more frequent. Let's see how it goes! 

On a brighter note: Now we have the first incarnation of Set-TfsWorkItem. Give it a spin!

### Improvements and Changes
  - **NEW**: Set-TfsWorkItem
  - Rename all mentions of VSO to VSTS

### Bug fixes
  - N/A

### Known issues
  - N/A

Previous Versions
-----------------

### 1.0.0-alpha7 (_22/Oct/2015_)

#### Improvements
  - Added essential help comments to all cmdlets. Future versions will improve the documentation quality and depth.

#### Bug Fixes
  - Fix Nuget and Chocolatey package icons
  - Fix bug in Get-TfsWorkItemType that would not return any WITDs

#### Known Issues
  - N/A

### 1.0.0-alpha6 (_22/Oct/2015_)

#### Improvements
  - Enable build from cmdline with VS 2015 ([ab2325b](https://github.com/igoravl/tfscmdlets/commit/ab2325bae7cce788292d8532742a230756d1fd06))
  - Change PoShTools detection from error to warning ([09e62f3](https://github.com/igoravl/tfscmdlets/commit/09e62f3b034e1706fb5845b3f8588658f99a21f8))
  - Skip PoShTools detection in AppVeyor ([47cafa4](https://github.com/igoravl/tfscmdlets/commit/47cafa40f16c3e9c7d6f18594154f994d74cfb9c))
  - Add custom type detection logic ([b10f32c](https://github.com/igoravl/tfscmdlets/commit/b10f32c5538576ea3cec7bf9f8b8d4c96eddba56))

#### Bug Fixes
  - Fix commit message with apostrophe breaking build ([8066ab8](https://github.com/igoravl/tfscmdlets/commit/8066ab8310fa21111e09c5ecba306914edb6e4ab))
  - Add proper parameter initialization for credentials ([d0c4d6c](https://github.com/igoravl/tfscmdlets/commit/d0c4d6c7d28682f43ae730904d802ebf4a2d4584))
  - Fix handling of current config server ([d7b53f](https://github.com/igoravl/tfscmdlets/commit/d7b53fca74a66f22f793bed39f1ef3bdf642ae83))

#### Known Issues
  - N/A

### 1.0.0-alpha5 (_10/Sep/2015_)

#### Improvements
  - Add cascade disconnection: When calling a "higher" Disconnect cmdlet (e.g. Disconnect-TfsConfigurationServer), the "lower" ones (e.g. TeamProjectCollection, TeamProject) are cascade-invoked, in other to prevent inconsistent connection information.

#### Bug fixes
  - Fix conditional use of -Title in New-TfsWorkItem ([96a8a60](https://github.com/igoravl/tfscmdlets/commit/818af6e9d6ba3f30e976f3ef20d6070ac50fa3e7))
  - Fix argument naming and pipelined return in Get-TfsWorkItem ([0a118125](https://github.com/igoravl/tfscmdlets/commit/0a11812554b447f4418e11454911ca5f53f34924))
  - Fix return when passing -Current in Get-TfsConfigurationServer ([58647d2f](https://github.com/igoravl/tfscmdlets/commit/58647d2f29d84d6f5db4e0022062c2dd30cfaba1))

#### Known Issues
  - N/A

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
