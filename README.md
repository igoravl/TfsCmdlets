# TfsCmdlets: PowerShell Cmdlets for Azure DevOps and Team Foundation Server

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/igoravl/tfscmdlets/master/LICENSE.md) [![Build status](https://github.com/igoravl/TfsCmdlets/actions/workflows/main.yml/badge.svg?label=Build)](https://github.com/igoravl/TfsCmdlets/actions/workflows/main.yml) [![GitHub release](https://img.shields.io/github/release/igoravl/tfscmdlets.svg)](https://github.com/igoravl/tfscmdlets/releases) [![Issues](https://img.shields.io/github/issues/igoravl/tfscmdlets.svg)](https://github.com/igoravl/tfscmdlets/issues) [![Forks](https://img.shields.io/github/forks/igoravl/tfscmdlets.svg)](https://github.com/igoravl/tfscmdlets/forks) [![Stars](https://img.shields.io/github/stars/igoravl/tfscmdlets.svg)](https://github.com/igoravl/tfscmdlets/stargazers)

[![PowerShell Gallery](https://img.shields.io/powershellgallery/dt/tfscmdlets?label=PSGallery)](https://www.powershellgallery.com/packages/TfsCmdlets) [![NuGet](https://img.shields.io/nuget/dt/TfsCmdlets.svg?label=Nuget)](https://nuget.org/packages/tfscmdlets) [![Chocolatey](https://img.shields.io/chocolatey/dt/TfsCmdlets.svg?label=Chocolatey)](https://chocolatey.org/packages/tfscmdlets) ![GitHub all releases](https://img.shields.io/github/downloads/igoravl/tfscmdlets/total?label=GitHub)

## What is TfsCmdlets?

TfsCmdlets is a PowerShell module which provides many commands ("cmdlets" in PowerShell parlance) to simplify automated interaction with Team Foundation Server (2010 to 2018) and Azure DevOps (Server 2019+ and Services).

By using TfsCmdlets, Azure DevOps administrators and/or power users can create scripts to automate many different tasks, ranging from retrieving work items to creating new team project collections.

TfsCmdlets is available in many installation formats. It also includes **Azure DevOps Shell**, a PowerShell window pre-configured to make interacting with Azure DevOps via command line a joy!

![Azure DevOps Shell](src/assets/TfsShell.png)

## Quick start guide

Do you have an Azure DevOps account created? Awesome! If not, you might want to [create one](https://azure.microsoft.com/en-us/services/devops/).

> **HINT**: Optionally, you may consider to leverage the [Azure DevOps Demo Generator](https://azuredevopsdemogenerator.azurewebsites.net/) to fill a team project with sample data, in order to have a sandbox to play with. The examples below will assume that you created a team project called **PartsUnlimited**, based on the namesake template available at the Azure DevOps Generator web site.

Next, install TfsCmdlets in your computer (see section "_How to install_", below), open a PowerShell window and try the following commands:

```PowerShell
# Connect to your Azure DevOps organization
# (Will be used as default for the -Collection argument when required by a cmdlet)
Connect-TfsTeamProjectCollection 'https://dev.azure.com/<your_organization_name>'

# Get a list of team projects in the currently connected organization
Get-TfsTeamProject

# List the existing iterations in the PartsUnlimited team project
Get-TfsIteration -Project PartsUnlimited

# Connect to the PartsUnlimited team project
# (will be used as default for the -Project argument when required by a cmdlet)
Connect-TfsTeamProject PartsUnlimited

# Create a new iteration
New-TfsIteration 'Sprint 7'

# Get all bugs in the current team project
Get-TfsWorkItem -WorkItemType 'Bug'

# Create a new PBI in the 'Sprint 7' iteration
New-TfsWorkItem -Title 'New product backlog item' -Type 'Product Backlog Item' -Iteration 'Sprint 7'
```

## How to install

TfsCmdlets can be obtained from many different sources and in many different formats. Choose the one most suitable to you!

### PowerShell Gallery

If you're using Windows 10, Windows Server 2016 (or later) or have installed Windows Management Framework 5 (or later) then the simplest way to install TfsCmdlets is via [PowerShell Gallery](https://www.powershellgallery.com/).

Open an elevated PowerShell prompt and type:

```PowerShell
Install-Module TfsCmdlets
```

Optionally, you can install it locally in your user profile. That is particularly useful when you can't run as an administrator or don't want to make the module available to all users in the computer:

```PowerShell
Install-Module TfsCmdlets -Scope CurrentUser
```

[Package details](https://www.powershellgallery.com/packages/TfsCmdlets/)

### Windows Package Manager (winget)

The new [Windows Package Manager](https://github.com/microsoft/winget-cli) ("winget") is a command line tool that enables developers to discover, install, upgrade, remove and configure applications on Windows 10 computers. This tool is the client interface to the Windows Package Manager service.

To install TfsCmdlets via `winget`, open a command prompt and type:

```PowerShell
winget install TfsCmdlets
```

[Package details](https://github.com/microsoft/winget-pkgs/tree/master/manifests/i/Igoravl/TfsCmdlets/)

### Chocolatey

Using [Chocolatey](https://www.chocolatey.org/)? Then open an elevated PowerShell prompt and type:

```PowerShell
choco install TfsCmdlets
```

[Package details](https://community.chocolatey.org/packages/TfsCmdlets/)

### Nuget

Nuget is a great option if you need to integrate TfsCmdlets with your continuous integration process (e.g. you need to create a TFS work item during the execution of an automated build).

To add TfsCmdlets to your solution, search for **TfsCmdlets** in the Visual Studio "_Manage Nuget packages for solution..._" dialog.

- Note: To add the latest pre-release version of TfsCmdlets, don't forget check the "Include prerelease" checkbox

[Package details](https://www.nuget.org/packages/tfscmdlets)

### Offline installation

When the target machine is not connected to the internet, none of the options above are available. In that case, your best bet is one of the offline installation alternatives below.

You can get one of the offline installers listed below from the [GitHub Releases](https://github.com/igoravl/tfscmdlets/releases) page.

#### Full installer (MSI-based)

The full installer will install the module files to the Program Files folder in your computer, make the module available to PowerShell and create the **Azure DevOps Shell** icon in the Start Menu.

**To install the full installer**:

- Download the MSI file from the [Releases](https://github.com/igoravl/tfscmdlets/releases) page;
- Open the downloaded MSI file

> **NOTE**: If Windows SmartScreen flags the file as insecure and refuses to run it, you can click the "More info" button/link in the dialog and select the "Run anyway" option.

**To uninstall the full installer**:

- Use the _Programs and Features_ (formerly "Add and remove programs") function in the Windows Control Panel.

#### Portable installer

The portable installer is a zip file containing all the required the module files.

**To install the portable installer**:

- Download the zip file from the [Releases](https://github.com/igoravl/tfscmdlets/releases) page;
- Open your Documents folder in Windows, then open folder `WindowsPowerShell` (for PowerShell 5.1) or `PowerShell` (for PowerShell 6+);
- Inside the PowerShell folder, create a new folder called `Modules` in it (if missing) and then create a new folder called `TfsCmdlets` in `Modules`;
- Extract the contents of the zip file to the `TfsCmdlets` folder.

You must end up with a folder structure similar to `[Documents]\[Windows]PowerShell\Modules\TfsCmdlets`. Files such as `TfsCmdlets.psd1` must be located in the TfsCmdlets folder.

To test the installation, open a new PowerShell window and type:

```PowerShell
Import-Module TfsCmdlets
```

**To uninstall the portable installer**:

- Close all PowerShell windows where you were using TfsCmdlets (to free files in use);
- Delete the `TfsCmdlets` folder from the `[Documents]\[Windows]PowerShell\Modules` folder.

## Contribution Guidelines

- [Contributor Guide](CONTRIBUTING.md)
- [Code of Conduct](CODE_OF_CONDUCT.md)

## Additional Information

- [Online Documentation](https://tfscmdlets.dev/)
- [Release Notes](https://github.com/igoravl/TfsCmdlets/blob/master/RELEASENOTES.md)
